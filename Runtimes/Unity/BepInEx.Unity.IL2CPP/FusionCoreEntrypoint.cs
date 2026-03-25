using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using BepInEx.Preloader.Core;
using BepInEx.Unity.IL2CPP.Utils;
using MonoMod.Utils;

namespace BepInEx.Unity.IL2CPP;

internal static class FusionCoreEntrypoint
{
    /// <summary>
    ///     The main entrypoint of BepInEx, called from Doorstop.
    /// </summary>
    [UnmanagedCallersOnly(EntryPoint = "Start")]
    public static void Start()
    {
        Console.SetOut(new FusionInterop.InteropWriter());
        Console.SetError(new FusionInterop.InteropWriter());

        // We set it to the current directory first as a fallback, but try to use the same location as the .exe file.
        var silentExceptionLog = Environment.GetEnvironmentVariable("BEPINEX_PRELOADER_LOG") ??
                                 $"preloader_{DateTime.Now:yyyyMMdd_HHmmss_fff}.log";
        Mutex mutex = null;

        try
        {
            EnvVars.LoadVars();

            silentExceptionLog =
                Path.Combine(Path.GetDirectoryName(EnvVars.FUSION_APP_DATA_DIR), silentExceptionLog);

            var mutexId = Utility.HashStrings(Process.GetCurrentProcess().ProcessName, EnvVars.FUSION_GAME_BINARY,
                                              typeof(FusionCoreEntrypoint).FullName);

            mutex = new Mutex(false, $"Global\\{mutexId}");
            mutex.WaitOne();

            UnityPreloaderRunner.PreloaderMain();
        }
        catch (Exception ex)
        {
            File.WriteAllText(silentExceptionLog, ex.ToString());

            try
            {
                if (PlatformDetection.OS is OSKind.Windows)
                {
                    MessageBox.Show("Failed to start BepInEx", "BepInEx");
                }
                else if (NotifySend.IsSupported)
                {
                    NotifySend.Send("Failed to start BepInEx", "Check logs for details");
                }
                else if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BEPINEX_FAIL_FAST")))
                {
                    // Don't exit the game if we have no way of signaling to the user that a crash happened
                    return;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            Environment.Exit(1);
        }
        finally
        {
            mutex?.ReleaseMutex();
        }
    }
}
