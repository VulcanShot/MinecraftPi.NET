using System.Net;

using MinecraftPiApi;

namespace MinecraftPiApi.Demo;

internal class Program
{
    static void Main(string[] args)
    {
        Connection connection = new(IPAddress.Loopback, 1337);
        connection.Send("Hello World");
    }
}
