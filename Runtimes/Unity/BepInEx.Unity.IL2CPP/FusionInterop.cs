using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace BepInEx.Unity.IL2CPP;

internal static unsafe partial class FusionInterop
{
    private const string LIBRARY_NAME = "fusion";

    [LibraryImport(LIBRARY_NAME, EntryPoint = "write_log_level", StringMarshalling = StringMarshalling.Utf8)]
    public static unsafe partial void write_log_level(int logLevel, [MarshalAs(UnmanagedType.LPStr)] string message);

    [LibraryImport(LIBRARY_NAME)]
    // ReSharper disable once InconsistentNaming
    public static unsafe partial IntPtr hook(IntPtr target, IntPtr detour,
                                             [MarshalAs(UnmanagedType.I1)] bool specialReturnBuffer);

    [LibraryImport(LIBRARY_NAME)]
    public static unsafe partial void unhook(IntPtr target);
}
