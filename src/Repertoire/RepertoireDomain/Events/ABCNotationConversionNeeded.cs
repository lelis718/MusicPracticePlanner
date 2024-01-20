using MusicPracticePlanner.Base.DomainPrimitives;

namespace RepertoireDomain.Events;


public record ABCNotationConversionNeeded(Guid MusicId, string MidiFileUrl ) : IDomainEvent;
