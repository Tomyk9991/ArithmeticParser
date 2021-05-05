using System;
using System.Collections.Generic;

namespace ArithmeticParser.Parsing
{
    public class Expression
    {
        public int Value { get; private set; }
        public Operation Operation { get; private set; }
        public Expression LHS { get; private set; }
        public Expression RHS { get; private set; }

        public bool Defined =>
            Value == int.MaxValue &&
            this.Operation != Operation.Noop &&
            this.LHS != null && this.RHS != null;

        public Expression()
        {
            Value = int.MaxValue;
            Operation = Operation.Noop;
            LHS = null;
            RHS = null;
        }

        public Expression(Expression e)
        {
            this.Value = e.Value;
            this.Operation = e.Operation;
            this.LHS = e.LHS;
            this.RHS = e.RHS;
        }

        public Expression(int value, Operation operation, Expression lhs, Expression rhs)
        {
            this.Value = value;
            this.Operation = operation;
            this.LHS = lhs;
            this.RHS = rhs;
        }

        public void Set(int value, Operation operation, Expression lhs, Expression rhs)
        {
            this.Value = value;
            this.Operation = operation;
            this.LHS = lhs;
            this.RHS = rhs;
        }


        public override string ToString()
        {
            string lhs = this.LHS?.ToString() ?? "NULL";
            string rhs = this.RHS?.ToString() ?? "NULL";


            if (this.Value != int.MaxValue && lhs == "NULL" && rhs == "NULL")
                return this.Value.ToString();

            if (this.Operation != Operation.Noop && lhs == "NULL" && rhs == "NULL")
                return this.Operation.ToString();

            return string.Join(' ', lhs, this.Operation, rhs);
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
            
            var children = new List<Expression>();
            
            if (this.LHS != null) children.Add(this.LHS);
            if (this.RHS != null) children.Add(this.RHS);
            
            for (int i = 0; i < children.Count; i++)
            {
                children[i].TreeView(indent, i == children.Count - 1);
            }
        }

        private string ToInnerString()
        {
            if (this.Defined)
                return this.Operation.ToString();
            
            string lhs = this.LHS?.ToString() ?? "NULL";
            string rhs = this.RHS?.ToString() ?? "NULL";


            if (this.Value != int.MaxValue && lhs == "NULL" && rhs == "NULL")
                return this.Value.ToString();

            if (this.Operation != Operation.Noop && lhs == "NULL" && rhs == "NULL")
                return this.Operation.ToString();

            return string.Join(' ', lhs, this.Operation, rhs);
        }

        public int Evaluate()
        {
            if (this.LHS != null && this.RHS != null)
            {
                int var1 = this.LHS.Evaluate();
                int var2 = this.RHS.Evaluate();
                
                if (var2 == 0 && this.Operation == Operation.Division)
                    throw new DivideByZeroException();

                switch (this.Operation)
                {
                    case Operation.Addition:
                        return var1 + var2;
                    case Operation.Subtraction:
                        return var1 - var2;
                    case Operation.Multiplication:
                        return var1 * var2;
                    case Operation.Division:
                        return var1 / var2;
                }
            }

            if (this.Value != int.MaxValue)
                return this.Value;
            
            
            throw new ArgumentOutOfRangeException("Something went wrong");
        }
    }
}