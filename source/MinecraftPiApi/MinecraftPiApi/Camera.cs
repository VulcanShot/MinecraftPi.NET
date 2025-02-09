using System.Numerics;

namespace MinecraftPiApi;
public class Camera(Connection connection) : MinecraftBase(connection, "camera")
{
    public void SetNormal(int entityId)
    {
        Connection.Send($"{Prefix}.setNormal", entityId);
    }

    public void SetFixed(int entityId)
    {
        Connection.Send($"{Prefix}.setFixed");
    }

    public void SetFollow(int entityId)
    {
        Connection.Send($"{Prefix}.setFollow", entityId);
    }

    public void SetPosition(Vector3 position)
    {
        Connection.Send($"{Prefix}.setPos", position.X, position.Y, position.Z);
    }
}