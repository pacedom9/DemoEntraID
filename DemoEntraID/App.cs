using Microsoft.OpenApi.Models;

namespace DemoEntraID;

public static class App
{
  public static WebApplicationBuilder AddSwaggerDoc(this WebApplicationBuilder builder)
  {
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1",
          new OpenApiInfo
          {
            Title = "Swagger API",
            Version = "v1",
            Description = "OAuth2.0 using Authorization Code flow with Azure Entra ID"
          });

      c.CustomSchemaIds(type => type.FullName);

      c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
          AuthorizationCode = new OpenApiOAuthFlow
          {
            AuthorizationUrl = new Uri(builder.Configuration["SwaggerEntraID:AuthorizationUrl"]!),
            TokenUrl = new Uri(builder.Configuration["SwaggerEntraID:TokenUrl"]!),
            Scopes = new Dictionary<string, string>
                    {
                        { builder.Configuration["SwaggerEntraID:Scope"]!, "Access API" }
                    }
          }
        }
      });

      c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                    }
                },
                new[] { builder.Configuration["SwaggerEntraID:Scope"]! }
            }
        });
    });

    return builder;
  }
}
