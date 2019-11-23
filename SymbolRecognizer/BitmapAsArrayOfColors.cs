using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolRecognizer
{ 
    public class BitmapAsArrayOfColors
    {
        public int width;
        public int height;
        public int TempWidthOfWorkWindow;
        public int TempHeightOfWorkWindow;

        public Color[,] ArrayOfColors;
        public Color[,] TempArrayOfColors1;
        public Color[,] TempArrayOfColors2;

        public BitmapAsArrayOfColors(int width, int height)
        {
            this.width = width;
            this.height = height;
            TempWidthOfWorkWindow = width * 2;
            TempHeightOfWorkWindow = height * 2;

            TempArrayOfColors1 = new Color[TempWidthOfWorkWindow, TempHeightOfWorkWindow];
            TempArrayOfColors2 = new Color[TempWidthOfWorkWindow, TempHeightOfWorkWindow];
            ArrayOfColors = new Color[width, height];
        }

        public void BitmapToArray(Bitmap picture)
        {
            ArrayOfColors = new Color[picture.Width, picture.Height];
            for (int i = 0; i < picture.Width; i++)
                for (int j = 0; j < picture.Height; j++)
                {
                    ArrayOfColors[i, j] = picture.GetPixel(i, j);
                }
        }
    }
}
