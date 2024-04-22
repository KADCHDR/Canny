using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class MedianFilter : Filters
    {
        private int radius;

        public MedianFilter(int radius)
        {
            this.radius = radius;
        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int[] r = new int[(2 * radius + 1) * (2 * radius + 1)];
            int[] g = new int[r.Length];
            int[] b = new int[r.Length];

            int n = 0;
            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    int idX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + j, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    r[n] = neighborColor.R;
                    g[n] = neighborColor.G;
                    b[n] = neighborColor.B;
                    n++;
                }
            }

            Array.Sort(r);
            Array.Sort(g);
            Array.Sort(b);
            return Color.FromArgb(r[r.Length / 2], g[g.Length / 2], b[b.Length / 2]);
        }
    }
}
