using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shared.Api.Middlewares;

public class RemoveCAPRoutes : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions.OfType<ApiDescription>())
        {
            if (apiDescription.RelativePath!.Contains("CAPROUTE"))
            {
                swaggerDoc.Paths.Remove($"/{apiDescription.RelativePath}");
            }
        }
    }
}
