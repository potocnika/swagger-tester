using SwaggerTester.Web.Models;
using System;
using System.Collections.Generic;

namespace SwaggerTester.Web.Definitions
{
    public static class AnimalTypeMapper
    {
        public const string Cat = "cat";
        public const string Dog = "dog";
        public const string Horse = "horse";

        public static Dictionary<string, Type> Maps { get; } = new Dictionary<string, Type>
        {
            { Cat, typeof(Cat) },
            { Dog, typeof(Dog) },
            { Horse, typeof(Horse) }
        };
    }
}
