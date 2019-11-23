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
        public TransformSize transformSize;
        public BitmapAsArrayOfColors bitmapAsArrayOfColors;

        public int width;
        public int height;

        public Graphics graphic1;
        //public Graphics graphic2;
        public Bitmap bitmapLayerOfPictureBox1;
        public Bitmap bitmapLayerOfPictureBox2;

        public List<ContainerOfArrayAndSymbol> listForCompare = new List<ContainerOfArrayAndSymbol>();

        public Form1()
        {
            InitializeComponent();

            width = pictureBox1.Width;
            height = pictureBox1.Height;
            transformSize = new TransformSize(width, height);
            bitmapAsArrayOfColors = new BitmapAsArrayOfColors(width, height);
            transformSize.bitmapAsArrayOfColors = bitmapAsArrayOfColors;

            //создаем рабочую битмап картинкуы
            bitmapLayerOfPictureBox1 = new Bitmap(width, height);
            //связываем пикчербокс с битмап картинкой
            pictureBox1.Image = bitmapLayerOfPictureBox1;
            //связываем график холст с битмап картинкой
            graphic1 = Graphics.FromImage(bitmapLayerOfPictureBox1);
            graphic1.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            //Рисование
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.MouseMove += pictureBox1_MouseMove;

            button1.Click += button1_Click;

            button2.Click += button2_Click;

            button3.Click += button3_Click;

            textBox2.KeyUp += textBox1_KeyUp;
        }

        public void pictureBox1_MouseClick(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                graphic1.FillEllipse(Brushes.Black, args.X - 25, args.Y - 25, 50, 50);
            }
            if (args.Button == MouseButtons.Right)
            {
                graphic1.FillEllipse(Brushes.White, args.X - 25, args.Y - 25, 50, 50);
            }
            pictureBox1.Image = bitmapLayerOfPictureBox1;
        }

        public void pictureBox1_MouseMove(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                graphic1.FillEllipse(Brushes.Black, args.X - 25, args.Y - 25, 50, 50);
            }
            if (args.Button == MouseButtons.Right)
            {
                graphic1.FillEllipse(Brushes.White, args.X - 25, args.Y - 25, 50, 50);
            }
            pictureBox1.Image = bitmapLayerOfPictureBox1;
        }

        public void button1_Click(object sender, EventArgs args)
        {
            //сэйвим нарисованное на холсте, связанным с битмапом, по данным координатам в данном формате
            //((Image)bitmapLayerOfPictureBox1).Save(@"C:\Users\user\Desktop\drawingProg\TESTIMAGE1.png", System.Drawing.Imaging.ImageFormat.Png);
            //трансферим битмап в двумерный массив цветов
            bitmapAsArrayOfColors.BitmapToArray(bitmapLayerOfPictureBox1);
            //запускаем обработку-масштабирование
            transformSize.Chislo_povtorenii();
            //двумерный массив цветов после обработки превращаем обратно в битмап
            bitmapLayerOfPictureBox2 = new Bitmap(bitmapAsArrayOfColors.ArrayOfColors.GetLength(0), bitmapAsArrayOfColors.ArrayOfColors.GetLength(1));
            //создаем звено коллекции рисунков
            listForCompare.Add(new ContainerOfArrayAndSymbol());
            listForCompare[listForCompare.Count - 1].arrayOfColor = new Color[bitmapLayerOfPictureBox2.Width, bitmapLayerOfPictureBox2.Height];
            for (int i = 0; i < bitmapAsArrayOfColors.ArrayOfColors.GetLength(0); i++)//x 400
                for (int j = 0; j < bitmapAsArrayOfColors.ArrayOfColors.GetLength(1); j++)//y 560
                {
                    bitmapLayerOfPictureBox2.SetPixel(i, j, bitmapAsArrayOfColors.ArrayOfColors[i, j]);
                    listForCompare[listForCompare.Count - 1].arrayOfColor[i, j] = bitmapLayerOfPictureBox2.GetPixel(i, j);
                }
            //выводим в левом окне результат обработки
            pictureBox2.Image = bitmapLayerOfPictureBox2;

            button2.Enabled = true;
            button1.Enabled = false;
        }

        public void button2_Click(object sender, EventArgs args)
        {
            textBox2.Enabled = true;
            if (listForCompare.Count > 1)
                Sravnenie();
            else
                textBox2.Text = "Input symbol name";
            button2.Enabled = false;
        }

        public void button3_Click(object sender, EventArgs args)
        {
            graphic1.FillRectangle(Brushes.White, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            pictureBox2.Image = null;

            button1.Enabled = true;
            button3.Enabled = false;
        }

        public void textBox1_KeyUp(object sender, KeyEventArgs args)
        {
            //отвечает за считывание и обработку ввода в текстбокс
            if (args.KeyData == Keys.Enter)
            {
                listForCompare[listForCompare.Count - 1].symbol = textBox2.Text;
                textBox1.Text += "Unidentified symbol : " + listForCompare[listForCompare.Count - 1].symbol + Environment.NewLine;

                textBox2.Text = "";
                textBox2.Enabled = false;
                button3.Enabled = true;
                button2.Enabled = false;
            }
        }

        public void Sravnenie()//Сравнение рабочего массива со всеми звениями
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
                        if (ColorsComparer.CompareColors(listForCompare[number].arrayOfColor[i, j], Color.Black) ||
                            ColorsComparer.CompareColors(bitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                            summOfBlackPixels++;
                        if (ColorsComparer.CompareColors(listForCompare[number].arrayOfColor[i, j], Color.Black) &&
                            ColorsComparer.CompareColors(bitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                            intersectingOfBlackPixels++;
                    }

                pixelMatchPercentage = (double)(intersectingOfBlackPixels * 100) / (double)summOfBlackPixels;

                if (pixelMatchPercentage >= 80)//Выявление звений совпавших с рабочим массивом на >=80% 
                {
                    if (biggestPixelMatchPercentage <= pixelMatchPercentage)
                    {
                        biggestPixelMatchPercentage = pixelMatchPercentage;//Счетчик самого большого % из всех, которые >=80%
                        counterOfCollisionsWithCurrentImage++;//Счетчик попаданий >80%
                        containedSymbol = listForCompare[number].symbol;//запоминает название символа звена совпавшего на 80% и более с последним звеном
                    }
                }
            }

            //Если нет ни одного совпадения >=80%, то имя вводится в ручную(вынесено в ивент текстбокса)
            if (counterOfCollisionsWithCurrentImage == 0)
                textBox2.Text = "Input symbol name";
            //Если есть хотябы 1 совпадение, то имя вводится автоматически
            if (counterOfCollisionsWithCurrentImage != 0)
            {
                //заносим в последнее звено название симола звена с которым совпало на 80% и более действующее
                listForCompare[listForCompare.Count - 1].symbol = containedSymbol;
                textBox1.Text += "This is " + listForCompare[listForCompare.Count - 1].symbol + " : " + biggestPixelMatchPercentage + "%" + Environment.NewLine;

                textBox2.Enabled = false;
                button3.Enabled = true;
                button2.Enabled = false;
            }
        }

        public bool MouseInWorkSpace(MouseEventArgs args)
        {
            return args.X > Width / 2 - width / 2
                && args.Y > Height / 2 - height / 2
                && args.X < Width / 2 + width / 2
                && args.Y < Height / 2 + height / 2;
        }
    }

    public static class ColorCode
    {
        public static long GetColorCode(Color color)//цвета из разных источников не проверяются через оператор ==, поэтому нужен какойто код для проверки
        {
            return (long)(color.R * Math.Pow(10.0, 6.0) + color.B * Math.Pow(10.0, 3.0) + color.G);
        }
    }
}
//дописать обработку ошибочного автоопределения символа
//(если программа неправильно определила символ, то нужно вручную ввести верный)