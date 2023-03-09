using System.Collections.Generic;

namespace VMP.AssemblyFormat;

/// <summary>
///     Implements a simple Assembly.
///     Has a name and a collection of modules.
/// </summary>
public class Assembly
{
    /// <summary>
    ///     The modules of this assembly.
    /// </summary>
    private readonly Module[] m_Modules;

    /// <summary>
    ///     Constructs a new assembly.
    /// </summary>
    /// <param name="information">The Assembly Information</param>
    /// <param name="modules">The Collection of modules contained in this assembly</param>
    public Assembly(AssemblyInformation information, Module[] modules)
    {
        Information = information;
        m_Modules = modules;
        foreach (Module module in m_Modules)
        {
            module.Attach(this);
        }
    }

    /// <summary>
    ///     The name of this assembly.
    /// </summary>
    public AssemblyInformation Information { get; }

    /// <summary>
    ///     The modules of this assembly.
    /// </summary>
    public IEnumerable<Module> Modules => m_Modules;
}