using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    internal class SepiaFilter : Filters
    {
        const double k = 30;

        public override Color calculateNewPixelColor(Bitmap img, int x, int y)
        {
            Color pixel = img.GetPixel(x, y);

            double intensity = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
            double newR = intensity + 2 * k;
            double newG = intensity + 0.5 * k;
            double newB = intensity - k;

            Color newPixel = Color.FromArgb(
                Clamp((int)newR),
                Clamp((int)newG),
                Clamp((int)newB)
            );

            return newPixel;
        }

        public int Clamp(int value, int min = 0, int max = 255)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
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
