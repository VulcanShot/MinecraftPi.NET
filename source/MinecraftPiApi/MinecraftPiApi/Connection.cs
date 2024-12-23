using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MinecraftPiApi;
/// <summary>
/// Facilitates a TCP connection
/// </summary>
public class Connection : IDisposable
{
    /// <summary>
    /// The address to connect to
    /// </summary>
    public IPAddress Address { get; init; }
    /// <summary>
    /// The port to connect to
    /// </summary>
    public int Port { get; init; }
    /// <summary>
    /// The TCP socket used as client
    /// </summary>
    public Socket Client { get; init; }

    public Connection(IPAddress address, int port)
    {
        Address = address;
        Port = port;
        Client = new(AddressFamily.InterNetwork,
                     SocketType.Stream,
                     ProtocolType.Tcp);
    }

    /// <summary>
    /// Establish a TCP connection to <see href="Address"/> and <see href="Port"/>
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void Connect()
    {
        try
        {
            IPEndPoint endPoint = new IPEndPoint(Address, Port);
            Client.Connect(endPoint);
        }
        catch (SocketException ex)
        {
            throw new InvalidOperationException("Could not connect to the server.", ex);
        }
    }

    public void Send(string command, params string[] args)
    {
        CheckConnection();
        string request = $"{command}({string.Join(",", args)})\n";
        byte[] bytes = Encoding.UTF8.GetBytes(request);
        Client.Send(bytes, SocketFlags.None);
    }

    public void Send(string command, params float[] args)
    {
        Send(command, Array.ConvertAll(args, Convert.ToString));
    }

    public void Send(string command)
    {
        Send(command, "");
    }

    private string Receive()
    {
        CheckConnection();
        var buffer = new byte[1024];
        int received = Client.Receive(buffer, SocketFlags.None);
        return Encoding.ASCII.GetString(buffer, 0, received).Trim('\n');
    }

    public string SendReceive(string command, params string[] args)
    {
        Send(command, args);
        return Receive();
    }

    public string SendReceive(string command, params float[] args)
    {
        return SendReceive(command, Array.ConvertAll(args, Convert.ToString));
    }

    public string SendReceive(string command)
    {
        return SendReceive(command, "");
    }

    private void CheckConnection()
    {
        if (!Client.Connected)
        {
            throw new NotSupportedException("The client is not connected");
        }
    }

    public void Dispose()
    {
        Client?.Close();
        Client?.Dispose();
    }
}
