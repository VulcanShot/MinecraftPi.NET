namespace MinecraftPiApi;
public abstract class MinecraftBase(Connection connection, string _prefix)
{
    protected const char OBJECT_SEPARATOR = '|';
    protected const char DATA_SEPARATOR = ',';
    protected readonly string prefix = _prefix;
    public Connection Connection { get; init; } = connection;
}
