using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using VerdantAPI.Library.Context;
using VerdantAPI.Library.Models;

namespace VerdantAPI.Controllers;

[ApiController] [Route("[controller]")]
public class MonsterController : Controller
{
    private readonly MonsterDBContext _context;
    
    public MonsterController(MonsterDBContext context)
    {
        _context = context;
    }
    
    [HttpGet("/Monsters", Name = "Get All Monster")]
    public async Task<ActionResult<IEnumerable<Monster>>> GetAllMonsters()
    {
        var monsters = new List<Monster>();
        var actions = new List<StandardAction>();
        var stats = new List<Stats>();

        using (var lists = _context.Database.GetDbConnection().QueryMultiple("sp_Get_All_Monsters_Full"))
        {
            monsters = lists.Read<Monster>().ToList();
            stats = lists.Read<Stats>().ToList();
            actions = lists.Read<StandardAction>().ToList();
        }
        if(monsters.Count < 1 || actions.Count < 1 || stats.Count < 1) return NotFound();

        monsters.ForEach(m => m.Stats = stats.FirstOrDefault(s => s.Id == m.Id));

        monsters.ForEach(m => m.Actions = actions.Where(a => a.MonsterId == m.Id).ToList());



        monsters.ForEach(m => m.DamageWeaknesses = 
                         _context.Database.GetDbConnection().Query<DamageType>
                         ("sp_Get_Monster_Weaknesses", new { m.Id }, commandType: CommandType.StoredProcedure)
                        .ToList());


        return monsters;
    }
   
    [HttpGet("/Monsters/{id}", Name = "Get Monster By Id")]
    public async Task<ActionResult<Monster>> GetMonsterById(int id)
    {
        var monster = _context.Database.GetDbConnection()
            .QueryAsync<Monster>
            ("sp_Get_Monster_By_Id", 
             new { @Id = id }, 
             commandType: CommandType.StoredProcedure)
            .Result.First();
       
        if (monster == null) return NotFound();
        
        monster.Stats = _context.Database.GetDbConnection()
            .QueryAsync<Stats>
            ("sp_Get_Monster_Stats_By_Id",
             new { @Id = id },
             commandType: CommandType.StoredProcedure)
            .Result.First();

        monster.Actions = _context.Database.GetDbConnection()
            .QueryAsync<StandardAction>
            ("sp_Get_StandardActions_By_Id",
            new { @Id = id },
            commandType: CommandType.StoredProcedure)
            .Result.ToList();

        monster.DamageWeaknesses = _context.Database.GetDbConnection()
                                    .Query<DamageType>("sp_Get_Monster_Weaknesses", 
                                    new { @Id = id }, commandType: CommandType.StoredProcedure)
                                    .ToList();

        return monster;
    }
    
    [HttpPost("/Monsters", Name = "Create Monster")]
    public async Task<ActionResult> CreateMonster(Monster monster)
    {
        var weakness = monster.DamageWeaknesses.ToList();
        
        monster.DamageWeaknesses.Clear();

        //_context.Monsters.Add(monster);
        await _context.SaveChangesAsync();

        weakness.ForEach(w => _context.Database.GetDbConnection().Execute
                         ("sp_Insert_Monster_Weakness", 
                         new { @MonsterId = monster.Id, @DamageTypeId = w.Id }, 
                         commandType: CommandType.StoredProcedure));
        
        return Ok();
    }
    
    [HttpPut("/Monsters/{id}", Name = "Update Monster")]
    public async Task<ActionResult> UpdateMonster(Monster monster, int id)
    {
        //var oldMonster = await _context.Monsters//.Include(m => m.Actions).FirstOrDefaultAsync(m => m.Id == id);

        var oldMonster = _context.Database.GetDbConnection().Query<Monster>("sp_Get_Monster_By_Id",new { @Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();

        oldMonster.Actions = _context.Database.GetDbConnection()
            .QueryAsync<StandardAction>
            ("sp_Get_StandardActions_By_Id",
            new { @Id = id },
            commandType: CommandType.StoredProcedure)
            .Result.ToList();

        if (oldMonster is null) return NotFound();

        var deletedActions = oldMonster.Actions.Select(a => a.Id)
                                                .Where(a => a != 0)
                                                .ToList();
                                                
        deletedActions.RemoveAll(i => monster.Actions
                      .Select(n => n.Id)
                      .Contains(i));

        deletedActions.ForEach(i => _context.Database
                       .GetDbConnection()
                       .Execute("sp_Delete_Action",
                       new { @Id = i },
                       commandType: CommandType.StoredProcedure));
        
        monster.Actions.Where(a => a.Id != 0)
                       .ToList()
                       .ForEach(a => _context.Database
                       .GetDbConnection()
                       .Execute("sp_Update_Action", 
                       new{ 
                           a.Id,
                           @MonsterId = id, 
                           a.Name,
                           a.Description}, 
                       commandType: CommandType.StoredProcedure));
        
        monster.Actions.Where(a => a.Id == 0)
                       .ToList()
                       .ForEach(a => _context.Database
                       .GetDbConnection()
                       .Execute("sp_Insert_Action",
                       new
                       {
                           @MonsterId = id,
                           a.Name,
                           a.Description
                       },
                       commandType: CommandType.StoredProcedure));

        var m = GetMonsterParamerters(monster, id);
        
        _context.Database.GetDbConnection()
                         .Execute("sp_Update_Monster",
                          new
                             {
                                @Id = id,
                                @Monster = GetMonsterParamerters(monster, id).AsTableValuedParameter("udt_Monster"),
                                //monster.Stats.Strength,
                          },
                          commandType: CommandType.StoredProcedure);

        _context.Database.GetDbConnection().Execute(
            "sp_Delete_MDTW_Monsters",
            new { @Id = id },
            commandType: CommandType.StoredProcedure);
        
        monster.DamageWeaknesses.ForEach(w => _context.Database.GetDbConnection().Execute
                                         ("sp_Insert_Monster_Weakness",
                                         new { @MonsterId = id, @DamageTypeId = w.Id },
                                         commandType: CommandType.StoredProcedure));

        return Ok();
    }
    
    [HttpDelete("/Monsters/{id}", Name = "Delete Monster")]
    public async Task<ActionResult> DeleteMonster(int id)
    {
        var monster = _context.Database.GetDbConnection().Query<Monster>("sp_Get_Monster_By_Id", new { @Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        if (monster is null) return NotFound();

        await _context.Database.GetDbConnection().ExecuteAsync
                                ("sp_Delete_Monster", 
                                 new { @Id = id }, 
                                 commandType: CommandType.StoredProcedure);
        return Ok();
    }

    private static DataTable GetMonsterParamerters(Monster monster, int id)
    {
        var output = new DataTable();

        output.Columns.Add("[Id}", typeof(int));
        output.Columns.Add("[MonsterName]", typeof(string));
        output.Columns.Add("[Description]", typeof(string));
        output.Columns.Add("[STR]", typeof(int));
        output.Rows.Add
            (id,
             monster.Name, 
             monster.Description,
             monster.Stats.Strength);

        return output;
    }

}