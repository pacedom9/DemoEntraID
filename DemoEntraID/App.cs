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
            AuthorizationUrl = new Uri("https://login.microsoftonline.com/1a59e398-83d8-4052-aec9-74a7d6461c5e/oauth2/v2.0/authorize"),
            TokenUrl = new Uri("https://login.microsoftonline.com/1a59e398-83d8-4052-aec9-74a7d6461c5e/oauth2/v2.0/token"),
            Scopes = new Dictionary<string, string>
                    {
                        { "api://50ddbf3e-c916-4ae7-9061-98a42e335337/demo", "Access API" }
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
                new[] { "api://50ddbf3e-c916-4ae7-9061-98a42e335337/demo" }
            }
        });
    });

    return builder;
  }
}
