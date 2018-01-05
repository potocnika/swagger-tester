using System.Threading.Tasks;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Contexts;
using System.Collections.Generic;
using NSwag;
using SwaggerTester.Web.Definitions;

namespace SwaggerTester.Web.Swagger
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private const string TypeParameter = "type";
        private readonly string UrlTypeParameter = $"{{{TypeParameter}}}";

        public async Task ProcessAsync(DocumentProcessorContext context)
        {
            var operationsToRemove = new List<string>();
            var operationsToAdd = new Dictionary<string, SwaggerOperations>();

            foreach (var operation in context.Document.Paths)
            {
                if (!operation.Key.Contains(UrlTypeParameter))
                    continue;
                operationsToRemove.Add(operation.Key);
                foreach (var type in AnimalTypeMapper.Maps)
                {
                    var path = operation.Key.Replace(UrlTypeParameter, type.Key);
                    var newOperations = new SwaggerOperations();
                    foreach (var response in operation.Value)
                        newOperations.Add(response.Key, await OperationBuilder.BuildOperationAsync(type.Key, response.Value, TypeParameter));
                    operationsToAdd.Add(path, newOperations);
                }
            }
            foreach (var remove in operationsToRemove)
                context.Document.Paths.Remove(remove);
            foreach (var newOperation in operationsToAdd)
                context.Document.Paths.Add(newOperation.Key, newOperation.Value);
        }
    }
}
