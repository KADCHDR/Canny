using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class WavesFilter1 : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k = Clamp(x + (int)(20 * Math.Sin(2 * Math.PI * y / 60)), 0, sourceImage.Width - 1);
            int l = y;

            return sourceImage.GetPixel(k, l);
        }
    }
}
