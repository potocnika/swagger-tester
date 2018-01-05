using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema;
using NJsonSchema.Generation;
using NSwag.Annotations;
using SwaggerTester.Web.Definitions;
using SwaggerTester.Web.Models;
using SwaggerTester.Web.Swagger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwaggerTester.Web.Controllers
{
    public class AnimalsController : ControllerBase
    {
        [SwaggerResponse(StatusCodes.Status200OK, typeof(SuccessEnvelope), Description = "All good")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorEnvelope), Description = "Something went wrong")]
        [HttpGet("animals/{type}")]
        public IActionResult Get([FromRoute] string type)
        {
            try
            {
                return Ok(new SuccessEnvelope { Animals = Mock(type) });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorEnvelope(ex));
            }
        }

        [SwaggerResponse(StatusCodes.Status200OK, typeof(string), Description = "All good")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorEnvelope), Description = "Something went wrong")]
        [HttpGet("animals/{type}/schema")]
        public async Task<IActionResult> GetSchema([FromRoute] string type)
        {
            try
            {
                var concreteType = AnimalTypeMapper.Maps[type];
                var settings = new JsonSchemaGeneratorSettings
                {
                    GenerateAbstractProperties = false,
                    FlattenInheritanceHierarchy = true,
                    DefaultEnumHandling = EnumHandling.String,
                    ContractResolver = new JsonSchemaContractResolver(concreteType)
                };
                var schema = await JsonSchema4.FromTypeAsync(concreteType, settings);
                return Content(schema.ToJson(), "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorEnvelope(ex));
            }
        }

        private List<Animal> Mock(string type)
        {
            switch (type)
            {
                case AnimalTypeMapper.Cat:
                    return new List<Animal>
                    {
                        new Cat { EyeColour = "yellow" },
                        new Cat { EyeColour = "brown" }
                    };
                case AnimalTypeMapper.Dog:
                    return new List<Animal>
                    {
                        new Dog { Breed = "German Shepherd" },
                        new Dog { Breed = "Poodle" },
                        new Dog { Breed = "Mastif" }
                    };
                case AnimalTypeMapper.Horse:
                    return new List<Animal>
                    {
                        new Horse { IsRacer = true, Weight = 100 },
                        new Horse { IsRacer = true, Weight = 110 },
                        new Horse { IsRacer = false, Weight = 150 },
                        new Horse { IsRacer = false, Weight = 200 }
                    };
            }
            throw new Exception("Animal type not supported");
        }
    }
}
