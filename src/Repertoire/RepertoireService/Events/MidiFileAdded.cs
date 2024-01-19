using MusicPracticePlanner.Base.ServicePrimitives.Integration;

namespace RepertoireService;

public record MidiFileAdded(Guid musicId, string midiUrl): IIntegrationEvent{}
