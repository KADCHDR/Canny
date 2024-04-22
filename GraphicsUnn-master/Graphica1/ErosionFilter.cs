using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    internal class ErosionFilter:Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radius = 1;
            int maxR = 0;
            Color resultColor = Color.Black;

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    int neighborX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int neighborY = Clamp(y + l, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(neighborX, neighborY);
                    int intensity = (int)(0.299 * neighborColor.R + 0.587 * neighborColor.G + 0.114 * neighborColor.B);

                    if (intensity > maxR)
                    {
                        maxR = intensity;
                        resultColor = Color.FromArgb(maxR, maxR, maxR);
                    }
                }
            }

            return resultColor;
        }
    }
}
