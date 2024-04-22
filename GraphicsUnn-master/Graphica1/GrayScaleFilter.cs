using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    internal class GrayScaleFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {

            Color pixel = sourceImage.GetPixel(x, y);

            int grayValue = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
            Color newPixel = Color.FromArgb(grayValue, grayValue, grayValue);

            return newPixel;
        }

        public Bitmap ApplyFilter(Bitmap sourceImage)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap resultImage = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color newPixel = calculateNewPixelColor(sourceImage, i, j);
                    resultImage.SetPixel(i, j, newPixel);
                }
            }
            return resultImage;
        }
    }
}
