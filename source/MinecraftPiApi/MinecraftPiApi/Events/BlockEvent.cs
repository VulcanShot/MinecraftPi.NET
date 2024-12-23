using System.Numerics;

namespace MinecraftPiApi.Events;
public class BlockEvent(Vector3 position, int face, int entityId)
{
    public Vector3 Position { get; init; } = position;
    public int Face { get; init; } = face;
    public int EntityId { get; init; } = entityId;
}