using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace OFFRestored
{
    [BepInPlugin("com.ferase.offrestored", "OFF Restored", "1.0.0")]
    public class OFFMainPlugin : BaseUnityPlugin
    {
        public static OFFMainPlugin Instance { get; private set; }

        // Main config
        public static ConfigEntry<bool> ShowVersionInfoOnTitle;

        // Music config
        public static ConfigEntry<bool> ReplaceMusic;
        public static ConfigEntry<bool> DedanLoopFix;
        public static ConfigEntry<bool> DisableCreditsLoop;

        // SFX config
        public static ConfigEntry<bool> ReplaceSFX;
        public static ConfigEntry<bool> NoATBSound;

        // Get amount of custom music and SFX for the mod info
        public static int custom_audio_count = 0;
        public static int custom_audio_error_count = 0;

        private void Awake()
        {
            Instance = this;
            Logger.LogInfo("Audio purification in progress...");

            // Set config
            SetConfig();

            // Initialize music/SFX replacement system
            OFFSoundLoader.Initialize();

            // Only show the mod version if the player wants it
            if (ShowVersionInfoOnTitle.Value)
            {
                OFFModUI.Initialize();
            }

            // Apply all Harmony patches
            var harmony = new Harmony("com.ferase.offrestored");
            harmony.PatchAll();

            Logger.LogInfo("Audversaries purified!");
        }

        // Game start
        private void Start()
        {
            // Preload all custom audio after game starts
            OFFSoundLoader.PreloadCustomAudio();
        }

        // Set config data
        private void SetConfig()
        {
            // Show mod version on title screen
            ShowVersionInfoOnTitle = Config.Bind(
                "Main",
                "ShowVersionInfoOnTitle",
                true,
                "If true, The mod name and version will be shown on the top-left of the title screen."
            );

            // Replace Music
            ReplaceMusic = Config.Bind(
                "Music",
                "ReplaceMusic",
                true,
                "If true, music will be replaced with anything in BepInEx/plugins/OFFRestored/Music"
            );
            // Dedan fix
            DedanLoopFix = Config.Bind(
                "Music",
                "DedanLoopFix",
                true,
                "If true, removes the hardcoded loop points for Dedan's battle theme (Dedan2.wav). If you're not replacing that file, set this to false."
            );
            // Credits loop removal
            DisableCreditsLoop = Config.Bind(
                "Music",
                "DisableCreditsLoop",
                true,
                "If true, The credits music will not loop."
            );

            // Replace SFX
            ReplaceSFX = Config.Bind(
                "SFX",
                "ReplaceSFX",
                true,
                "If true, sound effects will be replaced with anything in BepInEx/plugins/OFFRestored/SFX"
            );
            // ATB removal
            NoATBSound = Config.Bind(
                "SFX",
                "NoATBSound",
                true,
                "If true, removes the sound that plays at the start of an ally's turn in battle."
            );
        }

    }
}