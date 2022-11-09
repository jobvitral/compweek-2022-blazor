using CompWeek.Identity.Configuration;
using CompWeek.Identity.Domain.Interfaces;
using CompWeek.Identity.Infrastructure;
using Serilog;

namespace CompWeek.Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddClientStore<ClientStore>()
            .AddProfileService<ProfileService>();

        builder.Services.AddCors();
        builder.Services.AddControllers();

        // inicializa a injecao de dependencia
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        app.UseIdentityServer();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        return app;
    }
}