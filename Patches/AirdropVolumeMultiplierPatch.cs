using HarmonyLib;
using System.Reflection;
using Audio.SpatialSystem;
using SPT.Reflection.Patching;
using UnityEngine;

namespace RainVolumeControl.Patches
{
    public class AirdropVolumeMultiplierPatch : ModulePatch
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
    
    public class AirplaneVolumePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AirplaneLogicClass), "method_1", new[] { typeof(TaggedClip)});
        }

        [PatchPrefix]
        private static bool Prefix(AirplaneLogicClass __instance, TaggedClip clip)
        {
            float multiplier = Settings.AirdropVolumeMultiplier.Value;
            
    		float num = __instance.method_3();
    		__instance.BetterSource_0.SetBaseVolume(EFTHardSettings.Instance.PlaneVolumeCurve.Evaluate(num) * multiplier);
    		__instance.BetterSource_0.SetPitch(Mathf.Lerp(0.7f, 1.1f, num));
    		__instance.BetterSource_0.source1.spread = Mathf.Lerp(60f, 180f, num);
    		if (__instance.BetterSource_0.PlayBackState == BetterSource.EPlayBackState.Playing)
    		{
    			return false;
    		}
    		__instance.BetterSource_0.source1.rolloffMode = AudioRolloffMode.Custom;
    		__instance.BetterSource_0.source1.SetCustomCurve(AudioSourceCurveType.CustomRolloff, EFTHardSettings.Instance.SoundRolloff);
    		__instance.BetterSource_0.source1.clip = clip.Clip;
    		__instance.BetterSource_0.Loop = true;
    		__instance.BetterSource_0.source1.dopplerLevel = 1.5f;
    		__instance.BetterSource_0.SetSpatialBlend(1f);
    		__instance.BetterSource_0.SetRolloff((float)clip.Falloff);
    		__instance.BetterSource_0.Play(clip.Clip, null, 1f, 1f, false, false);
            
            return false;
        }
    }
}
