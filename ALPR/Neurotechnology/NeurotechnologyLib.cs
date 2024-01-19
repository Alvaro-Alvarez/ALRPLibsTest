using ALPR.Helper;
using ALPR.Models;

namespace ALPR.Neurotechnology
{
    public class NeurotechnologyLib
    {
        private List<string> _imageFiles;
        private NeurotechnologyOptions _neurotechnologyOptions;

        public NeurotechnologyLib(List<string> imageFiles)
        {
            _imageFiles = imageFiles;
            _neurotechnologyOptions = ConfigurationHelper.GetConfiguration<NeurotechnologyOptions>("Neurotechnology");
        }

        public void Process()
        {

        }
    }
}
