namespace SwaggerTester.Web.Models
{
    public class Dog : Animal
    {
        public override AnimalFamilies Family => AnimalFamilies.Carnivore;
        public string Breed { get; set; }
    }
}
