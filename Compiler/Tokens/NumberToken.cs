using System;

namespace ArithmeticParser
{
    public class NumberToken : Token
    {
        public int Value { get; set; }

        public NumberToken(int value)
        {
            Value = value;
        }

        public override void Print()
        {
            base.PrintToConsole(this.Value.ToString());
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}