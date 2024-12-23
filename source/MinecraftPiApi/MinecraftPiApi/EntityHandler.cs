using System.Numerics;

using MinecraftPiApi.Extensions;

namespace MinecraftPiApi;
public class EntityHandler : MinecraftBase
{
    public EntityHandler(Connection connection, string prefix = "entity") : base(connection, prefix)
    {
        Connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    /// <summary>
    /// Gets the position of the entity
    /// </summary>
    /// <param name="entityId">The ID of the entity in question</param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public Vector3 GetPosition(int entityId)
    {
        string response = Connection.SendReceive($"{prefix}.getPos", entityId);
        return VectorUtils.FromString(response);
    }

    /// <summary>
    /// Sets the position of the entity
    /// </summary>
    /// <param name="entityId">The ID of the entity in question</param>
    /// <param name="position">The <see cref="Vector3"/> position to which the entity's position will be set to</param>
    public void SetPosition(int entityId, Vector3 position)
    {
        Connection.Send($"{prefix}.SetPos", entityId, position.X, position.Y, position.Z);
    }

    /// <summary>
    /// Gets the position of the tile on which the entity is on.
    /// </summary>
    /// <param name="entityId">The ID of the entity in question</param>
    /// <returns></returns>
    public Vector3 GetTilePosition(int entityId)
    {
        string response = Connection.SendReceive($"{prefix}.getTile", entityId);
        return VectorUtils.FromString(response);
    }

    /// <summary>
    /// Move the entity to a tile located in <paramref name="position"/>.
    /// </summary>
    /// <param name="entityId">The ID of the entity in question</param>
    /// <param name="position">The <see cref="Vector3"/> position to which the entity's position will be set to</param>
    public void SetTilePosition(int entityId, Vector3 position)
    {
        Vector3 flooredPosition = position.Floor();
        Connection.Send($"{prefix}.SetTile", entityId, flooredPosition.X, flooredPosition.Y, flooredPosition.Z);
    }
}