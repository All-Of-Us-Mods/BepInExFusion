using System;
using System.IO;

namespace BepInEx.Preloader.Core;

/// <summary>
///     FusionCore environment variables, passed into the BepInEx preloader.
///     <para>https://github.com/All-Of-Us-Mods/FusionCore</para>
/// </summary>
public static class EnvVars
{
    /// <summary>
    ///     Path to the BepInEx folder, passed in by FusionCore.
    /// </summary>
    public static string FUSION_BEPINEX_PATH { get; private set; }
    
    /// <summary>
    ///     Path to the game binary (libil2cpp.so)
    /// </summary>
    public static string FUSION_GAME_BINARY { get; private set; }

    /// <summary>
    ///     Path to the app's data directory, not safe to write.
    /// </summary>
    public static string FUSION_GAME_DATA_DIR { get; private set; }

    /// <summary>
    ///     Path to FusionCore's data directory, safe to write.
    /// </summary>
    public static string FUSION_APP_DATA_DIR { get; private set; }

    
    /// <summary>
    ///     Unity version override passed in by FusionCore.
    /// </summary>
    public static string FUSION_UNITY_VERSION { get; private set; }
    
    internal static void LoadVars()
    {
        FUSION_BEPINEX_PATH = Environment.GetEnvironmentVariable("FUSION_BEPINEX_PATH");
        FUSION_GAME_BINARY = Environment.GetEnvironmentVariable("FUSION_GAME_BINARY");
        FUSION_GAME_DATA_DIR = Environment.GetEnvironmentVariable("FUSION_GAME_DATA_DIR");
        FUSION_APP_DATA_DIR = Environment.GetEnvironmentVariable("FUSION_APP_DATA_DIR");
        FUSION_UNITY_VERSION = Environment.GetEnvironmentVariable("FUSION_UNITY_VERSION");
    }
}
