using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SymbolRecognizer
{
    public class ColorsComparer : IComparer<Color>
    {
        public static bool IsEqual(Color color1, Color color2)
        {
            var gray1 = (int)(255 * Math.Round(0.299 * color1.R + 0.587 * color1.G + 0.114 * color1.B, 10) / 255);
            gray1 = Math.Min(gray1, 255);
            gray1 = Math.Max(gray1, 0);

            var gray2 = (int)(255 * Math.Round(0.299 * color2.R + 0.587 * color2.G + 0.114 * color2.B, 10) / 255);
            gray2 = Math.Min(gray2, 255);
            gray2 = Math.Max(gray2, 0);

            return gray1 == gray2;
        }

        public int Compare(Color color1, Color color2)
        {
            var gray1 = (int)(255 * Math.Round(0.299 * color1.R + 0.587 * color1.G + 0.114 * color1.B, 10) / 255);
            gray1 = Math.Min(gray1, 255);
            gray1 = Math.Max(gray1, 0);

            var gray2 = (int)(255 * Math.Round(0.299 * color2.R + 0.587 * color2.G + 0.114 * color2.B, 10) / 255);
            gray2 = Math.Min(gray2, 255);
            gray2 = Math.Max(gray2, 0);

            if (gray1 > gray2) return 1;
            else if (gray1 == gray2) return 0;
            else return -1;
        }
    }
}
//цвета из разных источников не проверяются через оператор ==
