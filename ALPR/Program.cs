// See https://aka.ms/new-console-template for more information
using ALPR;
using ALPR.Helper;
using ALPR.Models;

var generalCofig = ConfigurationHelper.GetConfiguration<GeneralOptions>("General");
new Process().Run();

Console.WriteLine("Presiona cualquier tecla para terminar.");
Console.Read();
