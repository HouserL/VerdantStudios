using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.CompilerServices;
using VerdantFrontEnd.Library.DataAccess;

namespace VerdantWebFrontEnd
{
    public static class RegisteredServices
    {
        public static void ConfigureServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton<IMonsterData, MonsterData>();
        }
    }
}
