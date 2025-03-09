using System;
using System.Runtime.InteropServices;

namespace WinAppModelHelpers;

internal static class CommonNativeMethods
{
    [DllImport(ConstApiSetNames.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern IntPtr LoadLibraryExW(string lpLibFileName, IntPtr hFile, uint dwFlags);

    [DllImport(ConstApiSetNames.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport(ConstApiSetNames.KernelBase, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int FreeLibrary(IntPtr hLibModule);
}
