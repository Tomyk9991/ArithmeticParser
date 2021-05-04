using System;

namespace ArithmeticParser
{
    public abstract class Token
    {
        public abstract void Print();

        protected void PrintToConsole(string value)
        {
            Console.WriteLine(value);
        }
        
        protected void PrintToConsole(char value)
        {
            Console.WriteLine(value);
        }
    }
}