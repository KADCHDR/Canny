using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class Brightness : Filters
    {
        private int brightness = 30; // Константа для увеличения яркости

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            int R = Clamp(sourceColor.R + brightness, 0, 255);
            int G = Clamp(sourceColor.G + brightness, 0, 255);
            int B = Clamp(sourceColor.B + brightness, 0, 255);

            Color resultColor = Color.FromArgb(R, G, B);

            return resultColor;
        }
    }
}
