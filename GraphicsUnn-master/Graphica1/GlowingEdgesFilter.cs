using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class GlowingEdgesFilter : Filters
    {
        MedianFilter medianFilter = new MedianFilter(2);
        SobelFilter sobelFilter = new SobelFilter();
        MaximumFilter maximumFilter = new MaximumFilter();

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color medianColor = medianFilter.calculateNewPixelColor(sourceImage, x, y);
            Color edgeColor = sobelFilter.calculateNewPixelColor(sourceImage, x, y);
            Color maxColor = maximumFilter.calculateNewPixelColor(sourceImage, x, y);

            int resultR = Clamp(medianColor.R + edgeColor.R + maxColor.R, 0, 255);
            int resultG = Clamp(medianColor.G + edgeColor.G + maxColor.G, 0, 255);
            int resultB = Clamp(medianColor.B + edgeColor.B + maxColor.B, 0, 255);

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
}
