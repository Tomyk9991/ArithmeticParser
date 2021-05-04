namespace ArithmeticParser
{
    public interface ITokenRecognizer
    {
        Token Recognize(ref string code);
    }
}