using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Program
{
    internal class Even_Using_Dowhile
    {
        public Even_Using_Dowhile()
        {
            int i = 1;

            do
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
                i++;
            }
            while (i < 10);
        }
    }
}
