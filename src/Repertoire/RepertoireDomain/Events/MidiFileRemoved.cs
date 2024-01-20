using MusicPracticePlanner.Base.DomainPrimitives;

namespace RepertoireDomain.Events;

public record MidiFileRemoved(Guid Id, string MidiFileUrl) : IDomainEvent;
