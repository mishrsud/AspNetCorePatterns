namespace Smi.Api.Configuration
{
    /*
     * Demonstrates using strongly typed configuration following the options pattern
     * Explained here: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-2.1
     * Sample here: https://github.com/aspnet/Docs/blob/master/aspnetcore/fundamentals/configuration/options/samples/2.x/OptionsSample/Pages/Index.cshtml.cs
     */
    
    public class CoffeeOptions
    {
        public bool Decaf { get; set; }
        public string MilkName { get; set; }
    }
}