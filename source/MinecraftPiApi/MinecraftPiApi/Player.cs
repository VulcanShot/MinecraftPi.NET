namespace MinecraftPiApi;
public class Player(Connection connection) : EntityHandler(connection, "player")
{
    /// <summary>
    /// Set the <paramref name="value"/> of the host's player <paramref name="setting"/>.
    /// </summary>
    /// <param name="setting">
    /// List of known keys:
    /// <list type="bullet">autojump</list>
    /// </param>
    /// <param name="value"></param>
    public void Setting(string setting, bool value)
    {
        connection.Send($"{prefix}.setting", setting, Convert.ToInt32(value).ToString());
    }
}