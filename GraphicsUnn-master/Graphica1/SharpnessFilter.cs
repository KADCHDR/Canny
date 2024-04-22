using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class SharpnessFilter : Filters
    {
        private int[,] Kernel = new int[,] {
        { 0, -1, 0 },
        { -1, 5, -1 },
        { 0, -1, 0 }
    };

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resultR = 0;
            int resultG = 0;
            int resultB = 0;
            int radius = 1;

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    resultR += neighborColor.R * Kernel[k + radius, l + radius];
                    resultG += neighborColor.G * Kernel[k + radius, l + radius];
                    resultB += neighborColor.B * Kernel[k + radius, l + radius];
                }
            }

            resultR = Clamp(resultR, 0, 255);
            resultG = Clamp(resultG, 0, 255);
            resultB = Clamp(resultB, 0, 255);

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
}
