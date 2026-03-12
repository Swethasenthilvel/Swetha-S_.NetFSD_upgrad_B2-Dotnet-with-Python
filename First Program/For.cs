using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Program
{
    internal class For
    {

        public For() {

            string name;
            Console.WriteLine("Enter Your name: ");
            name = Console.ReadLine() !;
            
            for(int i =1; i <= 10; i++)
            {
                Console.WriteLine(name);
            }
        
        }
    }
}
