using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Program
{
    internal class Even_Numbers
    {
        public Even_Numbers() {

        
            for(int i =1; i <= 100; i++)
            {
                if (i%2 == 0)
                {
                    Console.WriteLine(i);
                }
               
            }
        
        }
    }
}
