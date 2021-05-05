using System;
using System.Data;
using ArithmeticParser.Parsing;

namespace ArithmeticParser
{
    public class Parser : IAnalysable
    {
        public Expression CachedExpression { get; private set; }
        
        
        private Token[] tokens;
        private int i = 0;
        public Parser(Token[] tokens)
        {
            this.tokens = tokens;
            this.CachedExpression = null;
        }
        
        public bool Analyse()
        {
            //Operate on tokens. Return true or false, based on token ordering
            
            // 1) Expression   -> Expression Operation Expression  (Nonterminale)
            // 2) Expression   -> (Expression)                     (Klammern sind Terminale)
            // 3) Expression   -> -Expression                      (- als Terminal)
            // 4) Expression   -> 0, 1, 2, 3, 4, 5, 6, 7, 8, 9...  (Terminale)
            // 5) Operation    -> +, -, *, /                       (Terminale)

            Expression expression = new Expression();
            try
            {
                expression = BuildExpressions(expression, i);
            }
            catch (Exception e)
            {
                return false;
            }

            this.CachedExpression = expression;
            return true;
        }

        private Expression BuildExpressions(Expression expression, int startIndex)
        {
            while(startIndex < tokens.Length)
            {
                //Still work to do, but Expression is already full.
                //Move the content of the root into the lhs 
                if (expression.Defined)
                {
                    Expression LHS = new Expression(expression);
                    Expression operation = BuildExpressions(expression, i);
                    Expression RHS = BuildExpressions(expression, i);
                }
                
                if (i < tokens.Length - 1 && tokens[i] is ParenthesisToken)
                {
                    ParenthesisToken parenthesisToken = (ParenthesisToken) tokens[i];
                    
                    if (parenthesisToken.Value == '(')
                    {
                        Expression e = new Expression();

                        i++;
                        Expression lhs = BuildExpressions(e, i);
                        i++;
                        Expression operation = BuildExpressions(e, i);
                        i++;
                        Expression rhs = BuildExpressions(e, i);
                        i++;

                        if (tokens[i] is ParenthesisToken && (tokens[i] as ParenthesisToken).Value == ')')
                        {
                            e.Set(int.MaxValue, operation.Operation, lhs, rhs);
                            return e;
                            // if (i == tokens.Length - 1)
                            //     return e;
                            //
                            // continue;
                        }

                        throw new SyntaxErrorException("Missing closing bracket");
                    }
                }

                if (tokens[i] is OperatorToken)
                {
                    OperatorToken ot = (OperatorToken) tokens[i];
                    return new Expression(Int32.MaxValue, ot.Operation, null, null);
                }
                
                if (tokens[i] is NumberToken)
                {
                    NumberToken nt = (NumberToken) tokens[i];
                    return new Expression(nt.Value, Operation.Noop, null, null);
                }
            }

            return expression;
        }
    }
}