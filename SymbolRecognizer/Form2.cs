using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SymbolRecognizer
{
    public partial class Form2 : Form
    {
        Form1 MainForm;

        public Form2(Form1 mainForm)
        {
            MainForm = mainForm;

            InitializeComponent();
            button3_EnterText.Click += button3_Click_EnterText;
            button2_WrongSymbol.Click +=button2_WrongSymbol_Click;
            button1_Approve.Click += button1_Approve_Click;

            IdentifyImage();
        }

        public void IdentifyImage()
        {
            //если список установленных символов пустой
            if (MainForm.IdentifiedImages.Count == 0)
            {
                textBox2.Enabled = true;
                button3_EnterText.Enabled = true;
                textBox2.Text = "No identified symbols, enter symbol name";
                return;
            }

            // Счетчик совпадений >80%
            int countOfMatchesWithCurrentImage = 0;
            // Процентное отношение количества общих черных пикселей к количеству всех черных пикселей
            double pixelMatchPercentage = 0;
            // Наибольший % из всех совпадений, которые >=80%
            double biggestPixelMatchPercentage = 0;
            // название символа с наибольший % из всех совпадений
            string tempSymbolName = "";

            for (int i = 0; i < MainForm.IdentifiedImages.Count; i++)
            {
                pixelMatchPercentage = CalculatePixelMatchPercentage(MainForm.IdentifiedImages[i].Bitmap, MainForm.bitmap2Drawn);
                textBox1_DataOfCompare.Text += String.Format(
                    "Совпадает на {0}% с изображением {1}", pixelMatchPercentage, MainForm.IdentifiedImages[i].symbolName);
                textBox1_DataOfCompare.Text += Environment.NewLine;

                if (pixelMatchPercentage >= 80) 
                {
                    if (biggestPixelMatchPercentage <= pixelMatchPercentage)
                    {
                        countOfMatchesWithCurrentImage++;
                        biggestPixelMatchPercentage = pixelMatchPercentage;
                        tempSymbolName = MainForm.IdentifiedImages[i].symbolName;
                    }
                }
            }

            //Если нет ни одного совпадения >=80%, то имя вводится в ручную
            if (countOfMatchesWithCurrentImage == 0)
            {
                textBox2.Enabled = true;
                button3_EnterText.Enabled = true;
                textBox2.Text = "Symbol not recognized, enter symbol name";
            }
            //Если есть хотябы 1 совпадение, то имя вводится автоматически
            else
            {
                textBox2.Enabled = false;
                button3_EnterText.Enabled = false;

                textBox1_DataOfCompare.Text += Environment.NewLine;
                textBox1_DataOfCompare.Text += String.Format(
                    "Символ на указанном изображении это предположительно - {0}", tempSymbolName);
                textBox1_DataOfCompare.Text += Environment.NewLine;
                textBox2.Text = tempSymbolName;

                button1_Approve.Enabled = true;
                button2_WrongSymbol.Enabled = true;
            }
        }

        public double CalculatePixelMatchPercentage(Bitmap bitmap1, Bitmap bitmap2)
        {
            double summOfBlackPixels = 0;//Количество всех черных пикселей 
            double intersectingOfBlackPixels = 0;//Количество общих черных пикселей

            for (int i = 0; i < MainForm.WidthOfScaledImage; i++)
                for (int j = 0; j < MainForm.HeightOfScaledImage; j++)
                {
                    if (ColorsComparer.IsEqual(bitmap1.GetPixel(i, j), Color.Black) ||
                        ColorsComparer.IsEqual(bitmap2.GetPixel(i, j), Color.Black))
                        summOfBlackPixels++;
                    if (ColorsComparer.IsEqual(bitmap1.GetPixel(i, j), Color.Black) &&
                        ColorsComparer.IsEqual(bitmap2.GetPixel(i, j), Color.Black))
                        intersectingOfBlackPixels++;
                }

            return Math.Round((intersectingOfBlackPixels * 100d) / summOfBlackPixels, 5);
        }

        private void button3_Click_EnterText(object sender, EventArgs e)
        {
            var text = textBox2.Text;
            string pattern = "([a-z]|[0-9]){1}";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (regex.IsMatch(text) && text.Length == 1)
            {
                textBox2.Enabled = false;
                button3_EnterText.Enabled = false;

                textBox1_DataOfCompare.Text += Environment.NewLine;
                textBox1_DataOfCompare.Text += String.Format(
                    "Символ на указанном изображении определен вручную как - {0}", textBox2.Text);
                textBox1_DataOfCompare.Text += Environment.NewLine;

                button1_Approve.Enabled = true;
                button2_WrongSymbol.Enabled = true;
            }
            else 
            {
                textBox2.Text = "Entered invalid symbol";
            }
        }

        //на случай если символ был определен неправильно
        private void button2_WrongSymbol_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            button3_EnterText.Enabled = true;

            textBox2.Text = "Enter symbol name";

            button1_Approve.Enabled = false;
            button2_WrongSymbol.Enabled = false;
        }

        private void button1_Approve_Click(object sender, EventArgs e)
        {
            var text = textBox2.Text;
            string pattern = "([a-z]|[0-9]){1}";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (!(regex.IsMatch(text) && text.Length == 1))
                return;

            var imageData = new ImageData()
            {
                symbolName = textBox2.Text,
                Bitmap = MainForm.bitmap2Drawn
            };
            MainForm.IdentifiedImages.Add(imageData);

            MainForm.Enabled = true;
            this.Close();
        }
    }
}
