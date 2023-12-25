using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace MusicPracticePlanner.Configuration;
public static class Extensions
{
 
    public static CommonConfigurationSection GetCommonConfiguration(this WebApplicationBuilder builder)
    {
        return builder.Configuration.Get<CommonConfigurationSection>();
    }
}
