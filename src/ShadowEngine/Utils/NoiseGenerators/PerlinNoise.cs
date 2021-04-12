using ShadowEngine.Objects.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowEngine.Utils.NoiseGenerators
{
    public class PerlinNoise
    {
        public float Size = 5;
        private Random rand = new Random();

        public PerlinNoise()
        {

        }

        private float lerp(float a0, float a1, float w)
        {
            return (1f - w) * a0 + w * a1;
        }

        private float dotGridGradient(int ix, int iy, float x, float y)
        {

            float dx = x - ix;
            float dy = y - iy;

            return (float)(dx * Math.Cos(rand.NextDouble()+rand.Next()) + dy * Math.Sin(rand.NextDouble() + rand.Next()));

        }

        private float getValue(float x, float y)
        {
            int x0 = (int)Math.Floor(x);
            int x1 = x0 + 1;
            int y0 = (int)Math.Floor(y);
            int y1 = y0 + 1;

            float sx = x - x0;
            float sy = y - y0;

            float n0, n1, ix0, ix1, value;
            n0 = dotGridGradient(x0, y0, x, y);
            n1 = dotGridGradient(x1, y0, x, y);
            ix0 = lerp(n0, n1, sx);
            n0 = dotGridGradient(x0, y1, x, y);
            n1 = dotGridGradient(x1, y1, x, y);
            ix1 = lerp(n0, n1, sx);
            value = lerp(ix0, ix1, sy);

            return value;
        }

        public System.Drawing.Bitmap GenerateBitmap(Size size)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)size.Width, (int)size.Height);

            float xMultipler = Size / size.Width;
            float yMultipler = Size / size.Height;

            for (int x = 0; x < (int)size.Width; x++)
                for (int y = 0; y < (int)size.Height; y++)
                {
                    int val = (int)((getValue((float)x * xMultipler, (float)y * yMultipler) + 1) * (255 / 2));
                    bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(val, val, val));
                }

            return bmp;
        }
    }
}
