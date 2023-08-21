using System.Runtime.CompilerServices;

namespace SubStringConcat;

public static class Extensions
{
    /// <summary>
    /// Creates a new string, which is a substring of the first, using the supplied range and concatenated with the second.
    /// </summary>
    /// <param name="s">The string to substring</param>
    /// <param name="range">The range of the substring</param>
    /// <param name="postString">The string to concatenate to the first substring</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string SubCat(this string s, Range range, ReadOnlySpan<char> postString)
    {
        var (offset, length) = range.GetOffsetAndLength(s.Length);
        return string.Concat(s.AsSpan(offset, length), postString);
    }
}