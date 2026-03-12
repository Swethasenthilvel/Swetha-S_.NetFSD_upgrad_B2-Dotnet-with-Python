using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Program
{
    internal class DebugDemo
    {
        public DebugDemo()
        {
            int num1 = 100;
            int num2 = 90;
            Console.WriteLine("enter choice");
            int ch = byte.Parse(Console.ReadLine());
            if (ch == 1)
                Console.WriteLine("Sum is " + (num1 + num2));
            else if (ch == 2)
                Console.WriteLine("Difference is " + (num1 - num2));
            else if (ch == 3)
                Console.WriteLine("Product is " + num1 * num2);

            else
                Console.WriteLine("Invalid choice");

        }
    }
}
