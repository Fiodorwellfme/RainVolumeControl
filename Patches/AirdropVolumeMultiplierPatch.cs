using HarmonyLib;
using System.Reflection;
using Audio.SpatialSystem;
using Comfort.Common;
using SPT.Reflection.Patching;
using UnityEngine;

namespace RainVolumeControl.Patches
{
    public class AirdropVolumePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AirdropLogicClass), "method_5", new[] { typeof(TaggedClip), typeof(bool), typeof(bool) });
        }

        [PatchPrefix]
        private static bool Prefix(AirdropLogicClass __instance, TaggedClip clip, bool looped, bool oneShot)
        {
            float multiplier = Settings.AirdropVolumeMultiplier.Value;

            if (__instance.BetterSource_0.PlayBackState == BetterSource.EPlayBackState.Playing)
                return false;

            if (MonoBehaviourSingleton<SpatialAudioSystem>.Instantiated)
            {
                EOcclusionTest occlusionTest = looped ? EOcclusionTest.ContinuousPropagated : EOcclusionTest.OneShotPropagation;

                MonoBehaviourSingleton<SpatialAudioSystem>.Instance.ProcessSourceOcclusion(__instance.AirdropSynchronizableObject_0.gameObject, __instance.BetterSource_0, occlusionTest, 30000f, default(Vector3), false);
            }

            __instance.BetterSource_0.SetSpatialBlend(1f);
            __instance.BetterSource_0.SetRolloff((float)clip.Falloff);
            __instance.BetterSource_0.Loop = looped;
            __instance.BetterSource_0.Play(clip.Clip, null, 1f, clip.Volume * multiplier, false, oneShot && !looped);

            return false;
        }
    }
}
