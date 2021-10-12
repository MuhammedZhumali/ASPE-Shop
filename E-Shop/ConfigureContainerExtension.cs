using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace E_Shop
{
  public static class ConfigureContainerExtension
  {
    public static void AddApiVersions(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddApiVersioning(
          config =>
          {
            config.ReportApiVersions = true;
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.ApiVersionReader = new HeaderApiVersionReader("api-version");
          });
    }
    public static void AddSwagger(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sapsan", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
          In = ParameterLocation.Header,
          Description = "Please enter into field the word 'Bearer' following by space and JWT",
          Scheme = "Bearer",
          Name = "Authorization",
          Type = SecuritySchemeType.Http
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "Bearer",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                        new List<string>()
                        }
                    });
      });
    }
  }
}
