using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class SobelFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resultX = 0;
            int resultY = 0;
            int radius = 1;

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    int intensity = (int)(0.299 * neighborColor.R + 0.587 * neighborColor.G + 0.114 * neighborColor.B);
                    resultX += intensity * SobelMatrixX[k + radius, l + radius];
                    resultY += intensity * SobelMatrixY[k + radius, l + radius];
                }
            }

            int result = Clamp((int)(Math.Sqrt(resultX * resultX + resultY * resultY)), 0, 255);

            return Color.FromArgb(result, result, result);
        }

        private readonly int[,] SobelMatrixX = new int[,]
        {
        { -1, 0, 1 },
        { -2, 0, 2 },
        { -1, 0, 1 }
        };

        private readonly int[,] SobelMatrixY = new int[,]
        {
        { -1, -2, -1 },
        { 0, 0, 0 },
        { 1, 2, 1 }
        };
    }
}
