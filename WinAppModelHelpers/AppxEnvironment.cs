using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAppModelHelpers;

public static class AppxEnvironment
{
    public static bool IsAppx
    {
        get
        {
            [DllImport(CommonNativeInterop.KernelBase, CharSet = CharSet.Unicode, SetLastError = true)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder? packageFullName);
            int length = 0;
            return GetCurrentPackageFullName(ref length, null) != 15700L;
        }
    }

    public enum AppPolicyWindowingModel
    {
        None,
        Universal,
        ClassicDesktop,
        ClassicPhone
    }

    [DllImport(CommonNativeInterop.MsWinAppModelL112, ExactSpelling = true, SetLastError = true)]
    public static extern long AppPolicyGetWindowingModel(IntPtr processToken, out AppPolicyWindowingModel policy);

    public static bool IsCoreApplication
    {
        get
        {
            AppPolicyGetWindowingModel(new(-4), out AppPolicyWindowingModel windowingModel);
            return windowingModel switch
            {
                AppPolicyWindowingModel.None => false,
                AppPolicyWindowingModel.Universal => true,
                AppPolicyWindowingModel.ClassicDesktop => false,
                AppPolicyWindowingModel.ClassicPhone => true,
                _ => false
            };
        }
    }

    public enum AppPolicyLifecycleManagement
    {
        AppPolicyLifecycleManagement_Unmanaged,
        AppPolicyLifecycleManagement_Managed
    }

    [DllImport(CommonNativeInterop.MsWinAppModelL112, ExactSpelling = true, SetLastError = true)]
    public static extern long AppPolicyGetLifecycleManagement(IntPtr processToken, out AppPolicyLifecycleManagement policy);

    public static bool IsAppLifecycleManaged
    {
        get
        {
            AppPolicyGetLifecycleManagement(new(-4), out AppPolicyLifecycleManagement policy);
            return policy switch
            {
                AppPolicyLifecycleManagement.AppPolicyLifecycleManagement_Unmanaged => false,
                AppPolicyLifecycleManagement.AppPolicyLifecycleManagement_Managed => true,
                _ => false
            };
        }
    }
}
