using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExercise
{
    public static class ExtensionMethods
    {
        public static Tuple<int, int> Compute(this WinExcerciseForm excerciseForm, int value)
        {
            Tuple<int, int> tuple = Tuple.Create(value * value, value * value * value);
            return tuple;
        }
    }
}
