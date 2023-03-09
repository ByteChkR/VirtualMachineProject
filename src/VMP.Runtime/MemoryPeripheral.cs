namespace VMP.Runtime;

/// <summary>
/// Abstract class for a memory peripheral.
/// Defines Read and Write Byte methods.
/// Defines a Size property.
/// </summary>
public abstract class MemoryPeripheral
{
    public abstract ulong Size { get; }
    public abstract byte ReadByte(ulong address);
    public abstract void WriteByte(ulong address, byte value);
}