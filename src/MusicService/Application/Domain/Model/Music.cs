using L718Framework.Core.Domain.Model;

namespace MusicPracticePlanner.MusicService.Application.Domain.Model;

public class Music : EntityBase
{    
    public Music(string name, string pdfFile){
        this.Name = name;
        this.PdfFile = pdfFile;
    }

    public string Name{get; private set;}

    public string PdfFile{get; private set;}

    public void Update(string name, string pdfFile)
    {
        this.Name = name;
        this.PdfFile = pdfFile;
    }

    public static Music Create(string name, string pdfFile){
        return new Music(name, pdfFile);
    }

}