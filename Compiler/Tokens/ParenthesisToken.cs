namespace ArithmeticParser
{
    public class ParenthesisToken : Token
    {
        public char Value { get; set; }

        public ParenthesisToken(char value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override void Print()
        {
            base.PrintToConsole(this.Value);
        }
    }
}