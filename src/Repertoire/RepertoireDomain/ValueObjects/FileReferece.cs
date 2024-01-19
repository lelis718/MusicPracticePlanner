namespace RepertoireDomain.ValueObjects;

public class FileReferece
{
    public FileReferece(string url)
    {
        Url = url;
    }
    public string Url { get; private set; }
}
