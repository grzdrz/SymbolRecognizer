using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SymbolRecognizer
{
    public partial class Form1 : Form
    {
        public Scaling scaleHandler;

        public int WidthOfDrawn;
        public int HeightOfDrawn;
        public int WidthOfScaledImage = 50;
        public int HeightOfScaledImage = 100;

        public Graphics canvas;
        public Bitmap bitmap1Drawn;
        public Bitmap bitmap2Drawn;
        Color[,] pixels1Drawn;
        Color[,] pixels2Drawn;

        public List<ImageData> IdentifiedImages = new List<ImageData>();

        Form2 form2;

        public Form1()
        {
            InitializeComponent();

            WidthOfDrawn = pictureBox1.Width;
            HeightOfDrawn = pictureBox1.Height;

            //битмап для рисунка
            bitmap1Drawn = new Bitmap(WidthOfDrawn, HeightOfDrawn);
            //привязываем битмап к левому пикчербоксу
            pictureBox1.Image = bitmap1Drawn;
            //привязываем битмап к холсту
            canvas = Graphics.FromImage(bitmap1Drawn);
            canvas.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            //Рисование на холсте(левый пикчербокс) черным цветом
            pictureBox1.MouseClick += pictureBox1_MouseClick_Draw;
            pictureBox1.MouseMove += pictureBox1_MouseMove_Draw;

            button1_Scale.Click += button1_Click_Scale;
            button2_Analyze.Click += button2_Click_Analyze;
            button3_Clean.Click += button3_Click_Clean;

            button6_TESTSave.Click += button6_Click_SaveImage;///////////////
        }

        //масштабируем
        public void button1_Click_Scale(object sender, EventArgs args)
        {
            pixels1Drawn = bitmap1Drawn.BitmapToArray();

            //масштабируем нарисованную картинку
            bitmap2Drawn = Scaling.ApplyScaling(pixels1Drawn, WidthOfScaledImage, HeightOfScaledImage);
            pictureBox2.Image = bitmap2Drawn;
            pixels2Drawn = bitmap2Drawn.BitmapToArray();

            button2_Analyze.Enabled = true;
        }

        //идентифицируем символ
        public void button2_Click_Analyze(object sender, EventArgs args)
        {
            this.Enabled = false;
            form2 = new Form2(this);
            form2.Show();
        }

        //заливаем белым область рисования
        public void button3_Click_Clean(object sender, EventArgs args)
        {
            canvas.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
        }

        #region "Рисование"
        public void pictureBox1_MouseClick_Draw(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                canvas.FillEllipse(Brushes.Black, args.X - 25, args.Y - 25, 50, 50);
            }
            if (args.Button == MouseButtons.Right)
            {
                canvas.FillEllipse(Brushes.White, args.X - 25, args.Y - 25, 50, 50);
            }
            pictureBox1.Image = bitmap1Drawn;
        }

        public void pictureBox1_MouseMove_Draw(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                canvas.FillEllipse(Brushes.Black, args.X - 25, args.Y - 25, 50, 50);
            }
            if (args.Button == MouseButtons.Right)
            {
                canvas.FillEllipse(Brushes.White, args.X - 25, args.Y - 25, 50, 50);
            }
            pictureBox1.Image = bitmap1Drawn;
        }
        #endregion

        //TEST
        private void button6_Click_SaveImage(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "PNG files (*.png)|*.png";

            DialogResult showDialogResult = saveFileDialog.ShowDialog();
            if (showDialogResult == DialogResult.Cancel)
                return;
            if (showDialogResult == System.Windows.Forms.DialogResult.OK)
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    bitmap2Drawn.Save(stream, ImageFormat.Png);
                }
            MessageBox.Show("Файл сохранен");
        }
    }
}