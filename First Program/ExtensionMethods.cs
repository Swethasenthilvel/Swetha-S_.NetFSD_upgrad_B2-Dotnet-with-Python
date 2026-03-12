using System;

namespace First_Program
{
    internal class ExtensionMethods
    {
        public ExtensionMethods()
        {
            int numeric = 5;

            Console.WriteLine(numeric.IsPrime());

            string name = "abc";
            Console.WriteLine(name.ToLower());
            Console.WriteLine(name.ToUpper());
            Console.WriteLine(name.Length);
            Console.WriteLine(name.IsPalindrome());
        }

        //static void Main(string[] args)
        //{
        //    ExtensionMethods obj = new ExtensionMethods();
        //}
    }

    static class ExtensionToStringClass
    {
        public static bool IsPalindrome(this string str)
        {
            string rev = "";

            for (int i = str.Length - 1; i >= 0; i--)
            {
                rev += str[i];
            }

            return str == rev;
        }

        public static bool IsPrime(this int num)
        {
            if (num <= 1)
                return false;

            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }
    }
}