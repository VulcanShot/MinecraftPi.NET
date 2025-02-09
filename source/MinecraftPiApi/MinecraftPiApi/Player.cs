namespace MinecraftPiApi;
public class Player(Connection connection) : EntityHandler(connection, "player")
{
    /// <summary>
    /// Set the <paramref name="value"/> of the host's player <paramref name="setting"/>.
    /// </summary>
    /// <param name="setting">
    /// <list type="bullet">
    /// <listheader>
    /// <term>List of known keys:</term>
    /// </listheader>
    /// <item>
    /// <term>autojump</term>
    /// </item>
    /// </list>
    /// </param>
    /// <param name="value">The value associated to <paramref name="setting"/></param>
    public void Setting(string setting, bool value)
    {
        Connection.Send($"{Prefix}.setting", setting, Convert.ToInt32(value).ToString());
    }
}