using System;
using System.Runtime.InteropServices;

namespace WinAppModelHelpers;

public static class Win11DynamicDependency
{
    public enum PackageDependencyProcessorArchitectures
    {
        PackageDependencyProcessorArchitectures_None,
        PackageDependencyProcessorArchitectures_Neutral,
        PackageDependencyProcessorArchitectures_X86,
        PackageDependencyProcessorArchitectures_X64,
        PackageDependencyProcessorArchitectures_Arm,
        PackageDependencyProcessorArchitectures_Arm64,
        PackageDependencyProcessorArchitectures_X86A64
    }

    public enum PackageDependencyLifetimeKind
    {
        PackageDependencyLifetimeKind_Process,
        PackageDependencyLifetimeKind_FilePath,
        PackageDependencyLifetimeKind_RegistryKey
    }

    [Flags]
    public enum CreatePackageDependencyOptions
    {
        CreatePackageDependencyOptions_None,
        CreatePackageDependencyOptions_DoNotVerifyDependencyResolution,
        CreatePackageDependencyOptions_ScopeIsSystem
    }

    [Flags]
    public enum AddPackageDependencyOptions
    {
        AddPackageDependencyOptions_None,
        AddPackageDependencyOptions_PrependIfRankCollision
    }

    [Flags]
    public enum AddPackageDependencyOptions2
    {
        AddPackageDependencyOptions2_None,
        AddPackageDependencyOptions2_PrependIfRankCollision,
        AddPackageDependencyOptions2_SpecifiedPackageFamilyOnly
    }

    public static class NativeMethods
    {
        [DllImport(CommonNativeInterop.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern int TryCreatePackageDependency(
            IntPtr user, string packageFamilyName, PackageVersion minVersion, PackageDependencyProcessorArchitectures packageDependencyProcessorArchitectures,
            PackageDependencyLifetimeKind lifetimeKind, string lifetimeArtifact, CreatePackageDependencyOptions options, out string packageDependencyId);

        [DllImport(CommonNativeInterop.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern int AddPackageDependency(
            string packageDependencyId, int rank, AddPackageDependencyOptions options, IntPtr packageDependencyContext, out string packageFullName);

        [DllImport(CommonNativeInterop.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern int AddPackageDependency2(
            string packageDependencyId, int rank, AddPackageDependencyOptions2 options, IntPtr packageDependencyContext, out string packageFullName);

        [DllImport(CommonNativeInterop.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern int DeletePackageDependency(string packageDependencyId);

        [DllImport(CommonNativeInterop.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern int RemovePackageDependency(IntPtr packageDependencyContext);
    }

    public static bool IsApiPresent
    {
        get
        {
            IntPtr hlib = CommonNativeInterop.LoadLibraryExW(CommonNativeInterop.KernelBase, IntPtr.Zero, 0);
            if (hlib == IntPtr.Zero) return false;
            try
            {
                return CommonNativeInterop.GetProcAddress(hlib, "AddPackageDependency") != IntPtr.Zero;
            }
            finally
            {
                CommonNativeInterop.FreeLibrary(hlib);
            }
        }
    }
}
