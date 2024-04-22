using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class ClosingFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            ErosionFilter erosion = new ErosionFilter();
            DilationFilter dilation = new DilationFilter();

            Color closedColor = dilation.calculateNewPixelColor(sourceImage, x, y);

            return closedColor;
        }
    }
}
