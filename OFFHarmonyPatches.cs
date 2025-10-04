using System;
using System.Collections.Generic;
using DG.Tweening;
using FangamerRPG;
using HarmonyLib;
using OFFGame.Battle;
using UnityEngine;

namespace OFFRestored
{
    // Replace sound, reusable
    class ReplaceSound
    {
        public static void Replace(ScriptedAudioClip sound, string type)
        {
            // Before playing anything, check if we should replace the clip
            if (sound != null && sound.clip != null)
            {
                // Load new audio
                AudioClip new_audio = OFFSoundLoader.GetCustomClip(sound.clip, type);

                // Set new audio as replacement
                if (new_audio != null)
                {
                    Debug.Log($"Found replacement for {sound.clip.name}");
                    sound.clip = new_audio;
                }
            }
        }
    }

    // Patch: Dedan theme fix
    [HarmonyPatch(typeof(CrossFadableAudio), "CrossFade")]
    class DedanThemePatch
    {
        static bool Prefix(CrossFadableAudio __instance, ScriptedAudioClip sound)
        {
            // Skips the fix if the player doesn't want it
            if (!OFFMainPlugin.DedanLoopFix.Value)
            {
                return true;
            }

            // Access private fields
            var traverse = Traverse.Create(__instance);
            traverse.Field("_isLooping").SetValue(false);

            if (sound.fadeDuration <= 0f)
            {
                __instance.Play(sound);
                return false;
            }

            List<Tween> list = DOTween.TweensByTarget(__instance.primarySource, false, null);
            if (list != null && list.Count > 2)
            {
                __instance.Stop();
            }

            if (!__instance.primarySource.isPlaying)
            {
                ScriptedAudioClip.CopyClipSettingsToSource(ref sound, ref __instance.primarySource);
                // Replaces:
                //__instance.FadeIn(__instance.primarySource, sound, sound.fadeDuration);
                traverse.Method("FadeIn", __instance.primarySource, sound, sound.fadeDuration).GetValue();
                return false;
            }

            // Replaces:
            //AudioSource audioSource = __instance.CreateCrossFadeSource();
            AudioSource audioSource = traverse.Method("CreateCrossFadeSource").GetValue<AudioSource>();
            audioSource.Play();
            ScriptedAudioClip.CopyClipSettingsToSource(ref sound, ref __instance.primarySource);
            __instance.primarySource.volume = 0f;

            // Replaces:
            //__instance.FadeOut(audioSource, sound.fadeDuration, true);
            traverse.Method("FadeOut", new Type[] {
                typeof(AudioSource),
                typeof(float),
                typeof(bool)
            }, new object[] {
                audioSource,
                sound.fadeDuration,
                true
            }).GetValue();

            // Replaces:
            //__instance.FadeIn(__instance.primarySource, sound, sound.fadeDuration);
            traverse.Method("FadeIn", __instance.primarySource, sound, sound.fadeDuration).GetValue();

            return false;
        }
    }

    // Patch: Remove ATB sound
    [HarmonyPatch(typeof(BATUnit), "PlayReadySFX")]
    class ATBSoundPatch
    {
        static bool Prefix()
        {
            // Skips removing it if the player wants the sound
            if (!OFFMainPlugin.NoATBSound.Value)
            {
                return true;
            }
            return false;
        }
    }

    // Patch: Music replacement
    [HarmonyPatch(typeof(FPGAudioManager), "ChangeBGM")]
    class MusicPatch
    {
        static bool Prefix(ScriptedAudioClip sound)
        {
            ReplaceSound.Replace(sound, "Music");

            // Continue with the rest of the function
            return true;
        }
    }

    // Disables looping for the credits music
    [HarmonyPatch(typeof(FPGAudioManager), "ChangeBGM")]
    class CreditsPatch
    {
        static void Postfix(FPGAudioManager __instance, ScriptedAudioClip sound)
        {
            // Skip if the player doesn't want to skip the credits
            if (!OFFMainPlugin.DisableCreditsLoop.Value)
            {
                return;
            }

            // Check for the credits theme, and disable the loop
            if (sound.clip.name == "Ending Song_January2025")
            {
                __instance.appBGMSource.primarySource.loop = false;
            }

            // End the function
            return;
        }
    }

    // Patch: SFX replacement
    [HarmonyPatch(typeof(FPGAudioManager), "PlaySFX")]
    class SFXPatch
    {
        static bool Prefix(ScriptedAudioClip sound)
        {
            ReplaceSound.Replace(sound, "SFX");

            // Continue with the rest of the function
            return true;
        }
    }
}