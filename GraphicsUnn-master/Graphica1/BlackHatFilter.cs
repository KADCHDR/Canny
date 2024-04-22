using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class BlackHatFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            ClosingFilter closing = new ClosingFilter();

            Color closedColor = closing.calculateNewPixelColor(sourceImage, x, y);
            Color blackHatColor = Color.FromArgb(
                Math.Abs(closedColor.R - sourceImage.GetPixel(x, y).R),
                Math.Abs(closedColor.G - sourceImage.GetPixel(x, y).G),
                Math.Abs(closedColor.B - sourceImage.GetPixel(x, y).B)
            );

            return blackHatColor;
        }
    }
}
