using MusicPracticePlanner.Base.DomainPrimitives;
using RepertoireDomain.Events;
using RepertoireDomain.ValueObjects;

namespace RepertoireDomain.Entities;

public class Music : Entity
{
    public Music(Guid id, string name, Repertoire repertoire) : base(id)
    {
        Name = name;
        Repertoire = repertoire;
    }

    public string Name { get; private set; }
    public Repertoire Repertoire { get; private set; }
    public ABCNotation? ABCNotation { get; private set; }
    public MidiFile? MidiFile { get; private set; }

    public static Music Create(Repertoire repertoire, string name)
    {
        return new Music(Guid.NewGuid(), name, repertoire);
    }

    public void UpdateMidiFile(string fileUrl)
    {
        if(MidiFile != null){
            this.Raise(new MidiFileRemoved(Id, MidiFile.FileUrl));
        }
        this.MidiFile = new MidiFile(fileUrl);
    }

    public void UpdateAbcNotation(string notationString)
    {
        this.ABCNotation = new ABCNotation(notationString);
    }

    public void UpdateName(string name)
    {
        this.Name = name;
    }
}
