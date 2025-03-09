using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System;
using System.Threading;
using WinAppModelHelpers;
using WinRT;

namespace AppxRTHTestSampleWAS;

public static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        if (!AppxEnvironment.IsAppx)
        {
            if (!WindowsAppRuntimeBootstrap.TryInitialize(0x00010006, out int hr))
                Environment.Exit(hr);
        }
        ComWrappersSupport.InitializeComWrappers();
        Application.Start((p) => {
            var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            new App();
        });
    }
}
