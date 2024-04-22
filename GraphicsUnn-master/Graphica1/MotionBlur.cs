using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class MotionBlur : Filters
    {
        private int n;

        public MotionBlur(int n)
        {
            this.n = n;
        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resultR = 0;
            int resultG = 0;
            int resultB = 0;

            int radius = n / 2;

            for (int l = -radius; l <= radius; l++)
            {
                int idx = Clamp(x + l, 0, sourceImage.Width - 1);
                resultR += sourceImage.GetPixel(idx, y).R;
                resultG += sourceImage.GetPixel(idx, y).G;
                resultB += sourceImage.GetPixel(idx, y).B;
            }

            resultR /= n;
            resultG /= n;
            resultB /= n;

            return Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));
        }
    }
}
