using MusicPracticePlanner.Base.DomainPrimitives;
using RepertoireDomain.Events;

namespace RepertoireDomain.Entities;

public class Repertoire : Entity
{
    protected Repertoire(Guid studentId) : base(Guid.NewGuid())
    {
        Musics = new List<Music>();
        StudentId = studentId;
    }

    public Guid StudentId { get; private set; }

    public IList<Music> Musics { get; private set; }


    public Music AddMusic(string name, string midiFileUrl)
    {
        Music music = Music.Create(this, name);
        music.UpdateMidiFile(midiFileUrl);
        this.Musics.Add(music);
        this.Raise(new ABCNotationConversionNeeded(music.Id, midiFileUrl));

        return music;
    }
    public static Repertoire Create(Guid studentId)
    {
        return new Repertoire(studentId);
    }

    public void UpdateMusic(Guid musicId, string name, string? abcNotation, string? midiFileUrl)
    {
        Music? music = Musics.Where(i => i.Id == musicId).FirstOrDefault();
        if (music == null) throw new Exception("Music not found");

        music.UpdateName(name);

        if (midiFileUrl != null)
        {
            music.UpdateMidiFile(midiFileUrl);
        }
        if (abcNotation != null)
        {
            music.UpdateAbcNotation(abcNotation);
        }
        else if (midiFileUrl != null)
        {
            this.Raise(new ABCNotationConversionNeeded(music.Id, midiFileUrl));
        }
    }

}
