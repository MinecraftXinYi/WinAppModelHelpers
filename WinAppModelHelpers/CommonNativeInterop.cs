using System;
using System.Runtime.InteropServices;

namespace WinAppModelHelpers;

internal static class CommonNativeInterop
{
    internal const string
        KernelBase = "KernelBase.dll",
        MsWinAppModelL112 = "api-ms-win-appmodel-runtime-l1-1-2";

    [DllImport(KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern IntPtr LoadLibraryExW(string lpLibFileName, IntPtr hFile, uint dwFlags);

    [DllImport(KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport(KernelBase, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int FreeLibrary(IntPtr hLibModule);
}
