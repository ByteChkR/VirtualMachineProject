using System;

namespace VMP.AssemblyFormat;

/// <summary>
///     Implements a Symbol that can be used to reference a specific location in the assembly.
/// </summary>
public class Symbol
{
    /// <summary>
    ///     The separator used to separate the parts of the qualified name.
    /// </summary>
    public const string PART_SEPARATOR = "::";

    /// <summary>
    ///     Creates a new Symbol.
    /// </summary>
    /// <param name="assemblyName">The name of the assembly.</param>
    /// <param name="moduleName">The name of the module.</param>
    /// <param name="segmentName">The name of the segment.</param>
    /// <param name="name">The name of the symbol.</param>
    /// <param name="isExported">Indicates if this symbol is intended to be used between sections.</param>
    public Symbol(string assemblyName, string moduleName, string segmentName, string name, bool isExported)
    {
        AssemblyName = assemblyName;
        ModuleName = moduleName;
        SegmentName = segmentName;
        Name = name;
        IsExported = isExported;
    }

    /// <summary>
    ///     The name of the assembly.
    /// </summary>
    public string AssemblyName { get; }

    /// <summary>
    ///     The name of the module.
    /// </summary>
    public string ModuleName { get; }

    /// <summary>
    ///     The name of the segment.
    /// </summary>
    public string SegmentName { get; }

    /// <summary>
    ///     The name of the symbol.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Indicates if this symbol is intended to be used between sections.
    /// </summary>
    public bool IsExported { get; }

    /// <summary>
    ///     The qualified name of the symbol.
    /// </summary>
    public string QualifiedName => $"{(IsExported ? "@" : "")}{AssemblyName}{PART_SEPARATOR}{ModuleName}{PART_SEPARATOR}{SegmentName}{PART_SEPARATOR}{Name}";

    /// <summary>
    ///     Returns the qualified name of the symbol.
    /// </summary>
    /// <returns>Qualified Name</returns>
    public override string ToString()
    {
        return QualifiedName;
    }

    /// <summary>
    ///     Parses a qualified name into a symbol.
    /// </summary>
    /// <param name="qualifiedName">:: separated qualified name.</param>
    /// <returns>Symbol</returns>
    /// <exception cref="ArgumentException">Gets thrown if the qualified name is invalid.</exception>
    public static Symbol Parse(string qualifiedName)
    {
        string[] parts = qualifiedName.Split(new[] { PART_SEPARATOR }, StringSplitOptions.None);
        if (parts.Length != 4)
        {
            throw new ArgumentException("Invalid qualified name", nameof(qualifiedName));
        }

        bool isExported = parts[0].StartsWith("@");
        if (isExported)
        {
            parts[0] = parts[0].Substring(1);
        }

        return new Symbol(parts[0], parts[1], parts[2], parts[3], isExported);
    }
}