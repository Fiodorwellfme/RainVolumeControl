using Audio.AmbientSubsystem;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace RainVolumeControl.Patches
{
    internal class RainVolumeMultiplierPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(PrecipitationAmbientBlender), "method_6");
        }

        [PatchPostfix]
        static void Postfix(PrecipitationAmbientBlender __instance)
        {
            var field = Traverse.Create(__instance).Field("float_4");
            float current = field.GetValue<float>();
            field.SetValue(current * Settings.RainVolumeMultiplier.Value);
        }

    }
}
