namespace SwaggerTester.Web.Models
{
    public class Cat : Animal
    {
        public override AnimalFamilies Family => AnimalFamilies.Carnivore;

        public string EyeColour { get; set; }
    }
}
