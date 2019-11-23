using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SymbolRecognizer
{
    public class ColorsComparer
    {
        public static bool CompareColors(Color color1, Color color2)//цвета из разных источников не проверяются через оператор ==, поэтому нужен какойто код для проверки
        {
            var colorCode1 = (long)(color1.R * Math.Pow(10.0, 6.0) + color1.B * Math.Pow(10.0, 3.0) + color1.G);
            var colorCode2 = (long)(color2.R * Math.Pow(10.0, 6.0) + color2.B * Math.Pow(10.0, 3.0) + color2.G);
            return colorCode1 == colorCode2;
        }
    }
}
