namespace VMP.Runtime;

/// <summary>
/// Implements a simple Virtual Machine.
/// </summary>
public class VirtualMachine
{
    /// <summary>
    /// The Attached Memory Bus
    /// </summary>
    private readonly MemoryBus m_MemoryBus;
        
    /// <summary>
    /// Constructs a new Virtual Machine.
    /// </summary>
    /// <param name="memoryBus">The MemoryBus to attach to.</param>
    public VirtualMachine(MemoryBus memoryBus)
    {
        m_MemoryBus = memoryBus;
    }
}