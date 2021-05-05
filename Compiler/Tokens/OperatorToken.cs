namespace ArithmeticParser
{
    public class OperatorToken : Token
    {
        public Operation Operation { get; set; }

        public OperatorToken(Operation operation)
        {
            Operation = operation;
        }

        public override void Print()
        {
            base.PrintToConsole(this.Operation.ToString());
        }

        public override string ToString()
        {
            return this.Operation.ToString();
        }

        public static Operation FromChar(char value)
        {
            return value switch
            {
                '+' => Operation.Addition,
                '-' => Operation.Subtraction,
                '*' => Operation.Multiplication,
                '/' => Operation.Division,
                _ => Operation.Noop
            };
        }
    }

    public enum Operation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Noop
    }
}