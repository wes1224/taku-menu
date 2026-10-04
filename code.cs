using BepInEx;
using System.ComponentModel;

namespace TakuMenu.Patches
{
    [Description(PluginInfo.Description)]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private void OnEnable()
        {
            PatchManager.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            PatchManager.RemoveHarmonyPatches();
        }
    }
}
