using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphica1
{
    class EmbossFilter : Filters
    {
        // Ядро фильтра для тиснения
        private int[,] Kernel = new int[,] {
        { 0, 1, 0 },
        { 1, 0, -1 },
        { 0, -1, 0 }
    };

        // Переопределение метода для вычисления нового цвета пикселя с учетом фильтра
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resultR = 0;
            int resultG = 0;
            int resultB = 0;
            int radius = 1;

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    Color neighborColor = sourceImage.GetPixel(Clamp(x + k, 0, sourceImage.Width - 1),
                                                               Clamp(y + l, 0, sourceImage.Height - 1));
                    resultR += neighborColor.R * Kernel[k + radius, l + radius];
                    resultG += neighborColor.G * Kernel[k + radius, l + radius];
                    resultB += neighborColor.B * Kernel[k + radius, l + radius];
                }
            }

            // Сдвиг по яркости
            int resultBrightness = 10;
            resultR += resultBrightness;
            resultG += resultBrightness;
            resultB += resultBrightness;

            // Нормировка значения пикселей
            int maxDensityRange = 255;
            int normalizedValue = Clamp(resultR, 0, maxDensityRange) + Clamp(resultG, 0, maxDensityRange) + Clamp(resultB, 0, maxDensityRange);
            normalizedValue += 255;
            int finalValue = Clamp(normalizedValue / 2, 0, 255);

            return Color.FromArgb(finalValue, finalValue, finalValue);
        }
    }

}
