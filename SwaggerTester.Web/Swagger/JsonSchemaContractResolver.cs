using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SwaggerTester.Web.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SwaggerTester.Web.Swagger
{
    public class JsonSchemaContractResolver : DefaultContractResolver
    {
        private readonly Type _type;
        private readonly Dictionary<Type, Type> _mappings;

        public JsonSchemaContractResolver(Type type)
        {
            _type = type;
            var baseType = typeof(Animal);
            var listType = typeof(List<>);
            var ienumerableType = typeof(IEnumerable<>);
            _mappings = new Dictionary<Type, Type>
                {
                    { baseType, type },
                    { listType.MakeGenericType(baseType), listType.MakeGenericType(_type) },
                    { ienumerableType.MakeGenericType(baseType), ienumerableType.MakeGenericType(_type) }
                };
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo && _mappings.ContainsKey(property.PropertyType))
                property.PropertyType = _mappings[property.PropertyType];
            return property;
        }
    }
}
