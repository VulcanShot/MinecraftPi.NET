namespace MinecraftPiApi;
public abstract class MinecraftBase(Connection connection, string prefix)
{
    protected const char OBJECT_SEPARATOR = '|';
    protected const char DATA_SEPARATOR = ',';
    protected readonly string prefix = prefix;
    protected Connection connection = connection;
}
