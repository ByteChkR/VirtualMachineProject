using System;

namespace VMP.AssemblyFormat;

/// <summary>
///     Segment Type Flags. Used to determine type and load behavior of a segment.
/// </summary>
[Flags]
public enum SegmentType
{
    /// <summary>
    ///     If this flag is present the segment is zero padded if the size is larger than the data.
    /// </summary>
    ZERO_PADDED = 1,

    /// <summary>
    ///     If this flag is present the segment is read only.
    /// </summary>
    READ_ONLY = 2,

    /// <summary>
    ///     If this flag is present the segment is executable. (And also READ_ONLY)
    /// </summary>
    CODE = 4 | READ_ONLY,

    /// <summary>
    ///     If this flag is present the segment is data.
    /// </summary>
    DATA = 8,

    /// <summary>
    ///     If this flag is present the segment is not mapped into memory.
    /// </summary>
    NOT_MAPPED = 16,
}