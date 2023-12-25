namespace MusicPracticePlanner.Configuration;

public record CommonConfigurationSection
{
    public Vault Vault { get; init; }
}

public record Vault
{
    public string Database { get; init; }
    public string Storage { get; init; }
}