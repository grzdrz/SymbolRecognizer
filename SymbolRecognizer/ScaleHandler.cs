using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SymbolRecognizer
{
    public class ScaleHandler
    {
        public BitmapAsArrayOfColors bitmapAsArrayOfColors;

        public double CoefficientX;
        public double CoefficientY;

        public int width;
        public int height;
        public int TempWidthOfWorkWindow;
        public int TempHeightOfWorkWindow;

        public ScaleHandler(int width, int height)
        {
            this.width = width;
            this.height = height;
            TempWidthOfWorkWindow = width * 2;
            TempHeightOfWorkWindow = height * 2;
        }

        //Выявление координат крайних точек
        public void FindScalingCoefficients(out int mostLeft, out int mostTop, out int mostRight, out int mostDown)
        {
            mostRight = 0;//Аргументы для процедуры вычисления крайних i,j
            mostDown = 0;
            mostLeft = width;
            mostTop = height;

            for (int i = 0; i < width; i++)//Поиск координат левого верхнего угла
                for (int j = 0; j < height; j++)
                    if (ColorsComparer.CompareColors(bitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                    {
                        if (i <= mostLeft) mostLeft = i;//Отсеивание самого левого пикселя
                        if (j <= mostTop) mostTop = j;//Отсеивание самого верхнего пикселя
                        if (i >= mostRight) mostRight = i;//Отсеивание самого правого пикселя
                        if (j >= mostDown) mostDown = j;//Отсеивание самого нижнего пикселя
                    }

            //Коэффициенты уравнивания изображения с рабочей областью.
            //(оптимальными являются 0 < x <= 2, т.к. при масштабировании(см.ниже) 
            //будут возникать белые пробелы между черными пикселями толщиной 1 пиксель).
            CoefficientX = (double)width / (double)(mostRight - mostLeft);
            CoefficientY = (double)height / (double)(mostDown - mostTop);
        }

        //Масштабирует умножением на значение Coefficient(или на 2)
        public void BeginScale()
        {
            int i1;//Увеличенные координаты
            int j1;
            int mostRight;//Аргументы для процедуры вычисления крайних i,j
            int mostDown;
            int mostLeft;
            int mostTop;

            for (int i = 0; i < TempWidthOfWorkWindow; i++)//Предворительная чистка первого доп.массива
                for (int j = 0; j < TempHeightOfWorkWindow; j++)
                    bitmapAsArrayOfColors.TempArrayOfColors1[i, j] = Color.White;

            FindScalingCoefficients(out mostLeft, out mostTop, out mostRight, out mostDown);//Получение коэффициентов масштабирования kx,ky

            //Максимальное увеличение х2 за раз(из-за того что устранение полос работает в промежутке толщиной 1 пиксель,
            //а такую толщину можно получить при данном коэффициенте)
            if (CoefficientX <= 2 && CoefficientY <= 2)
            {
                for (int i = 0; i < width; i++)//Масштабирование в k раз
                    for (int j = 0; j < height; j++)
                        if (ColorsComparer.CompareColors(bitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                        {
                            i1 = (int)Math.Round((double)i * CoefficientX);
                            j1 = (int)Math.Round((double)j * CoefficientY);
                            //Перенос пикселей исходника по новым координатам в промежуточный массив 
                            bitmapAsArrayOfColors.TempArrayOfColors1[i1, j1] = bitmapAsArrayOfColors.ArrayOfColors[i, j];//
                        }
            }
            else if (CoefficientX <= 2 && CoefficientY > 2)
            {
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                        if (ColorsComparer.CompareColors(bitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                        {
                            i1 = (int)Math.Round((double)i * CoefficientX);
                            j1 = (int)Math.Round((double)j * 2);
                            //Перенос пикселей исходника по новым координатам в промежуточный массив 
                            bitmapAsArrayOfColors.TempArrayOfColors1[i1, j1] = bitmapAsArrayOfColors.ArrayOfColors[i, j];//
                        }
            }
            else if (CoefficientX > 2 && CoefficientY <= 2)
            {
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                        if (ColorsComparer.CompareColors(bitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                        {
                            i1 = (int)Math.Round((double)i * 2);
                            j1 = (int)Math.Round((double)j * CoefficientY);
                            //Перенос пикселей исходника по новым координатам в пром.массив 840х600 пикселей
                            bitmapAsArrayOfColors.TempArrayOfColors1[i1, j1] = bitmapAsArrayOfColors.ArrayOfColors[i, j];//
                        }
            }
            else
            {
                for (int i = 0; i < width; i++)//Масштабирование в 2 раз
                    for (int j = 0; j < height; j++)
                        if (ColorsComparer.CompareColors(bitmapAsArrayOfColors.ArrayOfColors[i, j], Color.Black))
                        {
                            i1 = (int)Math.Round((double)i * 2);
                            j1 = (int)Math.Round((double)j * 2);//Перенос пикселей исходника по новым координатам в пром.массив 840х600 пикселей
                            bitmapAsArrayOfColors.TempArrayOfColors1[i1, j1] = bitmapAsArrayOfColors.ArrayOfColors[i, j];//
                        }
            }

            RemoveWhiteLines();//Устранение белых полос
            MoveScaledImageToWorkspace();//Сдвиг отредактированного изображения в исходный раб.массив
        }

        //Устранение белых полос из изображения 
        public void RemoveWhiteLines()
        {
            for (int i = 1; i < TempWidthOfWorkWindow - 1; i++)
                for (int j = 1; j < TempHeightOfWorkWindow - 1; j++)
                {
                    //Проверяет соседние(лево/право)пиксели на одновременную схожесть с действующим(после процедуры должны остаться горизонтальные полосы)
                    if (!(ColorsComparer.CompareColors(bitmapAsArrayOfColors.TempArrayOfColors1[i, j], bitmapAsArrayOfColors.TempArrayOfColors1[i + 1, j]))
                     && !(ColorsComparer.CompareColors(bitmapAsArrayOfColors.TempArrayOfColors1[i, j], bitmapAsArrayOfColors.TempArrayOfColors1[i - 1, j])))
                    {
                        bitmapAsArrayOfColors.TempArrayOfColors1[i, j] = Color.Black;//Перекрашивает действующий пиксель в черный
                    }
                }

            for (int i = 1; i < TempWidthOfWorkWindow - 1; i++)
                for (int j = 1; j < TempHeightOfWorkWindow - 1; j++)
                    //Проверяет соседние(верх/низ)пиксели на одновременную схожесть с действующим 
                    if (!(ColorsComparer.CompareColors(bitmapAsArrayOfColors.TempArrayOfColors1[i, j], bitmapAsArrayOfColors.TempArrayOfColors1[i, j + 1]))
                     && !(ColorsComparer.CompareColors(bitmapAsArrayOfColors.TempArrayOfColors1[i, j], bitmapAsArrayOfColors.TempArrayOfColors1[i, j - 1])))
                    {
                        bitmapAsArrayOfColors.TempArrayOfColors1[i, j] = Color.Black;//Перекрашивает действующий пиксель в черный
                    }
        }

        //Сдвиг отмасштабированного изображения в раб.область
        public void MoveScaledImageToWorkspace()
        {
            int mostLeft = TempWidthOfWorkWindow;
            int mostTop = TempHeightOfWorkWindow;

            for (int i = 0; i < TempWidthOfWorkWindow; i++)//Поиск координат левого верхнего угла
                for (int j = 0; j < TempHeightOfWorkWindow; j++)
                    if (ColorsComparer.CompareColors(bitmapAsArrayOfColors.TempArrayOfColors1[i, j], Color.Black))
                    {
                        if (i <= mostLeft) mostLeft = i;//Отсеивание самого левого пикселя
                        if (j <= mostTop) mostTop = j;//Отсеивание самого верхнего пикселя
                    }

            for (int i = 0; i < TempWidthOfWorkWindow; i++)//Предварительная чистка второго доп.массива
                for (int j = 0; j < TempHeightOfWorkWindow; j++)
                    bitmapAsArrayOfColors.TempArrayOfColors2[i, j] = Color.White;

            for (int i = 0; i < TempWidthOfWorkWindow; i++)//Сдвиг отмасштабированного изображения в исходное поле(из первого во второй доп.массив)
                for (int j = 0; j < TempHeightOfWorkWindow; j++)
                    if (ColorsComparer.CompareColors(bitmapAsArrayOfColors.TempArrayOfColors1[i, j], Color.Black))
                        bitmapAsArrayOfColors.TempArrayOfColors2[i - mostLeft, j - mostTop] = bitmapAsArrayOfColors.TempArrayOfColors1[i, j];

            for (int i = 0; i < width; i++)//Перенос из второго доп.массива в исходный
                for (int j = 0; j < height; j++)
                    bitmapAsArrayOfColors.ArrayOfColors[i, j] = bitmapAsArrayOfColors.TempArrayOfColors2[i, j];
        }

        //Повторение масштабирования до выравнивания с раб.областью   
        public void RepeatScaling()
        {
            int mostRight;//Аргументы для процедуры вычисления крайних i,j
            int mostDown;
            int mostLeft;
            int mostTop;

            while (true)//Если иображение отмасштабировано крайние i,j будут примерно равны границам рабочей области 
            {
                BeginScale();
                //Получение крайних координат, чтобы определить заняло ли изображение все пространство
                FindScalingCoefficients(out mostLeft, out mostTop, out mostRight, out mostDown);
                if ((mostLeft - 10 <= 0) && (mostTop - 10 <= 0) && (mostRight + 10 >= width) && (mostDown + 10 >= height))
                    break;//Если заняло то цикл масштабирований заканчивается
                
                //P.S. Без устранения белых полос каждую итерацию данная проверка может уйти в бесконечность на тонких линиях у краёв рабочей области,
                //т.к. полосы будут увеличиваться каждую итерацию и ряды черных пикселей в определенный момент увеличения будут выскакивать за рабочую область,
                //из-за чего новая рамка границ изображения все время будет оставаться внутри рабочей области не доходя до её границ
            }
        }
    }
}
