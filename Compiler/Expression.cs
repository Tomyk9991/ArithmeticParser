using System;
using System.Collections.Generic;

namespace ArithmeticParser
{
    public class Expression : IExpression
    {
        public Expression(IExpression e)
        {
            Value = e.Evaluate();
        }

        public Expression(double val)
        {
            Value = val;
        }

        private Expression(IExpression lhs, Operation op, IExpression rhs, double value)
        {
            this.LHS = lhs;
            this.OP = op;
            this.RHS = rhs;

            this.Value = value;
        }
        
        public bool Defined =>
            Value == int.MaxValue &&
            this.OP != Operation.NOOP &&
            this.LHS != null && this.RHS != null;

        public IExpression LHS { get; set; }
        public IExpression RHS { get; set; }
        public Operation OP { get; set; }
        public double Value { get; set; }
        
        
        public double Evaluate() => Value;

        public IExpression Add(IExpression other)
        {
            return new Expression(this, Operation.ADD, other, this.Value + other.Evaluate());
        }

        public IExpression Sub(IExpression other)
        {
            return new Expression(this, Operation.SUB, other, this.Value - other.Evaluate());
        }

        public IExpression Mul(IExpression other)
        {
            return new Expression(this, Operation.MUL, other, this.Value * other.Evaluate());
        }

        public IExpression Div(IExpression other)
        {
            return new Expression(this, Operation.DIV, other, this.Value / other.Evaluate());
        }

        public void TreeView(string indent = "", bool last = true)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "| ";
            }
            
            Console.WriteLine(this.ToInnerString());
            
            var children = new List<IExpression>();
            
            if (this.LHS != null) children.Add(this.LHS);
            if (this.RHS != null) children.Add(this.RHS);
            
            for (int i = 0; i < children.Count; i++)
            {
                children[i].TreeView(indent, i == children.Count - 1);
            }
        }
        
        public override string ToString()
        {
            string lhs = this.LHS?.ToString() ?? "NULL";
            string rhs = this.RHS?.ToString() ?? "NULL";


            if (this.Value != int.MaxValue && lhs == "NULL" && rhs == "NULL")
                return this.Value.ToString();

            if (this.OP != Operation.NOOP && lhs == "NULL" && rhs == "NULL")
                return this.OP.ToString();

            return string.Join(' ', lhs, this.OP, rhs);
        }
        
        private string ToInnerString()
        {
            if (this.Defined)
                return this.OP.ToString();
            
            string lhs = this.LHS?.ToString() ?? "NULL";
            string rhs = this.RHS?.ToString() ?? "NULL";


            if (this.Value != int.MaxValue && this.LHS == null && this.RHS == null)
                return this.Value.ToString();

            if (this.OP != Operation.NOOP && this.LHS == null && this.RHS == null)
                return this.OP.ToString();

            return string.Join(' ', this.Value + ", " + this.OP);
        }
    }
}