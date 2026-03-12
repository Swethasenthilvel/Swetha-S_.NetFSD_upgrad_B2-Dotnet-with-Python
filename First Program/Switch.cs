using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Program
{
    internal class Switch
    {
        public Switch() {
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

            switch(choice)
                {
                case 1:
                    Console.WriteLine("Addition: " + (a + b));
                    break;

                case 2:
                    Console.WriteLine("Subtraction: " + (a - b));
                    break;
                case 3:
                    Console.WriteLine("Multiplication: " + (a * b));
                    break;
                case 4:
                    Console.WriteLine("Division: " + (a / b));
                    break;
                default:
                    Console.WriteLine("Invalid Number:");
                    break;
                }
            }
        }
    }
