namespace VMP.AssemblyFormat;

/// <summary>
///     Contains the offset and the symbol at that offset.
/// </summary>
public class SegmentSymbol
{
    /// <summary>
    ///     Indicates if this symbol is intended to be used between sections.
    /// </summary>
    public readonly bool IsExported;

    /// <summary>
    ///     Offset Relative to the start of the segment.
    /// </summary>
    public readonly int Offset;

    /// <summary>
    ///     The Symbol that is at the given offset.
    /// </summary>
    public readonly string Symbol;

    /// <summary>
    ///     Creates a new SegmentSymbol.
    /// </summary>
    /// <param name="offset">The offset relative to the start of the segment.</param>
    /// <param name="symbol">The symbol at the given offset.</param>
    /// <param name="isExported">Indicates if this symbol is intended to be used between sections.</param>
    public SegmentSymbol(int offset, string symbol, bool isExported)
    {
        Offset = offset;
        Symbol = symbol;
        IsExported = isExported;
    }
}