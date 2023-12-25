using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using L718Framework.Infrastructure.Mapping.AutoMapper;
using L718Framework.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Main Configuration Extensions for all the Infrastructure Project 
/// </summary>
public static class Extensions{

    /// <summary>
    /// Add the base configuration for all the API Projects
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="applicationAssembly"></param>
    /// <param name="enableSwagger"></param>
    public static void AddInfrastructure(this WebApplicationBuilder builder, Assembly? applicationAssembly = null, bool enableSwagger = true){
        var config = builder.Configuration;
        config.AddEnvironmentVariables();

        
        builder.Services.AddCors(options=>{
            options.AddPolicy(name: "AllowAll", builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() );
        });

        builder.Services.AddControllers();

        if(applicationAssembly != null){
            builder.Services.AddAutoMapperExtension(applicationAssembly);
            builder.Services.AddValidatorsFromAssembly(applicationAssembly);
            builder.Services.AddMediatR(options=>options.RegisterServicesFromAssembly(applicationAssembly));
        }

        if(enableSwagger) {
            builder.Services.AddSwaggerGen();
        }

        builder.Services.BuildServiceProvider(false);

    }

    /// <summary>
    /// Extensions to turn on the installed infrastructure features
    /// </summary>
    /// <param name="app"></param>
    /// <param name="enableSwagger"></param>
    public static void UseInfrastructure(this WebApplication app, bool enableSwagger = true){
        if(enableSwagger && app.Environment.IsDevelopment()){
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.InitializeLogging();
        app.UseHttpsRedirection();
        app.MapControllers();

        
    }
}