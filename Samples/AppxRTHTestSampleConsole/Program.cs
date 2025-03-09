// See https://aka.ms/new-console-template for more information
using WinAppModelHelpers;

Console.WriteLine("---- WinAppModelHelpers Test ----");
Console.ReadKey();
Console.WriteLine($"IsAppx: {AppxEnvironment.IsAppx}");
Console.WriteLine($"IsCoreApplication: {AppxEnvironment.IsCoreApplication}");
Console.WriteLine($"IsAppLifecycleManaged: {AppxEnvironment.IsAppLifecycleManaged}");
Console.ReadKey();
