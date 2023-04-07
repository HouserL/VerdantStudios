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
        var monsters = await _context.Monsters.Include(a => a.Actions)
                                                          .Include(m => m.Stats)
                                                          .ToListAsync();

        monsters.ForEach(m => m.DamageWeaknesses = 
                         _context.Database.GetDbConnection().Query<DamageType>
                         ("sp_Get_Monster_Weaknesses", new { @Id = m.Id }, commandType: CommandType.StoredProcedure)
                        .ToList());

        return monsters;
    }
    [HttpGet("/Monsters/{id}", Name = "Get Monster By Id")]
    public async Task<ActionResult<Monster>> GetMonsterById(int id)
    {
        var monster = await _context.Monsters.Include(m => m.Stats)
                                                    .Include(m => m.Actions)
                                                    .FirstOrDefaultAsync(m => m.Id == id);
        if (monster == null) return NotFound();
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

        _context.Monsters.Add(monster);
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
        var oldMonster = await _context.Monsters.FirstOrDefaultAsync(m => m.Id == 1);
        
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
                       new { @ActionId = i },
                       commandType: CommandType.StoredProcedure));
        
        monster.Actions.Where(a => a.Id != 0)
                       .ToList()
                       .ForEach(a => _context.Database
                       .GetDbConnection()
                       .Execute("sp_Update_Action", 
                       new{ 
                           @Id = a.Id,
                           @MonsterId = id, 
                           @Name = a.Name,
                           @Description = a.Description}, 
                       commandType: CommandType.StoredProcedure));
        
        monster.Actions.Where(a => a.Id == 0)
                       .ToList()
                       .ForEach(a => _context.Database
                       .GetDbConnection()
                       .Execute("sp_Insert_Action",
                       new
                       {
                           @MonsterId = a.Id,
                           @Name = a.Name,
                           @Description = a.Description
                       },
                       commandType: CommandType.StoredProcedure));

        _context.Database.GetDbConnection()
                         .Execute("sp_Update_Monster",
                          new
                             {
                                @Id = id,
                                @MonsterName = monster.Name,
                                @MonsterDescription = monster.Description,
                                @Strength = monster.Stats.Strength,
                                @Dexterity = monster.Stats.Dexterity,
                                @Constitution = monster.Stats.Constitution,
                                @Intelligence= monster.Stats.Intelligence,
                                @Wisdom = monster.Stats.Wisdom,
                                @Charisma = monster.Stats.Charisma
                          },
                          commandType: CommandType.StoredProcedure);
        
        monster.DamageWeaknesses.ForEach(w => _context.Database.GetDbConnection().Execute
                                         ("sp_Insert_Monster_Weakness",
                                         new { @MonsterId = monster.Id, @DamageTypeId = w.Id },
                                         commandType: CommandType.StoredProcedure));

        return Ok();
    }
    [HttpDelete("/Monsters/{id}", Name = "Delete Monster")]
    public async Task<ActionResult> DeleteMonster(int id)
    {
        var monster = await _context.Monsters.FindAsync(id);
        if (monster is null) return NotFound();

        await _context.Database.GetDbConnection().ExecuteAsync
                                ("sp_Delete_Monster", 
                                 new { @Id = id }, 
                                 commandType: CommandType.StoredProcedure);
        return Ok();
    }

}