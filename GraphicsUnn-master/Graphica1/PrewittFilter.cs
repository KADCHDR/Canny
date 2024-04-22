using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class PrewittFilter : Filters
    {
        private int[,] kernelX = new int[,] {
        { -1, 0, 1 },
        { -1, 0, 1 },
        { -1, 0, 1 }
    };

        private int[,] kernelY = new int[,] {
        { -1, -1, -1 },
        { 0, 0, 0 },
        { 1, 1, 1 }
    };

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernelX.GetLength(0) / 2;
            int radiusY = kernelY.GetLength(1) / 2;
            float gx = 0;
            float gy = 0;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    gx += neighborColor.R * kernelX[k + radiusX, l + radiusY];
                    gy += neighborColor.R * kernelY[k + radiusX, l + radiusY];
                }
            }

            int gradient = (int)Math.Sqrt(gx * gx + gy * gy);
            return Color.FromArgb(
                Clamp(gradient, 0, 255),
                Clamp(gradient, 0, 255),
                Clamp(gradient, 0, 255));
        }
    }
}
