using System.Runtime.InteropServices;

namespace WinAppModelHelpers;

[StructLayout(LayoutKind.Sequential)]
public struct PackageVersion
{
    // NOTE: MUST match memory layout of PACKAGE_VERSION in appmodel.h
    public ushort Revision;
    public ushort Build;
    public ushort Minor;
    public ushort Major;

    // Create an instance with the value `major.0.0.0`.
    public PackageVersion(ushort major) :
        this(major, 0, 0, 0)
    { }

    // Create an instance with the value `major.minor.0.0`.
    public PackageVersion(ushort major, ushort minor) :
        this(major, minor, 0, 0)
    { }

    // Create an instance with the value `major.minor.build.0`.
    public PackageVersion(ushort major, ushort minor, ushort build) :
        this(major, minor, build, 0)
    { }

    // Create an instance with the value `major.minor.build.revision`.
    public PackageVersion(ushort major, ushort minor, ushort build, ushort revision)
    {
        Major = major;
        Minor = minor;
        Build = build;
        Revision = revision;
    }

    // Create an instance from a version as a uint64.
    public PackageVersion(ulong version) :
        this((ushort)(version >> 48), (ushort)(version >> 32), (ushort)(version >> 16), (ushort)version)
    { }

    // Return the version as a uint64.
    public readonly ulong ToVersion()
    {
        return (ulong)Major << 48 | (ulong)Minor << 32 | (ulong)Build << 16 | Revision;
    }

    // Return the string as a formatted value "major.minor.build.revision".
    public readonly override string ToString()
    {
        return $"{Major}.{Minor}.{Build}.{Revision}";
    }
}
