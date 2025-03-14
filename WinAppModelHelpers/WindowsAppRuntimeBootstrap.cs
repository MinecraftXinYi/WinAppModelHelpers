using System;
using System.Runtime.InteropServices;

namespace WinAppModelHelpers;

public static class WindowsAppRuntimeBootstrap
{
    public enum InitializeOptions : int
    {
        /// Default behavior
        None = 0,

        /// If not successful call DebugBreak()
        OnError_DebugBreak = 0x0001,

        /// If not successful call DebugBreak() if a debugger is attached to the process
        OnError_DebugBreak_IfDebuggerAttached = 0x0002,

        /// If not successful perform a fail-fast
        OnError_FailFast = 0x0004,

        /// If a compatible Windows App Runtime framework package is not found show UI
        OnNoMatch_ShowUI = 0x0008,

        /// Do nothing (do not error) if the process has package identity
        OnPackageIdentity_NOOP = 0x0010,
    }

    public static class NativeMethods
    {
        public const string WASBootstrapDll = "Microsoft.WindowsAppRuntime.Bootstrap.dll";

        [DllImport(WASBootstrapDll, EntryPoint = "MddBootstrapInitialize2", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
        public static extern void MddBootstrapInitialize2_Throw(uint majorMinorVersion, string versionTag, PackageVersion packageVersion, InitializeOptions options);

        [DllImport(WASBootstrapDll, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int MddBootstrapInitialize2(uint majorMinorVersion, string versionTag, PackageVersion packageVersion, InitializeOptions options);

        [DllImport(WASBootstrapDll, ExactSpelling = true)]
        public static extern void MddBootstrapShutdown();
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// This is equivalent to `Initialize(majorMinorVersion, null, new PackageVersion(), InitializeOptions.None)`.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @see Initialize(uint, string)
    /// @see Initialize(uint, string, PackageVersion)
    /// @see Initialize(uint, string, PackageVersion, InitializeOptions)
    /// @see Shutdown()
    public static void Initialize(uint majorMinorVersion)
    {
        Initialize(majorMinorVersion, null!);
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// This is equivalent to `Initialize(majorMinorVersion, versionTag, new PackageVersion(), InitializeOptions.None)`.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @param versionTag version tag (if any), e.g. "preview1".
    /// @see Initialize(uint)
    /// @see Initialize(uint, string, PackageVersion)
    /// @see Initialize(uint, string, PackageVersion, InitializeOptions)
    /// @see Shutdown()
    public static void Initialize(uint majorMinorVersion, string versionTag)
    {
        Initialize(majorMinorVersion, versionTag, new PackageVersion());
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// This is equivalent to `Initialize(majorMinorVersion, versionTag, minVersion, InitializeOptions.None)`.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @param versionTag version tag (if any), e.g. "preview1".
    /// @param minVersion the minimum version to use.
    /// @see Initialize(uint)
    /// @see Initialize(uint, string)
    /// @see Initialize(uint, string, PackageVersion, InitializeOptions)
    /// @see Shutdown()
    public static void Initialize(uint majorMinorVersion, string versionTag, PackageVersion minVersion)
    {
        NativeMethods.MddBootstrapInitialize2_Throw(majorMinorVersion, versionTag, minVersion, InitializeOptions.None);
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @param versionTag version tag (if any), e.g. "preview1".
    /// @param minVersion the minimum version to use.
    /// @param options optional behavior.
    /// @see Initialize(uint)
    /// @see Initialize(uint, string)
    /// @see Initialize(uint, string, PackageVersion)
    /// @see Shutdown()
    public static void Initialize(uint majorMinorVersion, string versionTag, PackageVersion minVersion, InitializeOptions options)
    {
        NativeMethods.MddBootstrapInitialize2_Throw(majorMinorVersion, versionTag, minVersion, options);
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    /// Failure returns false with the failure HRESULT in the hresult parameter.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// This is equivalent to `TryInitialize(majorMinorVersion, null, new PackageVersion(), InitializeOptions.None, hresult)`.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @retval true if successful, otherwise false is returned.
    /// @see TryInitialize(uint, string, out int)
    /// @see TryInitialize(uint, string, PackageVersion, InitializeOptions, out int)
    /// @see Shutdown()
    public static bool TryInitialize(uint majorMinorVersion, out int hresult)
    {
        return TryInitialize(majorMinorVersion, null!, out hresult);
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    /// Failure returns false with the failure HRESULT in the hresult parameter.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// This is equivalent to `TryInitialize(majorMinorVersion, versionTag, new PackageVersion(), InitializeOptions.None, hresult)`.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @param versionTag version tag (if any), e.g. "preview1".
    /// @retval true if successful, otherwise false is returned.
    /// @see TryInitialize(uint, out int)
    /// @see TryInitialize(uint, string, PackageVersion, out int)
    /// @see TryInitialize(uint, string, PackageVersion, InitializeOptions, out int)
    /// @see Shutdown()
    public static bool TryInitialize(uint majorMinorVersion, string versionTag, out int hresult)
    {
        var minVersion = new PackageVersion();
        return TryInitialize(majorMinorVersion, versionTag, minVersion, out hresult);
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    /// Failure returns false with the failure HRESULT in the hresult parameter.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// This is equivalent to `TryInitialize(majorMinorVersion, versionTag, minVersion, InitializeOptions.None, hresult)`.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @param versionTag version tag (if any), e.g. "preview1".
    /// @param minVersion the minimum version to use.
    /// @param options optional behavior.
    /// @param hresult the error code if an error occurred.
    /// @retval true if successful, otherwise false is returned.
    /// @see TryInitialize(uint, out int)
    /// @see TryInitialize(uint, string, out int)
    /// @see TryInitialize(uint, string, PackageVersion, out int)
    /// @see Shutdown()
    public static bool TryInitialize(uint majorMinorVersion, string versionTag, PackageVersion minVersion, out int hresult)
    {
        return TryInitialize(majorMinorVersion, versionTag, minVersion, InitializeOptions.None, out hresult);
    }

    /// Initialize the calling process to use Windows App SDK's framework package.
    /// Failure returns false with the failure HRESULT in the hresult parameter.
    ///
    /// Find a Windows App SDK framework package meeting the criteria and make it available
    /// for use by the current process. If multiple packages meet the criteria the best
    /// candidate is selected.
    ///
    /// @param majorMinorVersion major and minor version of Windows App SDK's framework package, encoded as `0xMMMMNNNN` where M=Major, N=Minor (e.g. 1.2 == 0x00010002).
    /// @param versionTag version tag (if any), e.g. "preview1".
    /// @param minVersion the minimum version to use.
    /// @param options optional behavior.
    /// @param hresult the error code if an error occurred.
    /// @retval true if successful, otherwise false is returned.
    /// @see TryInitialize(uint, out int)
    /// @see TryInitialize(uint, string, out int)
    /// @see TryInitialize(uint, string, PackageVersion, out int)
    /// @see Shutdown()
    public static bool TryInitialize(uint majorMinorVersion, string versionTag, PackageVersion minVersion, InitializeOptions options, out int hresult)
    {
        hresult = NativeMethods.MddBootstrapInitialize2(majorMinorVersion, versionTag, minVersion, options);
        return hresult >= 0;
    }

    /// Undo the changes made by Initialize().
    ///
    /// @warning Packages made available via `Initialize()` and
    ///          the Dynamic Dependencies API should not be used after this call.
    /// @see Initialize(uint)
    /// @see Initialize(uint, string)
    /// @see Initialize(uint, string, PackageVersion)
    /// @see Initialize(uint, string, PackageVersion, InitializeOptions options)
    /// @see TryInitialize(uint, out int)
    /// @see TryInitialize(uint, string, out int)
    /// @see TryInitialize(uint, string, PackageVersion, out int)
    /// @see TryInitialize(uint, string, PackageVersion, InitializeOptions options, out int)
    public static void Shutdown()
    {
        NativeMethods.MddBootstrapShutdown();
    }

    public static bool IsApiPresent
    {
        get
        {
            IntPtr hlib = CommonNativeInterop.LoadLibraryExW(NativeMethods.WASBootstrapDll, IntPtr.Zero, 0);
            if (hlib == IntPtr.Zero) return false;
            try
            {
                return CommonNativeInterop.GetProcAddress(hlib, "MddBootstrapInitialize2") != IntPtr.Zero;
            }
            finally
            {
                CommonNativeInterop.FreeLibrary(hlib);
            }
        }
    }
}
