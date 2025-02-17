﻿using System.Net;
using System.Numerics;

using MinecraftPiApi.Extensions;

namespace MinecraftPiApi;
public class Minecraft : MinecraftBase, IDisposable
{
    private const int DEFAULT_PORT = 4711;
    private static readonly IPAddress defaultAddress = IPAddress.Loopback;
    public Camera CameraHandler { get; init; }
    public Player Player { get; init; }
    public EntityHandler EntityHandler { get; init; }
    public EventHandler EventHandler { get; init; }

    private Minecraft(IPAddress address, int port) : base(new Connection(address, port), "world")
    {
        CameraHandler = new(Connection);
        Player = new(Connection);
        EntityHandler = new(Connection);
        EventHandler = new(Connection);
    }

    /// <summary>
    /// Returns an instance of <see cref="Minecraft"/> with the default <see cref="IPAddress"/> and port for connecting with the game.
    /// </summary>
    /// <returns></returns>
    public static Minecraft Create() => Create(defaultAddress, DEFAULT_PORT);

    /// <summary>
    /// Returns a new instance of <see cref="Minecraft"/>.
    /// </summary>
    /// <param name="address"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    public static Minecraft Create(IPAddress address, int port = DEFAULT_PORT)
    {
        Minecraft minecraft = new(address, port);
        minecraft.Connection.Connect();
        return minecraft;
    }

    public BlockId GetBlockId(Vector3 position)
    {
        position = position.Floor();
        string response = Connection.SendReceive($"{Prefix}.getBlock", position.X, position.Y, position.Z);
        return (BlockId)int.Parse(response);
    }

    public Block GetBlock(Vector3 position)
    {
        position = position.Floor();
        string response = Connection.SendReceive($"{Prefix}.getBlockWithData", position.X, position.Y, position.Z);
        int[] blockInfo = Array.ConvertAll(response.Split(DATA_SEPARATOR), int.Parse);
        return new Block(blockInfo[0], blockInfo[1]);
    }

    public void SetBlock(Vector3 position, BlockId id)
    {
        position = position.Floor();
        Connection.Send($"{Prefix}.setBlock", position.X, position.Y, position.Z, (int)id);
    }

    public void SetBlock(Vector3 position, BlockId id, int data)
    {
        position = position.Floor();
        Connection.Send($"{Prefix}.setBlock", position.X, position.Y, position.Z, (int)id, data);
    }

    public void SetBlocks(Vector3 minBounds, Vector3 maxBounds, BlockId id)
    {
        minBounds = minBounds.Floor();
        maxBounds = maxBounds.Floor();
        Connection.Send($"{Prefix}.setBlock", minBounds.X, minBounds.Y, minBounds.Z, maxBounds.X, maxBounds.Y, maxBounds.Z, (int)id);
    }

    public void SetBlocks(Vector3 minBounds, Vector3 maxBounds, BlockId id, int data)
    {
        minBounds = minBounds.Floor();
        maxBounds = maxBounds.Floor();
        Connection.Send($"{Prefix}.setBlock", minBounds.X, minBounds.Y, minBounds.Z, maxBounds.X, maxBounds.Y, maxBounds.Z, (int)id, data);
    }

    public int GetHeight(Vector2 groundPlane)
    {
        groundPlane = groundPlane.Floor();
        string response = Connection.SendReceive($"{Prefix}.getHeight", groundPlane.X, groundPlane.Y);
        return int.Parse(response);
    }

    public int[] GetPlayerEntityIds()
    {
        string response = Connection.SendReceive($"{Prefix}.getPlayerIds");
        return Array.ConvertAll(response.Split(OBJECT_SEPARATOR), int.Parse);
    }

    /// <summary>
    /// Save a checkpoint that can be used for restoring the world
    /// </summary>
    public void SaveCheckpoint()
    {
        Connection.Send($"{Prefix}.checkpoint.save");
    }

    /// <summary>
    /// Restore the world state to the checkpoint
    /// </summary>
    public void UseCheckpoint()
    {
        Connection.Send($"{Prefix}.checkpoint.restore");
    }

    public void PostToChat(string message)
    {
        Connection.Send($"chat.post", message);
    }

    /// <summary>
    /// Set the <paramref name="value"/> of the world's <paramref name="setting"/>.
    /// </summary>
    /// <param name="setting">List of known keys: world_immutable, nametags_visible</param>
    /// <param name="value"></param>
    public void Setting(string setting, bool value)
    {
        Connection.Send($"{Prefix}.setting", setting, Convert.ToInt32(value).ToString());
    }

    public void Dispose()
    {
        Connection.Dispose();
        GC.SuppressFinalize(this);
    }
}