using NJsonSchema;
using NJsonSchema.Generation;
using NSwag;
using SwaggerTester.Web.Definitions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerTester.Web.Swagger
{
    public static class OperationBuilder
    {
        public static async Task<SwaggerOperation> BuildOperationAsync(string type, SwaggerOperation existingOperation, params string[] excludeParams)
        {
            var operation = new SwaggerOperation()
            {
                Consumes = existingOperation.Consumes,
                ExtensionData = existingOperation.ExtensionData,
                ExternalDocumentation = existingOperation.ExternalDocumentation,
                OperationId = $"{existingOperation.OperationId}_{type}",
                Produces = existingOperation.Produces,
                Schemes = existingOperation.Schemes,
                Security = existingOperation.Security,
                Summary = existingOperation.Summary,
                Tags = existingOperation.Tags
            };

            foreach (var param in existingOperation.Parameters.Where(x => !excludeParams.Any() || !excludeParams.Contains(x.Name)))
            {
                operation.Parameters.Add(param);
            }

            var settings = new JsonSchemaGeneratorSettings
            {
                GenerateAbstractProperties = false,
                FlattenInheritanceHierarchy = true,
                DefaultEnumHandling = EnumHandling.String,
                ContractResolver = new JsonSchemaContractResolver(AnimalTypeMapper.Maps[type])
            };

            foreach (var existingResponse in existingOperation.Responses)
            {
                var responseTypeName = existingOperation.ExtensionData.ContainsKey(existingResponse.Key)
                    ? (string)existingOperation.ExtensionData[existingResponse.Key]
                    : null;
                var responseType = string.IsNullOrEmpty(responseTypeName)
                    ? null
                    : Type.GetType(responseTypeName);

                if (responseType != null)
                {
                    operation.Responses[existingResponse.Key] = new SwaggerResponse()
                    {
                        Schema = await JsonSchema4.FromTypeAsync(responseType, settings)
                    };
                }
            }

            return operation;
        }
    }
}
