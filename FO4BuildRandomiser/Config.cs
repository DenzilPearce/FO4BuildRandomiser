namespace FO4BuildRandomiser
{
    internal class Config
    {
        public bool includeFarHarbor { get; set; }
        public bool includeNukaWorld { get; set; }
        public int minimumSPECIAL { get; set; }
        public int maximumSPECIAL { get; set; }
        public bool allowRerolls { get; set; }
        public int bias { get; set; }

        public Config() { }

        public Config(bool includeFarHarbor, bool includeNukaWorld, int minimumSPECIAL, int maximumSPECIAL, bool allowRerolls, int bias)
        {
            this.includeFarHarbor = includeFarHarbor;
            this.includeNukaWorld = includeNukaWorld;
            this.minimumSPECIAL = minimumSPECIAL;
            this.maximumSPECIAL = maximumSPECIAL;
            this.allowRerolls = allowRerolls;
            this.bias = bias;
        }
    }
}
