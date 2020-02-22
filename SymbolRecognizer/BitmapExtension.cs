using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolRecognizer
{
    public static class BitmapExtension
    {
        public static Color[,] BitmapToArray(this Bitmap bitmap)
        {
            Color[,] result = new Color[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    result[i, j] = bitmap.GetPixel(i, j);
                }
            }

            return result;
        }
    }
}
