using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphica1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog= new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK) { 
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Изображения|*.png;*.bmp;*.jpg";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog.FileName);
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value=e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled) {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void гауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void полутонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Brightness();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void собеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void повышениеРезкостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new EmbossFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TranslateFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void повротToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int centerX = 550;
            int centerY = 350;
            float rotationAngle = 90;
            Filters filter = new RotateFilter(centerX, centerY, rotationAngle);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волныToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void волны1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new WavesFilter1();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волны2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new WavesFilter2();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void эффектСтеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = 10;
            Filters filter = new MotionBlur(n);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void операторЩарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ScharrFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void операторВьюиттаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PrewittFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void светящиесяКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlowingEdgesFilter glowEdgesFilter = new GlowingEdgesFilter();
            backgroundWorker1.RunWorkerAsync(glowEdgesFilter);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DilationFilter dilationFilter = new DilationFilter();
            backgroundWorker1.RunWorkerAsync(dilationFilter);
            //Bitmap resultImage = dilationFilter.processImage(sourceImage, backgroundWorker);
            // Дальнейшие действия с результатом

        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErosionFilter erosionFilter = new ErosionFilter();
            backgroundWorker1.RunWorkerAsync(erosionFilter);
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpeningFilter openingFilter = new OpeningFilter();
            backgroundWorker1.RunWorkerAsync(openingFilter);
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClosingFilter closingFilter = new ClosingFilter();
            backgroundWorker1.RunWorkerAsync(closingFilter);
        }

        private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopHatFilter tophatFilter = new TopHatFilter();
            backgroundWorker1.RunWorkerAsync(tophatFilter);
        }

        private void blackHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlackHatFilter blackhatFilter = new BlackHatFilter();
            backgroundWorker1.RunWorkerAsync(blackhatFilter);
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GradFilter gradFilter = new GradFilter();
            backgroundWorker1.RunWorkerAsync(gradFilter);
        }

        private void кэнниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Предполагается, что у вас есть PictureBox, который отображает изображение
            // И у вас есть переменная sourceImage, которая содержит исходное изображение

            // Значения порогов для фильтра Кэнни
            double lowThreshold = 50;
            double highThreshold = 150;

            // Получаем исходное изображение из PictureBox
            Bitmap sourceImage = (Bitmap)pictureBox1.Image;

            // Создаем экземпляр фильтра Кэнни
            CannyEdgeFilter cannyFilter = new CannyEdgeFilter(lowThreshold, highThreshold);

            // Инициализируем BackgroundWorker
            BackgroundWorker worker = new BackgroundWorker();

            // Применяем фильтр к изображению
            Bitmap filteredImage = cannyFilter.processImage(sourceImage, worker);

            // Обновляем изображение в PictureBox
            pictureBox1.Image = filteredImage;
        }
    }
}
