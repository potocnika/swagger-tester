using NSwag.Annotations;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwaggerTester.Web.Swagger
{
    public class OperationProcessor : IOperationProcessor
    {
        public async Task<bool> ProcessAsync(OperationProcessorContext context)
        {
            var attributes = context.MethodInfo.GetCustomAttributes(typeof(SwaggerResponseAttribute), true);
            context.OperationDescription.Operation.ExtensionData = new Dictionary<string, object>();
            foreach (SwaggerResponseAttribute attribute in attributes)
                context.OperationDescription.Operation.ExtensionData.Add(attribute.StatusCode.ToString(), attribute.Type.AssemblyQualifiedName);
            return await Task.FromResult(true);
        }
    }
}
