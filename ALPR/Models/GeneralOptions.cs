namespace ALPR.Models
{
    public class GeneralOptions
    {
        public string SelectedLibrary { get; set; }
        public string AllowedExtensions { get; set; }
        public string ImagesDirectory { get; set; }
        public int ImagesToProcess { get; set; }
    }
}
