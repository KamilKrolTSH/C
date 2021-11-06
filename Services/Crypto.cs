class Crypto
{

    private int _cezarShift = 1;

    public char cipher(char ch, int shift)
    {
        if (!char.IsLetter(ch))
        {

            return ch;
        }

        return (char)(ch + shift);
    }

    public string cezarCode(string input)
    {
        string output = string.Empty;

        foreach (char ch in input)
            output += cipher(ch, _cezarShift);

        return output;
    }

    public string cezarDecode(string input)
    {
        string output = string.Empty;

        foreach (char ch in input)
            output += cipher(ch, -_cezarShift);

        return output;
    }
}