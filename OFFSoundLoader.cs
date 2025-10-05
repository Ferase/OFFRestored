using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.IO;

namespace OFFRestored
{
    public static class OFFSoundLoader
    {
        // Declare music and SFX
        private static readonly string[] sound_types = new string[2];

        // Target root directory for the plugin
        private static readonly string sound_root_dir = Path.Combine(BepInEx.Paths.PluginPath, "OFFRestored");

        // Set up dictionary to contain loaded audio as { "name", AudioClip }
        private static readonly Dictionary<string, AudioClip> loaded_audio = [];

        // Initialize directories
        public static void Initialize()
        {
            // Delcare which filders to create and check based on config
            if (OFFMainPlugin.ReplaceMusic.Value) { sound_types[0] = "Music"; }
            if (OFFMainPlugin.ReplaceSFX.Value) { sound_types[1] = "SFX"; }

            foreach (string type in sound_types)
            {
                // Skip if we aren't replacing something
                if (type == null) continue;

                // Get sound directory
                string new_path = Path.Combine(sound_root_dir, type);

                // Create folder if it doesn't exist
                if (!Directory.Exists(new_path))
                {
                    Directory.CreateDirectory(new_path);
                    Debug.Log($"Created custom sound folder in {new_path}");
                }
            }
        }

        // Check if a custom audio file exists for this audio_file_address
        public static bool HasCustomAudio(string audio_file_address, out string audio_file_path, string type)
        {
            // Extract just the filename from the audio_file_address
            string audio_file = Path.GetFileNameWithoutExtension(audio_file_address);

            // Get path of replacement audio file
            string path = Path.Combine(sound_root_dir, type, audio_file + ".wav");

            // If the replacement audio file exists, return true
            if (File.Exists(path))
            {
                audio_file_path = path;
                return true;
            }

            // If it doesn't exist, return false
            audio_file_path = null;
            return false;
        }

        // Load audio clip from file
        public static void LoadAudioClip(string audio_file_path)
        {
            // Don't load if it's already cached
            if (loaded_audio.TryGetValue(audio_file_path, out AudioClip cached))
            {
                return;
            }

            // Set up and send web request
            string url = "file://" + audio_file_path;
            UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);

            // Get the response from the operation
            var operation = request.SendWebRequest();
            operation.completed += (async_operation) =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                    clip.name = Path.GetFileNameWithoutExtension(audio_file_path);

                    OFFMainPlugin.custom_audio_count++;
                    loaded_audio[audio_file_path] = clip;

                    Debug.Log($"Loaded custom audio: {clip.name}");
                }
                else
                {
                    OFFMainPlugin.custom_audio_error_count++;
                    Debug.LogError($"Failed to load audio {audio_file_path}: {request.error}");
                }
            };
        }

        // Try to get a cached audio clip
        public static bool TryGetCachedClip(string file_path, out AudioClip clip)
        {
            return loaded_audio.TryGetValue(file_path, out clip);
        }

        // Get custom clip by original clip name (checks if file exists and loads/caches it)
        public static AudioClip GetCustomClip(AudioClip original_audio, string type)
        {
            // If there's no original audio, don't do anything
            if (original_audio == null) return null;

            // If we don't have a replacement, use the original
            if (!HasCustomAudio(original_audio.name, out string file_path, type))
            {
                return null;
            }

            // Check if already cached
            if (TryGetCachedClip(file_path, out AudioClip cachedClip))
            {
                return cachedClip;
            }

            // Fallback if for some reason we didn't precache
            Debug.LogWarning($"Clip {original_audio.name} was not precached and probably won't play. Something's messed up!");
            return null;
        }

        // Preload all custom audio files at startup
        public static void PreloadCustomAudio()
        {
            foreach (string type in sound_types)
            {
                if (type == null) continue;

                string target_path = Path.Combine(sound_root_dir, type);
                if (!Directory.Exists(target_path)) continue;

                string[] audio_files = Directory.GetFiles(target_path, "*.wav", SearchOption.TopDirectoryOnly);

                foreach (string file in audio_files)
                {
                    Debug.Log($"Preloading {Path.GetFileName(file)}...");
                    LoadAudioClip(file);
                }
            }
        }
    }
}