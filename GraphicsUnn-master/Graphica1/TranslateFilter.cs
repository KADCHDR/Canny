using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    using System;
    using System.Drawing;

    class TranslateFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int newX = x + 50;
            int newY = y;

            newX = Clamp(newX, 0, sourceImage.Width - 1);
            newY = Clamp(newY, 0, sourceImage.Height - 1);

            return sourceImage.GetPixel(newX, newY);
        }
    }
}
