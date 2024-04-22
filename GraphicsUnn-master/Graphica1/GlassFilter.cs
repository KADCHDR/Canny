using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class GlassFilter : Filters
    {
        private Random rand;

        public GlassFilter()
        {
            rand = new Random();
        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int newX = Clamp(x + (int)((rand.NextDouble() - 0.5) * 10), 0, sourceImage.Width - 1);
            int newY = Clamp(y + (int)((rand.NextDouble() - 0.5) * 10), 0, sourceImage.Height - 1);

            return sourceImage.GetPixel(newX, newY);
        }
    }
}
