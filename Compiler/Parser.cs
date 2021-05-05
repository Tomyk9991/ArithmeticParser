using System;
using System.Collections.Generic;
using ArithmeticParser.Parsing;

namespace ArithmeticParser
{
    public class Parser : IAnalysable
    {
        private Token[] tokens;
        public Parser(Token[] tokens)
        {
            this.tokens = tokens;
        }
        
        public bool Analyse()
        {
            //Operate on tokens. Return true or false, based on token ordering
            
            // 1) Expression   -> Expression Operation Expression  (Nonterminale)
            // 2) Expression   -> (Expression)                     (Klammern sind Terminale)
            // 3) Expression   -> -Expression                      (- als Terminal)
            // 4) Expression   -> 0, 1, 2, 3, 4, 5, 6, 7, 8, 9...  (Terminale)
            // 5) Operation    -> +, -, *, /                       (Terminale)

            List<Expression> expressions = new List<Expression>();
            BuildExpressions(expressions, 0);
            
            return true;
        }

        private Expression BuildExpressions(List<Expression> expressions, int startIndex)
        {
            for (int i = startIndex; i < tokens.Length; i++)
            {
                if (tokens[i] is Parenthesis)
                {
                    Parenthesis parenthesis = (Parenthesis) tokens[i];
                    
                    if (parenthesis.Value == '(')
                    {
                        Expression e = BuildExpressions(expressions, startIndex + 1);
                        expressions.Add(e);
                    }
                }
            }

            return null;
        }
    }
}