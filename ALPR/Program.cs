using ALPR;
using ALPR.Doubango;
using ALPR.Helper;
using ALPR.Models;
using ALPR.Neurotechnology;

var generalCofig = ConfigurationHelper.GetConfiguration<GeneralOptions>("General");
var imageFiles = FileHelper.GetFiles(generalCofig.ImagesDirectory, generalCofig.AllowedExtensions);

if (imageFiles.Count() > generalCofig.ImagesToProcess)
    imageFiles = imageFiles.Take(generalCofig.ImagesToProcess).ToList();

switch (generalCofig.SelectedLibrary)
{
    case Constants.DoubangoLib:
        new DoubangoLib(imageFiles).Process();
        break;
    case Constants.NeurotechnologyLib:
        new NeurotechnologyLib(imageFiles).Process();
        break;
}

Console.WriteLine("Presiona cualquier tecla para terminar.");
Console.Read();
