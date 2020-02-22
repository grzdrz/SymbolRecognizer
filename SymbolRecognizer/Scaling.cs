using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SymbolRecognizer
{
    public class Scaling
    {
        public static Bitmap ApplyScaling(Color[,] pixels, int newWidth, int newHeight)
        {
            //Аргументы для процедуры вычисления координат крайних пикселей изображения
            int mostRight = 0;
            int mostDown = 0;
            int mostLeft = pixels.GetLength(0);
            int mostTop = pixels.GetLength(1);

            //Поиск координат левого верхнего угла
            for (int i = 0; i < pixels.GetLength(0); i++)
                for (int j = 0; j < pixels.GetLength(1); j++)
                    if (ColorsComparer.IsEqual(pixels[i, j], Color.Black))
                    {
                        if (i <= mostLeft) mostLeft = i;//Отсеивание самого левого пикселя
                        if (j <= mostTop) mostTop = j;//Отсеивание самого верхнего пикселя
                        if (i >= mostRight) mostRight = i;//Отсеивание самого правого пикселя
                        if (j >= mostDown) mostDown = j;//Отсеивание самого нижнего пикселя
                    }

            var describingRectangleWidth = mostRight - mostLeft;
            var describingRectangleHeight = mostDown - mostTop;
            Bitmap temp1 = new Bitmap(describingRectangleWidth, describingRectangleHeight);
            int x = 0; int y = 0;
            for (int i = 0; i < describingRectangleWidth; i++)
            {
                for (int j = 0; j < describingRectangleHeight; j++)
                {
                    temp1.SetPixel(i, j, pixels[mostLeft + i, mostTop + j]);
                    y++;
                }
                x++;
                y = 0;
            }

            return new Bitmap(temp1, newWidth, newHeight);
        }   
    }
}
