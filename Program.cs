using System;

namespace ArithmeticParser
{
    class Program
    {
        public static void Main(string[] args)
        {
            string sourceCode = "(2 + (3 + 4))";

            Compiler compiler = new Compiler(sourceCode);
            Executable exe = compiler.Compile();
            exe.Execute();

            // Executable executable = compiler.Compile(sourceCode);

            // int result = executable.Execute();
        }
    }
}
