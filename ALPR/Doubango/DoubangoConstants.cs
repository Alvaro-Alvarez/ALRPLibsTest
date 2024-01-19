namespace ALPR.Doubango
{
    public static class DoubangoConstants
    {
        public const string Charset = "latin";

        public const string CONFIG_DEBUG_LEVEL = "info";
        public const bool CONFIG_DEBUG_WRITE_INPUT_IMAGE = false;
        public const string CONFIG_DEBUG_DEBUG_INTERNAL_DATA_PATH = ".";
        public const int CONFIG_NUM_THREADS = -1;
        public const bool CONFIG_GPGPU_ENABLED = true;
        public const int CONFIG_MAX_LATENCY = -1;
        public const string CONFIG_CHARSET = "latin";
        public const bool CONFIG_IENV_ENABLED = false;
        public const bool CONFIG_OPENVINO_ENABLED = true;
        public const string CONFIG_OPENVINO_DEVICE = "CPU";
        public const double CONFIG_DETECT_MINSCORE = 0.3;
        public static readonly IList<float> CONFIG_DETECT_ROI = new[] { 0f, 0f, 0f, 0f };
        public const bool CONFIG_CAR_NOPLATE_DETECT_ENABLED = false;
        public const double CONFIG_CAR_NOPLATE_DETECT_MINSCORE = 0.8;
        public const bool CONFIG_PYRAMIDAL_SEARCH_ENABLED = true;
        public const double CONFIG_PYRAMIDAL_SEARCH_SENSITIVITY = 0.33;
        public const double CONFIG_PYRAMIDAL_SEARCH_MINSCORE = 0.3;
        public const int CONFIG_PYRAMIDAL_SEARCH_MIN_IMAGE_SIZE_INPIXELS = 800;
        public const bool CONFIG_KLASS_LPCI_ENABLED = false;
        public const bool CONFIG_KLASS_VCR_ENABLED = false;
        public const bool CONFIG_KLASS_VMMR_ENABLED = false;
        public const bool CONFIG_KLASS_VBSR_ENABLED = false;
        public const double CONFIG_KLASS_VCR_GAMMA = 1.5;
        public const double CONFIG_RECOGN_MINSCORE = 0.2;
        public const string CONFIG_RECOGN_SCORE_TYPE = "min";
        public const bool CONFIG_RECOGN_RECTIFY_ENABLED = true;
    }
}
