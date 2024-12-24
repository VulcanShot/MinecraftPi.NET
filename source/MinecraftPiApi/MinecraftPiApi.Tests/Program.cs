namespace MinecraftPiApi.Demo;

internal class Program
{
    static void Main(string[] args)
    {
        using var minecraft = Minecraft.Create();
        minecraft.PostToChat("Hello, Minecraft!");
    }
}
