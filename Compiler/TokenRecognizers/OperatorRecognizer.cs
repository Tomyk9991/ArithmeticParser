namespace ArithmeticParser
{
    public class OperatorRecognizer : ITokenRecognizer
    {
        public Token Recognize(ref string code)
        {
            Operation operation = OperatorToken.FromChar(code[0]);
            
            if (operation != Operation.Noop)
            {
                code = code[1..];
                return new OperatorToken(operation);
            }

            return null;
        }
    }
}