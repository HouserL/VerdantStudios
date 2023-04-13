using System.Net;
using VerdantWebUI.Library.Models;

namespace VerdantWebUI.Library.DataAccess;

public class MonsterData : IMonsterData
{
    private readonly string _name = string.Empty;
    private readonly string URL = "";

    public void CreateMonster(Monster monster)
    {
        HttpClient client = new ();
        client.BaseAddress = new Uri(URL);
    }
}