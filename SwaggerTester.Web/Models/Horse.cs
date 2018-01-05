using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerTester.Web.Models
{
    public class Horse : Animal
    {
        public override AnimalFamilies Family => AnimalFamilies.Herbivore;
        public int Weight { get; set; }
        public bool IsRacer { get; set; }
    }
}
