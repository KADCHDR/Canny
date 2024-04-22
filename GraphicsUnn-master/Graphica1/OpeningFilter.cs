using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class OpeningFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            DilationFilter dilation = new DilationFilter();
            ErosionFilter erosion = new ErosionFilter();

            Color dilatedColor = dilation.calculateNewPixelColor(sourceImage, x, y);
            Color openedColor = erosion.calculateNewPixelColor(sourceImage, x, y);

            return openedColor;
        }
    }
}
