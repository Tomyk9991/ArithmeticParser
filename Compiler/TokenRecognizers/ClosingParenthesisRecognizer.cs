namespace ArithmeticParser
{
    public class ClosingParenthesisRecognizer : ITokenRecognizer
    {
        public Token Recognize(ref string code)
        {
            if (code[0] == ')')
            {
                code = code[1..];
                return new ParenthesisToken(')');
            }

            return null;
        }
    }
}