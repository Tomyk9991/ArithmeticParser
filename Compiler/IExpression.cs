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
        IExpression Pow(IExpression other);

        public string TreeView(string indent = "", bool last = true);
    }
}