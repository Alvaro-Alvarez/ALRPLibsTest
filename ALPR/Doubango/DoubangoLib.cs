using ALPR.Helper;
using ALPR.Models;
using Newtonsoft.Json;
using org.doubango.ultimateAlpr.Sdk;
using System.Drawing;
using System.Drawing.Imaging;

namespace ALPR.Doubango
{
    public class DoubangoLib
    {
        private List<string> _imageFiles;
        private DoubangoOptions _doubangoOptions;

        public DoubangoLib(List<string> imageFiles)
        {
            _imageFiles = imageFiles;
            _doubangoOptions = ConfigurationHelper.GetConfiguration<DoubangoOptions>("Doubango");
        }

        public void Process()
        {
            foreach (string file in _imageFiles)
            {
                UltAlprSdkResult result = CheckResult("Init", UltAlprSdkEngine.init(BuildJSON(DoubangoConstants.Charset, _doubangoOptions.AssetsFolder, string.Empty)));

                Bitmap image = new Bitmap(file);


                int bytesPerPixel = Image.GetPixelFormatSize(image.PixelFormat) >> 3;
                if (bytesPerPixel != 1 && bytesPerPixel != 3 && bytesPerPixel != 4)
                {
                    throw new System.Exception("Invalid BPP:" + bytesPerPixel);
                }


                const int ExifOrientationTagId = 0x112;
                int orientation = 1;
                if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
                {
                    int orientation_ = image.GetPropertyItem(ExifOrientationTagId).Value[0];
                    if (orientation_ >= 1 && orientation_ <= 8)
                    {
                        orientation = orientation_;
                    }
                }

                BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);


                try
                {
                    //// For packed formats (RGB-family): https://www.doubango.org/SDKs/anpr/docs/cpp-api.html#_CPPv4N15ultimateAlprSdk16UltAlprSdkEngine7processEK22ULTALPR_SDK_IMAGE_TYPEPKvK6size_tK6size_tK6size_tKi
                    //// For YUV formats (data from camera): https://www.doubango.org/SDKs/anpr/docs/cpp-api.html#_CPPv4N15ultimateAlprSdk16UltAlprSdkEngine7processEK22ULTALPR_SDK_IMAGE_TYPEPKvPKvPKvK6size_tK6size_tK6size_tK6size_tK6size_tK6size_tKi
                    result = CheckResult("Process", UltAlprSdkEngine.process(
                            (bytesPerPixel == 1) ? ULTALPR_SDK_IMAGE_TYPE.ULTALPR_SDK_IMAGE_TYPE_Y : (bytesPerPixel == 4 ? ULTALPR_SDK_IMAGE_TYPE.ULTALPR_SDK_IMAGE_TYPE_BGRA32 : ULTALPR_SDK_IMAGE_TYPE.ULTALPR_SDK_IMAGE_TYPE_BGR24),
                            imageData.Scan0,
                            (uint)imageData.Width,
                            (uint)imageData.Height,
                            (uint)(imageData.Stride / bytesPerPixel),
                            orientation
                        ));
                    // Print result to console
                    Console.WriteLine("Result: {0}", result.json());
                }
                finally
                {
                    image.UnlockBits(imageData);
                }
            }
        }
        static UltAlprSdkResult CheckResult(string functionName, UltAlprSdkResult result)
        {
            if (!result.isOK())
            {
                string errMessage = string.Format("{0}: Execution failed: {1}", new string[] { functionName, result.json() });
                Console.Error.WriteLine(errMessage);
                throw new Exception(errMessage);
            }
            return result;
        }
        static string BuildJSON(string charsetAkaAlphabet, string assetsFolder = "", string tokenDataBase64 = "")
        {
            return JsonConvert.SerializeObject(new
            {
                debug_level = DoubangoConstants.CONFIG_DEBUG_LEVEL,
                debug_write_input_image_enabled = DoubangoConstants.CONFIG_DEBUG_WRITE_INPUT_IMAGE,
                debug_internal_data_path = DoubangoConstants.CONFIG_DEBUG_DEBUG_INTERNAL_DATA_PATH,

                num_threads = DoubangoConstants.CONFIG_NUM_THREADS,
                gpgpu_enabled = DoubangoConstants.CONFIG_GPGPU_ENABLED,
                max_latency = DoubangoConstants.CONFIG_MAX_LATENCY,
                ienv_enabled = DoubangoConstants.CONFIG_IENV_ENABLED,
                openvino_enabled = DoubangoConstants.CONFIG_OPENVINO_ENABLED,
                openvino_device = DoubangoConstants.CONFIG_OPENVINO_DEVICE,

                detect_minscore = DoubangoConstants.CONFIG_DETECT_MINSCORE,
                detect_roi = DoubangoConstants.CONFIG_DETECT_ROI,

                car_noplate_detect_enabled = DoubangoConstants.CONFIG_CAR_NOPLATE_DETECT_ENABLED,
                car_noplate_detect_min_score = DoubangoConstants.CONFIG_CAR_NOPLATE_DETECT_MINSCORE,

                pyramidal_search_enabled = DoubangoConstants.CONFIG_PYRAMIDAL_SEARCH_ENABLED,
                pyramidal_search_sensitivity = DoubangoConstants.CONFIG_PYRAMIDAL_SEARCH_SENSITIVITY,
                pyramidal_search_minscore = DoubangoConstants.CONFIG_PYRAMIDAL_SEARCH_MINSCORE,
                pyramidal_search_min_image_size_inpixels = DoubangoConstants.CONFIG_PYRAMIDAL_SEARCH_MIN_IMAGE_SIZE_INPIXELS,

                klass_lpci_enabled = DoubangoConstants.CONFIG_KLASS_LPCI_ENABLED,
                klass_vcr_enabled = DoubangoConstants.CONFIG_KLASS_VCR_ENABLED,
                klass_vmmr_enabled = DoubangoConstants.CONFIG_KLASS_VMMR_ENABLED,
                klass_vbsr_enabled = DoubangoConstants.CONFIG_KLASS_VBSR_ENABLED,
                klass_vcr_gamma = DoubangoConstants.CONFIG_KLASS_VCR_GAMMA,

                recogn_minscore = DoubangoConstants.CONFIG_RECOGN_MINSCORE,
                recogn_score_type = DoubangoConstants.CONFIG_RECOGN_SCORE_TYPE,
                recogn_rectify_enabled = DoubangoConstants.CONFIG_RECOGN_RECTIFY_ENABLED,

                // Value added using command line args
                assets_folder = assetsFolder,
                charset = charsetAkaAlphabet,
                license_token_data = tokenDataBase64,
            });
        }
    }
}
