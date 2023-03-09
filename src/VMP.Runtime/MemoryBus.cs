using System;
using System.Collections.Generic;
using System.Linq;

namespace VMP.Runtime;

/// <summary>
///     Implements a simple Memory Bus.
/// </summary>
public class MemoryBus
{
    /// <summary>
    ///     Defines the start of the peripheral range. All peripherals must be mapped to addresses above or equal to this
    ///     value.
    /// </summary>
    public const ulong PERIPHERAL_RANGE_START = 0b1000_0000_0000_0000_0000_0000_0000_0000;

    /// <summary>
    ///     The Block Memory Array
    /// </summary>
    private readonly byte[] m_BlockMemory;

    /// <summary>
    ///     The list of memory maps.
    /// </summary>
    private readonly List<MemoryMap> m_MemoryMaps = new List<MemoryMap>();

    /// <summary>
    ///     Constructs a new Memory Bus.
    /// </summary>
    /// <param name="blockMemorySize">The Amount of block memory that will be allocated. default: 0x80000(512kb)</param>
    public MemoryBus(int blockMemorySize = 0x80000)
    {
        m_BlockMemory = new byte[blockMemorySize];
    }

    /// <summary>
    ///     Returns true if the given address and size overlap with any of the memory maps.
    /// </summary>
    /// <param name="address">Address to check</param>
    /// <param name="size">Size to check</param>
    /// <returns>True if the given address and size overlap with any of the memory maps.</returns>
    private bool IsOverlapping(ulong address, ulong size)
    {
        return m_MemoryMaps.Any(map => map.Start + map.Size <= address || map.Start >= address + size);
    }


    /// <summary>
    ///     Maps a peripheral to a given address.
    /// </summary>
    /// <param name="address">Address to map the peripheral to.</param>
    /// <param name="peripheral">The peripheral to map.</param>
    /// <returns>This MemoryBus Instance</returns>
    /// <exception cref="InvalidOperationException">Gets thrown if the address is below the peripheral range start.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Gets thrown if the address and size overlap with any of the memory maps.</exception>
    public MemoryBus Map(ulong address, MemoryPeripheral peripheral)
    {
        if (address < PERIPHERAL_RANGE_START)
        {
            throw new InvalidOperationException("Address is below the peripheral range start.");
        }

        if (IsOverlapping(address, peripheral.Size))
        {
            throw new ArgumentOutOfRangeException(nameof(address), "Memory Map Overlap");
        }

        m_MemoryMaps.Add(new MemoryMap(address, peripheral));

        return this;
    }

    /// <summary>
    ///     Reads a byte from the given address.
    /// </summary>
    /// <param name="address">The address to read from.</param>
    /// <returns>The byte at the given address.</returns>
    /// <exception cref="InvalidOperationException">Gets thrown if the address is not mapped to any peripheral.</exception>
    public byte ReadByte(ulong address)
    {
        if (address < PERIPHERAL_RANGE_START)
        {
            return m_BlockMemory[address];
        }

        MemoryMap? map = m_MemoryMaps.FirstOrDefault(m => m.Start <= address && m.Start + m.Size > address);
        if (map == null)
        {
            throw new InvalidOperationException("Memory Map not found");
        }

        return map.Value.Peripheral.ReadByte(address - map.Value.Start);
    }

    /// <summary>
    ///     Writes a byte to the given address.
    /// </summary>
    /// <param name="address">The address to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <exception cref="InvalidOperationException">Gets thrown if the address is not mapped to any peripheral.</exception>
    public void WriteByte(ulong address, byte value)
    {
        if (address < PERIPHERAL_RANGE_START)
        {
            m_BlockMemory[address] = value;

            return;
        }

        MemoryMap? map = m_MemoryMaps.FirstOrDefault(m => m.Start <= address && m.Start + m.Size > address);
        if (map == null)
        {
            throw new InvalidOperationException("Memory Map not found");
        }

        map.Value.Peripheral.WriteByte(address - map.Value.Start, value);
    }

    /// <summary>
    ///     Defines a struct that maps a memory peripheral to a start address.
    /// </summary>
    private readonly struct MemoryMap
    {
        /// <summary>
        ///     The peripheral that is mapped to the start address.
        /// </summary>
        public readonly MemoryPeripheral Peripheral;

        /// <summary>
        ///     The start address of the peripheral.
        /// </summary>
        public readonly ulong Start;

        /// <summary>
        ///     The size of the peripheral.
        /// </summary>
        public ulong Size => Peripheral.Size;

        /// <summary>
        ///     Constructs a new Memory Map.
        /// </summary>
        /// <param name="start">The start address of the peripheral.</param>
        /// <param name="peripheral">The peripheral that is mapped to the start address.</param>
        public MemoryMap(ulong start, MemoryPeripheral peripheral)
        {
            Start = start;
            Peripheral = peripheral;
        }
    }
}