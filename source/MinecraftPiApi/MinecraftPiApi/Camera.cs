using System.Numerics;

namespace MinecraftPiApi;
public class Camera(Connection connection) : MinecraftBase(connection, "camera")
{
    public void SetNormal(int entityId)
    {
        connection.Send($"{prefix}.setNormal", entityId);
    }

    public void SetFixed(int entityId)
    {
        connection.Send($"{prefix}.setFixed");
    }

    public void SetFollow(int entityId)
    {
        connection.Send($"{prefix}.setFollow", entityId);
    }

    public void SetPosition(Vector3 position)
    {
        connection.Send($"{prefix}.setPos", position.X, position.Y, position.Z);
    }
}