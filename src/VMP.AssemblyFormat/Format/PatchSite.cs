namespace VMP.AssemblyFormat;

/// <summary>
///     Describes a location inside the segment data(after load strategy like zeropad etc) that needs to be patched with a
///     value.
/// </summary>
public class PatchSite
{
    /// <summary>
    ///     The offset relative to the start of the segment data.
    /// </summary>
    public readonly int Offset;

    /// <summary>
    ///     The size of the value that will be patched into the segment data.
    /// </summary>
    public readonly int Size;

    /// <summary>
    ///     The Symbol that will be resolved and patched into the segment data.
    /// </summary>
    public readonly Symbol Symbol;

    /// <summary>
    ///     Creates a new PatchSite.
    /// </summary>
    /// <param name="symbol">The Symbol that will be looked up at load time.</param>
    /// <param name="offset">The Offset relative to the start of the segment data</param>
    /// <param name="size">The Final Size of the segment as it is loaded in memory</param>
    public PatchSite(Symbol symbol, int offset, int size)
    {
        Symbol = symbol;
        Offset = offset;
        Size = size;
    }
}