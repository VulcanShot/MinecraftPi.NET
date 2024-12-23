using System.Numerics;

namespace MinecraftPiApi;
public class Camera(Connection connection) : MinecraftBase(connection, "camera")
{
    public void SetNormal(int entityId)
    {
        Connection.Send($"{prefix}.setNormal", entityId);
    }

    public void SetFixed(int entityId)
    {
        Connection.Send($"{prefix}.setFixed");
    }

    public void SetFollow(int entityId)
    {
        Connection.Send($"{prefix}.setFollow", entityId);
    }

    public void SetPosition(Vector3 position)
    {
        Connection.Send($"{prefix}.setPos", position.X, position.Y, position.Z);
    }
}