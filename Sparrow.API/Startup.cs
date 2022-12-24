using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Sparrow.Data;
using Sparrow.Core;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Sparrow.Services.Behaviours;
using Sparrow.Services.Commands;
using Sparrow.API.Results.Wrapping;
using Sparrow.API.Results.ExceptionHandling;
using Sparrow.API.Results;

namespace Sparrow.API;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine(Environment.EnvironmentName);

        // Swagger
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(AppConsts.ApiVersion, new() { Title = AppConsts.ApiTitle, Version = AppConsts.ApiVersion });
            //uncomment for v2
            //options.SwaggerDoc("v2", new() { Title = AppConsts.ApiTitle, Version = "v2" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });


            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // Add services to the container.
        //Adds services required for using options.
        services.AddOptions();
        services.AddCors();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddHttpContextAccessor();
        services.AddHealthChecks();


        services.AddControllers(options =>
        {
            options.Filters.AddService(typeof(GlobalExceptionFilter));
            options.Filters.AddService(typeof(ResultFilter));
        });
        services.AddTransient<IActionResultWrapperFactory, ActionResultWrapperFactory>();
        services.AddTransient<ResultFilter>();
        services.AddTransient<GlobalExceptionFilter>();

        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        //MediatR
        services.AddMediatR(typeof(PingCommand));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        //db context
        services.AddDbContext<SparrowDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddSwaggerGen();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddSingleton(Configuration);
    }


    public void Configure(IApplicationBuilder app,
    IWebHostEnvironment env,
    IApiVersionDescriptionProvider provider)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResultStatusCodes =
            {
                    [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                    [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
            });

        });


        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }
}
