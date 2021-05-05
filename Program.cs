using System;

namespace ArithmeticParser
{
    class Program
    {
        public static void Main(string[] args)
        {
            string sourceCode = "(2 +)) 3)";

            Compiler compiler = new Compiler(sourceCode);
            compiler.Compile();
            
            
            // Executable executable = compiler.Compile(sourceCode);

            // int result = executable.Execute();
        }
    }
}
