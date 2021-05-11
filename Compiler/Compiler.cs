using System;

namespace ArithmeticParser
{
    public interface IExpression
    {
        public IExpression LHS { get; set; }
        public IExpression RHS { get; set; }
        public Operation OP { get; set; }

        public double Value { get; set; }
        
        double Evaluate();
        IExpression Add(IExpression other);
        IExpression Sub(IExpression other);
        IExpression Mul(IExpression other);
        IExpression Div(IExpression other);

        public string TreeView(string indent = "", bool last = true);
    }

    public enum Operation
    {
        ADD,
        SUB,
        DIV,
        MUL,
        POW,
        NOOP
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
            InitCompiler(sourceCode);
        }

        
        public IExpression SyntaxTree
        {
            get;
            private set;
        }

        private void InitCompiler(string sourceCode)
        {
            this.sourceCode = sourceCode;
        }

        public double Evaluate()
        {
            if (!string.IsNullOrEmpty(this.sourceCode))
            {
                return Parse().Evaluate();
            }

            throw new Exception("No string for evaluation present");
        }
        
        public double Evaluate(string sourceCode)
        {
            InitCompiler(sourceCode);
            return Parse().Evaluate();
        }

        void nextChar()
        {
            ch = ++pos < this.sourceCode.Length ? this.sourceCode[pos] : -1;
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
            this.sourceCode = this.sourceCode.Replace('.', ',');
            
            nextChar();
            this.SyntaxTree = ParseExpression();
            if (pos < this.sourceCode.Length) throw new Exception("Unexpected: " + (char) ch);
            return this.SyntaxTree;
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
            if (Eat('+'))
            {
                var e = new Expression(ParseFactor());
                return e;
            }

            if (Eat('-'))
            {
                var e = new Expression(-ParseFactor().Evaluate());
                return new Expression(e);
            }

            IExpression x;
            int startPos = this.pos;
            if (Eat('('))
            {
                x = ParseExpression();
                if (!Eat(')'))
                {
                    throw new Exception("Expected: )");
                }
            }
            else if (ch >= '0' && ch <= '9' || ch == '.')
            {
                while (ch >= '0' && ch <= '9' || ch == ',' || ch == '.') nextChar();
                x = new Expression(double.Parse(this.sourceCode.Substring(startPos, this.pos - startPos)));
            }
            else if (ch >= 'a' && ch <= 'z')
            {
                // functions
                while (ch >= 'a' && ch <= 'z') nextChar();
                String func = this.sourceCode.Substring(startPos, this.pos - startPos);
                x = ParseFactor();
                
                x = func switch
                {
                    "sqrt" => new Expression("Sqrt", x, Math.Sqrt(x.Evaluate())),
                    "sin" => new Expression("Sin", x, Math.Sin(x.Evaluate())),
                    "cos" => new Expression("Cos", x, Math.Cos(x.Evaluate())),
                    "tan" => new Expression("Tan", x, Math.Tan(x.Evaluate())),
                    _ => throw new Exception("Unknown function: " + func)
                };
            }
            else
            {
                throw new Exception("Unexpected: " + (char) ch);
            }

            if (Eat('^'))
            {
                x = new Expression(Math.Pow(x.Evaluate(), ParseFactor().Evaluate())); // exponentiation
            }

            return x;
        }
    }
}