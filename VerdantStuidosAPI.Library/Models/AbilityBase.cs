using System.Runtime.CompilerServices;

namespace VerdantAPI.Library.Models
{
    public class AbilityBase 
    {
        
        public int Id { get; set; }
        public int MonsterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}