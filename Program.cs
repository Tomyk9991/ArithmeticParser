using System;

namespace ArithmeticParser
{
    class Program
    {
        public static void Main(string[] args)
        {
            string sourceCode = "(2 + (3 + 4)) + 5";

            Compiler compiler = new Compiler(sourceCode);
            Console.WriteLine(compiler.Evaluate(sourceCode));
        }
    }
}
