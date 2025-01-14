using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SudokuServer.Data;
using SudokuServer.Helpers;
using SudokuServer.Repository;
using SudokuServer.Services;

namespace SudokuServer;

/// <summary>
/// 
/// </summary>
public class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);

        WebApplication app = builder.Build();

        ConfigureRequestPipeline(app);

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        IHostEnvironment environment = builder.Environment;

        //addd cors so SPA and other local apps can use this webservice without auth
        builder.Services.AddCors(opt =>
        {
            if (environment.IsDevelopment())
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback);
                });
            }
        });

        // Add services to the container.
        builder.Services.AddProblemDetails(opts =>
        {
            // Control when an exception details are included
            opts.IncludeExceptionDetails = (ctx, ex) =>
            {
                return environment.IsDevelopment() || environment.IsStaging();
            };
        });
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        IConfigurationSection config = builder.Configuration.GetSection("Database");

        string folder = Directory.GetCurrentDirectory();
        string connectionString = string.Format(config["ConnectionString"]!, folder);
        builder.Services.AddDbContext<PuzzleContext>(opt => opt.UseSqlite(connectionString));
        builder.Services.AddDbContext<SolutionContext>(opt => opt.UseSqlite(connectionString));

        builder.Services.AddScoped<IPuzzleRepository, PuzzleRepository>();
        builder.Services.AddScoped<ISolutionRepository, SolutionRepository>();
        builder.Services.AddScoped<IPuzzleService, PuzzleService>();
        builder.Services.AddScoped<ISolutionService, SolutionService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen(config =>
        {
            config.EnableAnnotations();
            config.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Sudonk Server - V1",
                    Version = "V1"
                }
            );
        });
    }

    private static void ConfigureRequestPipeline(WebApplication app)
    {
        app.UseCors();
        app.UseExceptionHandler(o => { });
        app.UseProblemDetails();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
