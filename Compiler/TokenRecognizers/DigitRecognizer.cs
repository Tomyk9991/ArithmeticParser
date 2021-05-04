namespace ArithmeticParser
{
    public class DigitRecognizer : ITokenRecognizer
    {
        public Token Recognize(ref string code)
        {
            char sign = '+';
            int startIndex = 0;
            
            if (code[0] == '+' || code[0] == '-')
            {
                startIndex = 1;
                sign = code[0];
            }

            string digit = sign.ToString();
            for (int i = startIndex; i < code.Length; i++)
            {
                if(char.IsDigit(code[i]))
                    digit += code[i];
                else
                    break;
            }

            if (digit.Length != 1)
            {
                if (startIndex == 1)
                {
                    code = code[digit.Length..];
                }
                else
                {
                    code = code[(digit.Length - 1)..];
                }
                return new NumberToken(int.Parse(digit));
            }

            return null;
        }
    }
}