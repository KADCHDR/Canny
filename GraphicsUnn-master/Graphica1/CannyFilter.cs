using System;
using System.Drawing;
using System.ComponentModel;

abstract class Filters
{
    public abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

    public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
    {
        Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
        worker.WorkerReportsProgress = true; // Установка WorkerReportsProgress в true

        for (int i = 0; i < sourceImage.Width; i++)
        {
            worker.ReportProgress((int)((float)i / resultImage.Width * 100));
            if (worker.CancellationPending)
                return null;
            for (int j = 0; j < sourceImage.Height; j++)
            {
                resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
            }
        }
        return resultImage;
    }

    public int Clamp(int value, int min, int max)
    {
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }
}

class CannyEdgeFilter : Filters
{
    public double lowThreshold;
    public double highThreshold;

    public CannyEdgeFilter(double lowThreshold, double highThreshold)
    {
        this.lowThreshold = lowThreshold;
        this.highThreshold = highThreshold;
    }

    public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
        // Преобразование в оттенки серого
        int intensity = (int)(sourceImage.GetPixel(x, y).R * 0.3 +
                              sourceImage.GetPixel(x, y).G * 0.59 +
                              sourceImage.GetPixel(x, y).B * 0.11);

        // Применение фильтра Собеля для обнаружения границ
        int gx = ApplySobelX(sourceImage, x, y);
        int gy = ApplySobelY(sourceImage, x, y);

        // Вычисление величины градиента и направления
        int gradientMagnitude = (int)Math.Sqrt(gx * gx + gy * gy);
        double gradientDirection = Math.Atan2(gy, gx) * (180.0 / Math.PI);

        // Подавление немаксимумов
        if (x > 0 && x < sourceImage.Width - 1 && y > 0 && y < sourceImage.Height - 1)
        {
            // Определение направления для сравнения с текущим направлением градиента
            int[] offsets = { 0, 45, 90, 135 };
            bool isMax = true;
            foreach (int offset in offsets)
            {
                int neighborX = x + (int)Math.Round(Math.Cos(offset * Math.PI / 180.0));
                int neighborY = y + (int)Math.Round(Math.Sin(offset * Math.PI / 180.0));
                int neighborIntensity = (int)(sourceImage.GetPixel(neighborX, neighborY).R * 0.3 +
                                              sourceImage.GetPixel(neighborX, neighborY).G * 0.59 +
                                              sourceImage.GetPixel(neighborX, neighborY).B * 0.11);
                if (neighborIntensity > intensity)
                {
                    isMax = false;
                    break;
                }
            }

            if (!isMax)
            {
                gradientMagnitude = 0; // Не максимум, поэтому не граница
            }
        }

        // Двойной пороговый анализ
        if (gradientMagnitude < lowThreshold)
        {
            return Color.Black; // Незначительная граница
        }
        else if (gradientMagnitude >= highThreshold)
        {
            return Color.White; // Сильная граница
        }
        else
        {
            // Промежуточный порог, может быть слабой или удаленной границей
            // Для связывания слабых границ с сильными можно использовать алгоритмы связывания,
            // такие как обратный поиск, анализ соседних пикселей и т.д.
            // В этом примере мы просто оставляем пиксель в сером цвете
            return Color.FromArgb(intensity, intensity, intensity);
        }
    }

    private int ApplySobelX(Bitmap sourceImage, int x, int y)
    {
        // Применение матрицы Собеля по X
        int[,] sobelX = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        int result = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborX = Clamp(x + i, 0, sourceImage.Width - 1);
                int neighborY = Clamp(y + j, 0, sourceImage.Height - 1);
                int intensity = (int)(sourceImage.GetPixel(neighborX, neighborY).R * 0.3 +
                                      sourceImage.GetPixel(neighborX, neighborY).G * 0.59 +
                                      sourceImage.GetPixel(neighborX, neighborY).B * 0.11);
                result += sobelX[i + 1, j + 1] * intensity;
            }
        }
        return result;
    }

    private int ApplySobelY(Bitmap sourceImage, int x, int y)
    {
        // Применение матрицы Собеля по Y
        int[,] sobelY = new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
        int result = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborX = Clamp(x + i, 0, sourceImage.Width - 1);
                int neighborY = Clamp(y + j, 0, sourceImage.Height - 1);
                int intensity = (int)(sourceImage.GetPixel(neighborX, neighborY).R * 0.3 +
                                      sourceImage.GetPixel(neighborX, neighborY).G * 0.59 +
                                      sourceImage.GetPixel(neighborX, neighborY).B * 0.11);
                result += sobelY[i + 1, j + 1] * intensity;
            }
        }
        return result;
    }
}