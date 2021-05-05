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
        
        public void Compile()
        {
            if (lexer.Analyse())
            {
                Token[] tokens = lexer.CreateTokens();

                foreach (var token in tokens)
                    token.Print();

                this.parser = new Parser(tokens);
                this.parser.Analyse();
            }
        }
    }
}