using MusicPracticePlanner.Base.DomainPrimitives;

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
        music.AddMidiFile(midiFileUrl);
        
        this.Musics.Add(music);
        return music;
    }
    public static Repertoire Create(Guid studentId)
    {
        return new Repertoire(studentId);
    }

}
