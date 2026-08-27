using System;
using System.Runtime.InteropServices;

namespace BepInEx.Unity.IL2CPP;

internal static partial class FusionInterop
{
    private const string LIBRARY_NAME = "fusion";

    [LibraryImport(LIBRARY_NAME)]
    public static unsafe partial void set_loader_stage(LoaderStage loadingStage);

    [LibraryImport(LIBRARY_NAME)]
    public static unsafe partial void set_loader_message([MarshalAs(UnmanagedType.LPStr)] string message);

    [LibraryImport(LIBRARY_NAME, StringMarshalling = StringMarshalling.Utf8)]
    public static unsafe partial void write_log_level(int logLevel, [MarshalAs(UnmanagedType.LPStr)] string message);

    [LibraryImport(LIBRARY_NAME)]
    // ReSharper disable once InconsistentNaming
    public static unsafe partial IntPtr hook(IntPtr target, IntPtr detour,
                                             [MarshalAs(UnmanagedType.I1)] bool specialReturnBuffer);

    [LibraryImport(LIBRARY_NAME)]
    public static unsafe partial void unhook(IntPtr target);

    public enum LoaderStage : byte
    {
        Preloader,
        Chainloader,
        Finished
    }
}
