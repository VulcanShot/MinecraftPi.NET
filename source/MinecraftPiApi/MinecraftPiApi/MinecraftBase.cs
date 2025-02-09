namespace MinecraftPiApi;
public abstract class MinecraftBase(Connection connection, string prefix)
{
    protected const char OBJECT_SEPARATOR = '|';
    protected const char DATA_SEPARATOR = ',';
    protected readonly string Prefix = prefix;
    protected Connection Connection = connection;
}
