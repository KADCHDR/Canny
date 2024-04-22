using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class GradFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            DilationFilter dilation = new DilationFilter();
            ErosionFilter erosion = new ErosionFilter();

            Color dilatedColor = dilation.calculateNewPixelColor(sourceImage, x, y);
            Color erodedColor = erosion.calculateNewPixelColor(sourceImage, x, y);
            Color gradColor = Color.FromArgb(
                erodedColor.R - dilatedColor.R,
                erodedColor.R- dilatedColor.G,
                erodedColor.R - dilatedColor.B
            ) ;

            return gradColor;
        }
    }
}
