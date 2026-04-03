using Audio.Vehicles.BTR;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace RainVolumeControl.Patches
{
    internal class BTRVolumeMultiplierPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(BtrSoundController), "method_10");
        }

        [PatchPrefix]
        private static bool Prefix(GInterface218 inControl, GInterface218 outControl, EnvironmentType environment, float t)
        {
            float multiplier = Settings.BTRVolumeMultiplier.Value;    
            GInterface218 active = (environment == EnvironmentType.Outdoor) ? outControl : inControl;
            GInterface218 inactive = (environment == EnvironmentType.Outdoor) ? inControl : outControl;     
            inactive.SetBaseVolume((1f - t) * multiplier);
            active.SetBaseVolume(t * multiplier);
        
            return false;
        }
    }
}
