using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Program
{
    internal class Arith_switch_if_else
    {
        public Arith_switch_if_else()
        {
            int a, b, choice;
            Console.WriteLine("Enter Number a:");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Number b:");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Arithmetic Opertions:");
            Console.WriteLine("1.Addition");
            Console.WriteLine("2.Subtraction");
            Console.WriteLine("3.Multiplication");
            Console.WriteLine("4.Division");
            Console.WriteLine("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("Add: " + (a + b));
            }
            else if (choice == 2)
            {
                Console.WriteLine("Sub: " + (a - b));

            }
            else if (choice == 3)
            {
                Console.WriteLine("Multi: " + (a * b));

            }
            else if (choice == 4) 
            {
                Console.WriteLine("Div:" + (a / b));
            }
            else
            {
                Console.WriteLine("Invalid Choice");
            }
        }

    }
}
