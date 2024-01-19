namespace RepertoireDomain.ValueObjects;

public class MidiFile
{
    public MidiFile(string fileUrl)
    {
        FileUrl = fileUrl;
    }
    public string FileUrl { get; private set; }
}
