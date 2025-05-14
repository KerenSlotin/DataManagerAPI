
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

internal static class ServiceCollectionExtensions
{
  internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
  {
    services.AddSwaggerGen(o =>
    {
      o.CustomSchemaIds(id => id.FullName!.Replace("+", "-"));
      var securitySchema = new OpenApiSecurityScheme
      {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT"
      };

      o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);

      var securityRequirement = new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = JwtBearerDefaults.AuthenticationScheme
            }
          }, []
        }
      };

      o.AddSecurityRequirement(securityRequirement);
      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });


    return services;
  }
}