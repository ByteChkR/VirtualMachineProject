using System;

namespace VMP.AssemblyFormat;

/// <summary>
///     Contains additional information about an assembly.
/// </summary>
public class AssemblyInformation
{
    /// <summary>
    ///     Creates a new Assembly Information Object
    /// </summary>
    /// <param name="name">Name of the Assembly</param>
    /// <param name="version">Version of the Assembly</param>
    /// <param name="guid">Guid of the Assembly</param>
    public AssemblyInformation(string name, Version version, Guid? guid = null)
    {
        Name = name;
        Guid = guid ?? Guid.NewGuid();
        Version = version;
    }

    /// <summary>
    ///     The name of the assembly.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     The Guid of the assembly.
    /// </summary>
    public Guid Guid { get; }

    /// <summary>
    ///     The Version of the assembly.
    /// </summary>
    public Version Version { get; }

    /// <summary>
    ///     The Qualified Name of the assembly.(Contains Name, Version and Guid)
    /// </summary>
    public string QualifiedName => $"{Name}, Version={Version}, Guid={Guid}";
}