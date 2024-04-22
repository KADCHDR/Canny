using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphica1
{
    class RotateFilter : Filters
    {
        private int x0;
        private int y0;
        private float angle;
        public RotateFilter(int centerX, int centerY, float rotationAngle)
        {
            x0 = centerX;
            y0 = centerY;
            angle = rotationAngle;
        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {

            double newX = (x - x0) * Math.Cos(angle) - (y - y0) * Math.Sin(angle) + x0;
            double newY = (x - x0) * Math.Sin(angle) + (y - y0) * Math.Cos(angle) + y0;

            int intX = (int)newX;
            int intY = (int)newY;

            intX = Clamp(intX, 0, sourceImage.Width - 1);
            intY = Clamp(intY, 0, sourceImage.Height - 1);

            return sourceImage.GetPixel(intX, intY);
        }
    }
}
