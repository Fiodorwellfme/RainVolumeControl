using Audio.Vehicles;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace RainVolumeControl.Patches
{
    public class BTRVolumeMultiplierPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.PropertyGetter(typeof(VehicleMovementSoundContext), "MaxAllowedVolume");
        }

        [PatchPostfix]
        private static void PatchPostfix(ref float __result)
        {
            __result *= Settings.BTRVolumeMultiplier.Value;
        }
    }
}
