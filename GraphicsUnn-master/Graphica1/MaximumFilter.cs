using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class MaximumFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int maxR = 0;
            int maxG = 0;
            int maxB = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int idX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + j, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    if (neighborColor.R > maxR)
                        maxR = neighborColor.R;
                    if (neighborColor.G > maxG)
                        maxG = neighborColor.G;
                    if (neighborColor.B > maxB)
                        maxB = neighborColor.B;
                }
            }

            return Color.FromArgb(Clamp(maxR, 0, 255), Clamp(maxG, 0, 255), Clamp(maxB, 0, 255));
        }
    }
}
