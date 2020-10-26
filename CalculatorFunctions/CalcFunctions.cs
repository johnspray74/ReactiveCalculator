using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorFunctions
{
    public static class CalcFunctions
    {
         public static int Fact(int x) { if (x <= 1) return 1; else return x * Fact(x - 1); }
    }
}
