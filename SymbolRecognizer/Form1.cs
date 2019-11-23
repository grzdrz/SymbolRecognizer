using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SymbolRecognizer
{
    public partial class Form1 : Form
    {
        public ScaleHandler scaleHandler;
        public BitmapAsArrayOfColors currentDrawnBitmapAsArrayOfColors;

        public int width;
        public int height;

        public Graphics canvas;
        public Bitmap bitmapOfPictureBox1;
        public Bitmap bitmapOfPictureBox2;

        public List<SymbolContainer> listForCompare = new List<SymbolContainer>();

        public Form1()
        {
            InitializeComponent();

            width = pictureBox1.Width;
            height = pictureBox1.Height;
            scaleHandler = new ScaleHandler(width, height);
            currentDrawnBitmapAsArrayOfColors = new BitmapAsArrayOfColors(width, height);
            scaleHandler.bitmapAsArrayOfColors = currentDrawnBitmapAsArrayOfColors;

            //битмап для рисунка
            bitmapOfPictureBox1 = new Bitmap(width, height);
            //привязываем битмап к левому пикчербоксу
            pictureBox1.Image = bitmapOfPictureBox1;
            //привязываем битмап к холсту
            canvas = Graphics.FromImage(bitmapOfPictureBox1);
            canvas.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            //Рисование на холсте(левый пикчербокс) черным цветом
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.MouseMove += pictureBox1_MouseMove;

            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;

            textBox2.KeyUp += textBox2_KeyUp;
        }

        public void pictureBox1_MouseClick(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                canvas.FillEllipse(Brushes.Black, args.X - 25, args.Y - 25, 50, 50);
            }
            if (args.Button == MouseButtons.Right)
            {
                canvas.FillEllipse(Brushes.White, args.X - 25, args.Y - 25, 50, 50);
            }
            pictureBox1.Image = bitmapOfPictureBox1;
        }

        public void pictureBox1_MouseMove(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                canvas.FillEllipse(Brushes.Black, args.X - 25, args.Y - 25, 50, 50);
            }
            if (args.Button == MouseButtons.Right)
            {
                canvas.FillEllipse(Brushes.White, args.X - 25, args.Y - 25, 50, 50);
            }
            pictureBox1.Image = bitmapOfPictureBox1;
        }

        //Begin Scale
        public void button1_Click(object sender, EventArgs args)
        {
            //трансферим битмап в двумерный массив цветов
            currentDrawnBitmapAsArrayOfColors.BitmapToArray(bitmapOfPictureBox1);
            //масштабируем нарисованную картинку
            scaleHandler.RepeatScaling();
            //двумерный массив цветов после масштабирования превращаем обратно в битмап
            bitmapOfPictureBox2 = new Bitmap(currentDrawnBitmapAsArrayOfColors.ArrayOfColors.GetLength(0),
                                             currentDrawnBitmapAsArrayOfColors.ArrayOfColors.GetLength(1));
            
            listForCompare.Add(new SymbolContainer());
            listForCompare[listForCompare.Count - 1].arrayOfColors = new Color[bitmapOfPictureBox2.Width, bitmapOfPictureBox2.Height];
            for (int i = 0; i < currentDrawnBitmapAsArrayOfColors.ArrayOfColors.GetLength(0); i++)//x 400
                for (int j = 0; j < currentDrawnBitmapAsArrayOfColors.ArrayOfColors.GetLength(1); j++)//y 560
                {
                    bitmapOfPictureBox2.SetPixel(i, j, currentDrawnBitmapAsArrayOfColors.ArrayOfColors[i, j]);
                    listForCompare[listForCompare.Count - 1].arrayOfColors[i, j] = bitmapOfPictureBox2.GetPixel(i, j);
                }
            //выводим в левом окне результат масштабирования
            pictureBox2.Image = bitmapOfPictureBox2;

            button2.Enabled = true;
            button1.Enabled = false;
        }

        //Analyze
        public void button2_Click(object sender, EventArgs args)
        {
            textBox2.Enabled = true;
            if (listForCompare.Count > 1)
                BeginAnalyzeOfDrawingImage();
            else
                textBox2.Text = "Enter symbol name";
            button2.Enabled = false;
        }

        //Clean
        public void button3_Click(object sender, EventArgs args)
        {
            canvas.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            pictureBox2.Image = null;

            button1.Enabled = true;
            button3.Enabled = false;
        }

        //Continue
        public void button4_Click(object sender, EventArgs args)
        {
            button4.Enabled = false;
            button5.Enabled = false;
            button3.Enabled = true;
        }

        //Wrong Symbol
        public void button5_Click(object sender, EventArgs args)
        {
            var tempList = listForCompare[listForCompare.Count - 1].arrayOfColors;
            listForCompare.RemoveAt(listForCompare.Count - 1);
            listForCompare.Add(new SymbolContainer());
            listForCompare[listForCompare.Count - 1].arrayOfColors = tempList;

            textBox2.Enabled = true;
            textBox2.Text = "Enter correct symbol name";
            button4.Enabled = false;
            button5.Enabled = false;
        }

        public void textBox2_KeyUp(object sender, KeyEventArgs args)
        {
            //отвечает за считывание и обработку ввода в текстбокс
            if (args.KeyData == Keys.Enter)
            {
                listForCompare[listForCompare.Count - 1].symbolName = textBox2.Text;
                textBox1.Text += "Unidentified symbolName : " + listForCompare[listForCompare.Count - 1].symbolName + Environment.NewLine;
                textBox2.Text = "";

                textBox2.Enabled = false;
                button3.Enabled = true;
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
        }

        public void BeginAnalyzeOfDrawingImage()//Сравнение рабочего массива со всеми звениями
        {
            int counterOfCollisionsWithCurrentImage = 0;
            int intersectingOfBlackPixels = 0;//Количество общих ч.пикселей
            int summOfBlackPixels = 0;//Суммарное количество ч.пикселей 
            double pixelMatchPercentage = 0;//Процент общих пикселей относительно суммарного значения
            double biggestPixelMatchPercentage = 0;
            string containedSymbol = "";

            for (int number = 0; number < listForCompare.Count - 1; number++)//Проход до предпоследнего звена
            {
                intersectingOfBlackPixels = 0;
                summOfBlackPixels = 0;
                pixelMatchPercentage = 0;

                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                    {
                        if (ColorsComparer.CompareColors(listForCompare[number].arrayOfColors[i, j], Color.Black) ||
                            ColorsComparer.CompareColors(currentDrawnBitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                            summOfBlackPixels++;
                        if (ColorsComparer.CompareColors(listForCompare[number].arrayOfColors[i, j], Color.Black) &&
                            ColorsComparer.CompareColors(currentDrawnBitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                            intersectingOfBlackPixels++;
                    }

                pixelMatchPercentage = (double)(intersectingOfBlackPixels * 100) / (double)summOfBlackPixels;

                if (pixelMatchPercentage >= 80)//Выявление звений совпавших с рабочим массивом на >=80% 
                {
                    if (biggestPixelMatchPercentage <= pixelMatchPercentage)
                    {
                        biggestPixelMatchPercentage = pixelMatchPercentage;//Счетчик самого большого % из всех, которые >=80%
                        counterOfCollisionsWithCurrentImage++;//Счетчик попаданий >80%
                        containedSymbol = listForCompare[number].symbolName;//запоминает название символа звена совпавшего на 80% и более с последним звеном
                    }
                }
            }

            //Если нет ни одного совпадения >=80%, то имя вводится в ручную(вынесено в ивент текстбокса)
            if (counterOfCollisionsWithCurrentImage == 0)
                textBox2.Text = "Enter symbol name";
            //Если есть хотябы 1 совпадение, то имя вводится автоматически
            if (counterOfCollisionsWithCurrentImage != 0)
            {
                //заносим в последнее звено название симола звена с которым совпало на 80% и более действующее
                listForCompare[listForCompare.Count - 1].symbolName = containedSymbol;
                textBox1.Text += "This is " + listForCompare[listForCompare.Count - 1].symbolName + " : " + Math.Round(biggestPixelMatchPercentage, 4) + "%" + Environment.NewLine;

                textBox2.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
            }
        }
    }

    //public static class ColorCode
    //{
    //    public static long GetColorCode(Color color)//цвета из разных источников не проверяются через оператор ==, поэтому нужен какойто код для проверки
    //    {
    //        return (long)(color.R * Math.Pow(10.0, 6.0) + color.B * Math.Pow(10.0, 3.0) + color.G);
    //    }
    //}
}