using System.Numerics;

using MinecraftPiApi.Events;

namespace MinecraftPiApi;
public class EventHandler(Connection connection) : MinecraftBase(connection, "events")
{
    public IEnumerable<BlockEvent> GetBlockHits()
    {
        string response = connection.SendReceive($"{prefix}.blocks.hits");
        string[] events = response.Split(OBJECT_SEPARATOR);
        return events.Select(e =>
        {
            string[] parameters = e.Split(DATA_SEPARATOR);
            Vector3 position = new(int.Parse(parameters[0]), int.Parse(parameters[1]), int.Parse(parameters[2]));
            return new BlockEvent(position, int.Parse(parameters[3]), int.Parse(parameters[4]));
        });
    }

    public void ClearAll()
    {
        connection.Send($"{prefix}.clear");
    }
}