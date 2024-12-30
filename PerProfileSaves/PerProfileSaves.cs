using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using PerProfileSaves.Patches;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace PerProfileSaves
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class PerProfileSaves : BaseUnityPlugin
    {
        public static PerProfileSaves Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        public static string FinalPath = Application.persistentDataPath;

        public ConfigEntry<string> configSavePath;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            bindConfigs();

            prePatch();
            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        void bindConfigs()
        {
            configSavePath = Config.Bind("General",  // Section
                                         "SavePath", // Key
                                         "saves",    // Default
                                         "Where to store the save files relative to the Bepinex folder, absolute paths are also allowed"); // Description
        }

        void prePatch()
        {
            string path = configSavePath.Value;

            if(path.IsNullOrWhiteSpace())
            {
                Logger.LogError("The specified save path is invalid. Reverting to the default directory.");

                path = (string)configSavePath.DefaultValue;
            }

            path = Regex.Escape(path);

            if (Path.IsPathRooted(path))
                Logger.LogDebug("Absolute path found!");
            else
            {
                Logger.LogDebug("Relative path found!");

                path = Path.Combine(BepInEx.Paths.BepInExRootPath, path);
            }

            FinalPath = path;

            Logger.LogInfo($"Save location set to: {FinalPath}");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll(typeof(General));

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}
