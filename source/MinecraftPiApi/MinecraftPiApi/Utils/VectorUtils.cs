using System.Numerics;

namespace MinecraftPiApi.Extensions;
internal static class VectorUtils
{
    /// <summary>
    /// Returns a <see cref="Vector3"/> by parsing <paramref name="str"/>.
    /// </summary>
    /// <param name="str"></param>
    /// <returns>The <see cref="Vector3"/> parsed from the string.</returns>
    /// <exception cref="FormatException"></exception>
    internal static Vector3 FromString(string str)
    {
        string[] split = str.Split(',');
        if (split.Length != 3 ||
            !float.TryParse(split[0], out float x) ||
            !float.TryParse(split[1], out float y) ||
            !float.TryParse(split[2], out float z))
        {
            throw new FormatException("Invalid response format for position.");
        }

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Returns a new <see cref="Vector3"/> whose elements are the largest integral values that are less than or equal to the given <paramref name="vector"/> elements.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    internal static Vector3 Floor(this Vector3 vector)
    {
        return new Vector3((float)Math.Floor(vector.X),
                           (float)Math.Floor(vector.Y),
                           (float)Math.Floor(vector.Z));
    }

    /// <summary>
    /// Returns a new <see cref="Vector2"/> whose elements are the largest integral values that are less than or equal to the given <paramref name="vector"/> elements.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    internal static Vector2 Floor(this Vector2 vector)
    {
        return new Vector2((float)Math.Floor(vector.X), (float)Math.Floor(vector.Y));
    }
}
