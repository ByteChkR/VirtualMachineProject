using System;
using System.Collections.Generic;

namespace VMP.AssemblyFormat;

/// <summary>
///     Implements a simple Module. Has a name and a collection of segments. Can only be used if attached to an assembly.
/// </summary>
public class Module
{
    /// <summary>
    ///     The segments of this module.
    /// </summary>
    private readonly Segment[] m_Segments;

    /// <summary>
    ///     The assembly this module is attached to.
    /// </summary>
    private Assembly? m_Assembly;

    /// <summary>
    ///     Constructs a new module.
    /// </summary>
    /// <param name="name">Module Name</param>
    /// <param name="segments">Segment Collection</param>
    public Module(string name, Segment[] segments)
    {
        Name = name;
        m_Segments = segments;
        foreach (Segment segment in m_Segments)
        {
            segment.Attach(this);
        }
    }

    /// <summary>
    ///     The Assembly this module is attached to.
    /// </summary>
    /// <exception cref="InvalidOperationException">Gets thrown if the Module is not attached to an Assembly</exception>
    public Assembly Assembly => m_Assembly ?? throw new InvalidOperationException("Module is not attached to an assembly");

    /// <summary>
    ///     The name of this module.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Collection of segments of this module.
    /// </summary>
    public IEnumerable<Segment> Segments => m_Segments;

    /// <summary>
    ///     Attaches this module to an assembly.
    /// </summary>
    /// <param name="assembly">The Assembly to attach to.</param>
    /// <exception cref="InvalidOperationException">Gets thrown if the Module is already attached to an Assembly</exception>
    internal void Attach(Assembly assembly)
    {
        if (m_Assembly != null)
        {
            throw new InvalidOperationException("Module is already attached to an assembly");
        }

        m_Assembly = assembly;
    }
}