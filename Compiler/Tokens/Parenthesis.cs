namespace ArithmeticParser
{
    public class Parenthesis : Token
    {
        public char Value { get; set; }

        public Parenthesis(char value)
        {
            Value = value;
        }

        public override void Print()
        {
            base.PrintToConsole(this.Value);
        }
    }
}