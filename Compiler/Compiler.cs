using System;
using ArithmeticParser.Parsing;

namespace ArithmeticParser
{
    public class Compiler
    {
        private Lexer lexer;
        private Parser parser;
        private SyntaxTree semantics;

        public Compiler(string sourceCode)
        {
            lexer = new Lexer(sourceCode);
        }
        
        public Executable Compile()
        {
            if (lexer.Analyse())
            {
                Token[] tokens = lexer.CreateTokens();

                // for (var i = 0; i < tokens.Length; i++)
                // {
                //     var token = tokens[i];
                //     Console.Write(i + 1 + ": ");
                //     token.Print();
                // }

                this.parser = new Parser(tokens);

                if (this.parser.Analyse())
                {
                    Expression expression = this.parser.CachedExpression;
                    expression.TreeView();
                    this.semantics = new SyntaxTree(expression);
                }

            }

            return null;
        }
    }
}