using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace BepInEx.Unity.IL2CPP;

internal static unsafe partial class FusionInterop
{
    private const string LIBRARY_NAME = "fusion";

    [LibraryImport(LIBRARY_NAME, EntryPoint = "write_log", StringMarshalling = StringMarshalling.Utf8)]
    public static unsafe partial void write_log([MarshalAs(UnmanagedType.LPStr)] string message);

    [LibraryImport(LIBRARY_NAME)]
    public static unsafe partial void init_bridge_helper([MarshalAs(UnmanagedType.LPStr)] string bridgeLibPath);

    [LibraryImport(LIBRARY_NAME)]
    // ReSharper disable once InconsistentNaming
    public static unsafe partial IntPtr hook(IntPtr target, IntPtr detour,
                                             [MarshalAs(UnmanagedType.I1)] bool specialReturnBuffer);

    [LibraryImport(LIBRARY_NAME)]
    public static unsafe partial void unhook(IntPtr target);
    
    public class InteropWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8; 

        private readonly StringBuilder buffer = new(1024);

        public override void WriteLine(string value)
        {
            buffer.AppendLine(value);
            FlushBuffer();
        }

        public override void Write(char value)
        {
            buffer.Append(value);

            if (value == '\n')
            {
                FlushBuffer();
            }
        }

        public void FlushBuffer()
        {
            if (buffer.Length == 0)
                return;

            write_log(buffer.ToString());
            buffer.Clear();
        }
    }
}
