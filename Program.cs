using System;

namespace ArithmeticParser
{
    class Program
    {
        public static void Main(string[] args)
        {
            string sourceCode = "((4 - 2^3 + 1) * -sqrt(3*3+4*4)) / 2";

            Compiler compiler = new Compiler(sourceCode);
            Console.WriteLine(compiler.Evaluate());
            Console.WriteLine(compiler.SyntaxTree.TreeView());
        }
    }
}