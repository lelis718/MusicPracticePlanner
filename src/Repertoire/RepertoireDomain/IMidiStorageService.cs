using Microsoft.AspNetCore.Http;
using RepertoireDomain.ValueObjects;

namespace RepertoireDomain;

public interface IMidiStorageService
{
    MidiFile Save(IFormFile file);
}
