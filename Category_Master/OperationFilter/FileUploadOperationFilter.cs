using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation == null || context == null)
            return;

        // Check if the operation contains any IFormFile parameters
        var fileParameters = context.MethodInfo
            .GetParameters()
            .Where(p => p.ParameterType == typeof(Microsoft.AspNetCore.Http.IFormFile))
            .ToList();

        if (!fileParameters.Any())
            return;

        // Modify operation parameters to support file uploads
        operation.Parameters.Clear();
        operation.RequestBody = new OpenApiRequestBody
        {
            Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties =
                        {
                            ["file"] = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            },
                            ["categoryId"] = new OpenApiSchema
                            {
                                Type = "integer",
                                Format = "int32"
                            },
                            ["subCategoryId"] = new OpenApiSchema
                            {
                                Type = "integer",
                                Format = "int32"
                            }
                        },
                        Required = new HashSet<string> { "file" } // Use HashSet instead of array
                    }
                }
            }
        };
    }
}