using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArithmeticParser
{
    public class Lexer : IAnalysable
    {
        public string SourceCode { get; private set; }

        public Lexer(string sourceCode)
        {
            this.SourceCode = sourceCode;
        }
        
        public Token[] CreateTokens()
        {
            ITokenRecognizer[] tokenRecognizer = new ITokenRecognizer[]
            {
                new OpeningParenthesisRecognizer(),
                new DigitRecognizer(),
                new OperatorRecognizer(),
                new DigitRecognizer(),
                new ClosingParenthesisRecognizer()
            };
            
            
            List<Token> tokens = new List<Token>(3);

            string code = this.SourceCode;
            while (code != "")
            {
                for (int i = 0; i < tokenRecognizer.Length && code != ""; i++)
                {
                    Token t = tokenRecognizer[i].Recognize(ref code);
                        
                    if (t != null)
                        tokens.Add(t);
                }
            }
            
            return tokens.ToArray();
        }
        
        public bool Analyse()
        {
            char[] validChars = "123456789()+-*/".ToCharArray();
            this.SourceCode = Regex.Replace(SourceCode, @"\s+", "");

            //Empty string
            if (String.IsNullOrEmpty(this.SourceCode))
                throw new SyntaxErrorException("Source code can't be empty");
            
            //Letters
            if (Regex.IsMatch(SourceCode, ".*[a-zA-Z].*"))
                throw new SyntaxErrorException("Letters are not supported");

            //Excluding all not supported characters
            if (SourceCode.Any(currentChar => validChars.All(c => c != currentChar)))
                throw new SyntaxErrorException("Mathematical term can only contain 1-9, (, ), +, -, * or /");
            
            
            return true;
        }
    }
}