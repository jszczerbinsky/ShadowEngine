using System.Windows;

namespace ShadowBuild
{
    public class ProjectConfig
    {
        public string Name { get; internal set; }
        public string Author { get; internal set; }

        public string LicenseFilePath { get; internal set; }

        public string ProjectVersion { get; internal set; }
        public string ShadowBuildVersion { get; internal set; }

        public Point StartResolution { get; internal set; }
        public bool StartFullscreen { get; internal set; }

        public string AxisConfigPath { get; internal set; }
        public bool AxisConfigAutoLoad { get; internal set; }
        public string LayerConfigPath { get; internal set; }
        public bool LayerConfigAutoLoad { get; internal set; } 
        public string TextureConfigPath { get; internal set; }
        public bool TextureConfigAutoLoad { get; internal set; }

        internal ProjectConfig() { }
    }
}
