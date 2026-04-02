using Audio.AmbientSubsystem;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RainVolumeControl.Patches;

namespace RainVolumeControl
{
    [BepInPlugin("com.fiodor.rainvolumecontrol", "RainVolumeControl", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("RainVolumeControl 1.0.0 plugin loaded!");
            Settings.Init(Config);
            new RainVolumeMultiplierPatch().Enable();

            Settings.RainVolumeMultiplier.SettingChanged += (_, __) =>
            {
                foreach (var blender in FindObjectsOfType<PrecipitationAmbientBlender>())
                {
                    AccessTools.Method(typeof(PrecipitationAmbientBlender), "method_2")
                        .Invoke(blender, new object[] { 1f, 1f });
                }
            };
        }
    }
}
