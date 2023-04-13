namespace VerdantWebUI.Library.Models;

public class Monster
{
    public int Id { get; set; }
    public string Name { get; set; } =string.Empty;
    public string Description { get; set; }= string.Empty;
    public Stats Stats { get; set; } = new Stats();
    public List<StandardAction> Actions { get; set; } = new();
    public List<DamageType> DamageWeaknesses { get; set; } = new();
    public Monster()
    {
        
    }

}
