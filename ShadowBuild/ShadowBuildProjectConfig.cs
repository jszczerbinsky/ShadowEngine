using System.Windows;

namespace ShadowBuild
{
    public class ShadowBuildProjectConfig
    {
        public string Name { get; private set; }
        public string Author { get; private set; }
        public Point StartResolution { get; private set; }
        public bool StartFullscreen { get; private set; }
    }
}
