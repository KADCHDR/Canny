using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class TopHatFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            OpeningFilter opening = new OpeningFilter();

            Color openedColor = opening.calculateNewPixelColor(sourceImage, x, y);
            Color topHatColor = Color.FromArgb(
                Math.Abs(sourceImage.GetPixel(x, y).R - openedColor.R),
                Math.Abs(sourceImage.GetPixel(x, y).G - openedColor.G),
                Math.Abs(sourceImage.GetPixel(x, y).B - openedColor.B)
            );

            return topHatColor;
        }
    }
}
