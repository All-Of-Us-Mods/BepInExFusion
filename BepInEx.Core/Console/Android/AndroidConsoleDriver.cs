using System;
using System.IO;
using BepInEx.Logging;

namespace BepInEx.Core.Console.Android;

public class AndroidConsoleDriver : IConsoleDriver
{
    public TextWriter StandardOut { get; } = new StringWriter();

    public TextWriter ConsoleOut { get; } = new StringWriter();
    public bool ConsoleActive => false;
    public bool ConsoleIsExternal => false;

    public AndroidConsoleDriver()
    {
        ConsoleOut.WriteLine("BepInEx Android Console Driver Created.");
    }
    
    public void PreventClose()
    {
        Logger.Log(LogLevel.Warning, "PreventClose() is unsupported on Android.");
    }

    public void Initialize(bool alreadyActive, bool useManagedEncoder)
    {
        Logger.Log(LogLevel.Info, "Console driver initialized for Android.");
    }

    public void CreateConsole(uint codepage)
    {
        Logger.Log(LogLevel.Warning, "An external console cannot be spawned on Android.");
    }

    public void DetachConsole()
    {
        Logger.Log(LogLevel.Warning, "The console cannot be detached on Android.");
    }

    public void SetConsoleColor(ConsoleColor color)
    {
        Logger.Log(LogLevel.Warning, "Console colors are not supported on Android.");
    }

    public void SetConsoleTitle(string title)
    {
        Logger.Log(LogLevel.Warning, "The console title is not supported on Android.");
    }
}
