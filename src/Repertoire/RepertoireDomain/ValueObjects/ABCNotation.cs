namespace RepertoireDomain.ValueObjects;

public class ABCNotation
{
    public ABCNotation(string notationString)
    {
        NotationString = notationString;
    }
    public string NotationString { get; private set; }
}