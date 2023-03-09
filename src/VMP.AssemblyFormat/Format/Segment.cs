using System;
using System.Collections.Generic;
using System.Linq;

namespace VMP.AssemblyFormat;

/// <summary>
///     Implements a simple Segment. Has a name, a size, a type, a collection of symbols, a collection of patch sites and a
///     data array.
/// </summary>
public class Segment
{
    /// <summary>
    ///     The patch sites of this segment.
    /// </summary>
    private readonly PatchSite[] m_PatchSites;

    /// <summary>
    ///     The symbols of this segment.
    /// </summary>
    private readonly SegmentSymbol[] m_Symbols;

    /// <summary>
    ///     The module this segment is attached to.
    /// </summary>
    private Module? m_Module;


    /// <summary>
    ///     Constructs a new segment.
    /// </summary>
    /// <param name="name">Name of the segment</param>
    /// <param name="size">Final size of the segment</param>
    /// <param name="type">Segment Type Flags</param>
    /// <param name="symbols">Segment Symbols</param>
    /// <param name="data">Segment Data</param>
    /// <param name="patchSites">Patch Sites</param>
    public Segment(string name, int size, SegmentType type, SegmentSymbol[] symbols, byte[] data, PatchSite[] patchSites)
    {
        m_Symbols = symbols;
        Name = name;
        Size = size;
        Type = type;
        Data = data;
        m_PatchSites = patchSites;
    }

    /// <summary>
    ///     The module this segment is attached to.
    /// </summary>
    /// <exception cref="InvalidOperationException">Gets thrown if the Segment is not attached to a Module</exception>
    public Module Module => m_Module ?? throw new InvalidOperationException("Segment is not attached to a module");

    /// <summary>
    ///     The name of this segment.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     The final size of this segment.
    /// </summary>
    public int Size { get; }

    /// <summary>
    ///     The symbols of this segment.
    /// </summary>
    public IEnumerable<SegmentSymbol> SegmentSymbols => m_Symbols;

    /// <summary>
    ///     The fully qualified symbols of this segment.
    /// </summary>
    public IEnumerable<Symbol> Symbols => m_Symbols.Select(x => new Symbol(Module.Assembly.Information.Name, Module.Name, Name, x.Symbol, x.IsExported));

    /// <summary>
    ///     The patch sites of this segment.
    /// </summary>
    public IEnumerable<PatchSite> PatchSites => m_PatchSites;

    /// <summary>
    ///     The type of this segment.
    /// </summary>
    public SegmentType Type { get; }

    /// <summary>
    ///     The data of this segment.
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    ///     Attaches this segment to a module.
    /// </summary>
    /// <param name="module"></param>
    internal void Attach(Module module)
    {
        m_Module = module;
    }
}