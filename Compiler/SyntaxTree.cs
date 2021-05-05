using ArithmeticParser.Parsing;

namespace ArithmeticParser
{
    public class SyntaxTree
    {
        private Expression expression;
        public SyntaxTree(Expression expression)
        {
            this.expression = expression;
        }

        public int Execute()
        {
            // this.expression.Sort();
            return this.expression.Evaluate();
        }
    }
}