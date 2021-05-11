using System;

namespace ArithmeticParser
{
    class Program
    {
        public static void Main(string[] args)
        {
            string sourceCode = "sin(2.1321 * 3) * 3";

            Compiler compiler = new Compiler(sourceCode);
            Console.WriteLine(compiler.Evaluate());
            Console.WriteLine(compiler.SyntaxTree.TreeView());
        }
    }
}
