using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class WavesFilter2 : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {

            // Для волн 2:
            int k = Clamp(x + (int)(20 * Math.Sin(2 * Math.PI * x / 30)), 0, sourceImage.Width - 1);
            int l = y;

            return sourceImage.GetPixel(k, l);
        }
    }
}
