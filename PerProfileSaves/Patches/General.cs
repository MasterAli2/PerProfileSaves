using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace PerProfileSaves.Patches
{ 
    public class General
    {
        private static ManualLogSource Logger => PerProfileSaves.Logger;

        [HarmonyPatch(typeof(ES3Settings), "get_" + nameof(ES3Settings.FullPath))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> ReplacePDP(IEnumerable<CodeInstruction> instructions)
        {
            bool found = false;

            FieldInfo path = AccessTools.Field(typeof(PerProfileSaves), nameof(PerProfileSaves.FinalPath));
            if (path == null)
            {
                Logger.LogError($"An exception occured while patching {MyPluginInfo.PLUGIN_NAME}.");
                found = true;
            }

            foreach (var item in instructions)
            {
                if (!found && item.opcode == OpCodes.Ldsfld)
                {
                    yield return new CodeInstruction(OpCodes.Ldsfld, path);

                    found = true;
                    continue;
                }

                yield return item;
            }
        }
    }
}
