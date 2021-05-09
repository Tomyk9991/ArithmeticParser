using System;

namespace ArithmeticParser
{
    public interface IExpression
    {
        double Evaluate();
        IExpression Add(IExpression other);
        IExpression Sub(IExpression other);
        IExpression Mul(IExpression other);
        IExpression Div(IExpression other);
    }
    
    public class Expression : IExpression
    {
        private double value = Double.NaN;
        
        public Expression(IExpression e) => value = e.Evaluate();
        public Expression(double val) => value = val;

        public double Evaluate() => value;
        
        public IExpression Add(IExpression other) => new Expression(this.value + other.Evaluate());
        public IExpression Sub(IExpression other) => new Expression(this.value - other.Evaluate());
        public IExpression Mul(IExpression other) => new Expression(this.value * other.Evaluate());
        public IExpression Div(IExpression other) => new Expression(this.value / other.Evaluate());
    }
    
    public class Compiler
    {
        // Grammatik:
        // expression = term | expression `+` term | expression `-` term
        // term = factor | term `*` factor | term `/` factor
        // factor = `+` factor | `-` factor | `(` expression `)` | number | functionName factor | factor `^` factor
        
        private string sourceCode = "";
        private int pos = -1;
        private int ch;

        public Compiler(string sourceCode)
        {
            this.sourceCode = sourceCode;
        }

        public double Evaluate(string sourceCode)
        {
            this.sourceCode = sourceCode;
            return Parse().Evaluate();
        }

        void nextChar()
        {
            ch = (++pos < this.sourceCode.Length) ? this.sourceCode[pos] : -1;
        }

        private bool Eat(int charToEat)
        {
            while (ch == ' ') nextChar();
            if (ch == charToEat)
            {
                nextChar();
                return true;
            }

            return false;
        }

        private IExpression Parse()
        {
            nextChar();
            IExpression x = ParseExpression();
            if (pos < this.sourceCode.Length) throw new Exception("Unexpected: " + (char) ch);
            return x;
        }
        
        private IExpression ParseExpression()
        {
            IExpression x = ParseTerm();
            while (true)
            {
                if (Eat('+')) x = x.Add(ParseTerm());
                else if (Eat('-')) x = x.Sub(ParseTerm());
                else return x;
            }
        }

        IExpression ParseTerm()
        {
            IExpression x = ParseFactor();
            while (true)
            {
                if (Eat('*')) x = x.Mul(ParseTerm());
                else if (Eat('/')) x = x.Div(ParseTerm());
                else return x;
            }
        }

        IExpression ParseFactor()
        {
            if (Eat('+')) return new Expression(ParseFactor());
            if (Eat('-')) return new Expression(-ParseFactor().Evaluate());

            IExpression x;
            int startPos = this.pos;
            if (Eat('('))
            {
                x = ParseExpression();
                Eat(')');
            }
            else if (ch >= '0' && ch <= '9' || ch == '.')
            {
                while (ch >= '0' && ch <= '9' || ch == '.') nextChar();
                x = new Expression(double.Parse(this.sourceCode.Substring(startPos, this.pos - startPos)));
            }
            else if (ch >= 'a' && ch <= 'z')
            {
                // functions
                while (ch >= 'a' && ch <= 'z') nextChar();
                String func = this.sourceCode.Substring(startPos, this.pos - startPos);
                x = ParseFactor();
                if (func == "sqrt")     x = new Expression(Math.Sqrt(x.Evaluate()));
                else if (func == "sin") x = new Expression(Math.Sin(x.Evaluate()));
                else if (func == "cos") x = new Expression(Math.Cos(x.Evaluate()));
                else if (func == "tan") x = new Expression(Math.Tan(x.Evaluate()));
                else throw new Exception("Unknown function: " + func);
            }
            else
            {
                throw new Exception("Unexpected: " + (char) ch);
            }

            if (Eat('^')) x = new Expression(Math.Pow(x.Evaluate(), ParseFactor().Evaluate())); // exponentiation

            return x;
        }
    }
}