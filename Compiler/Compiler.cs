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
                
                for (int i = 0; i < tokens.Length; i++)
                {
                    tokens[i].Print();
                }
            }
        }
    }
}