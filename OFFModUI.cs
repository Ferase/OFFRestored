using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace OFFRestored
{
    public static class OFFModUI
    {
        // Declare label game object
        private static GameObject mod_version_gameobject;

        // Init, add scene listener for mod label
        // Not really a fan of this, but not really sure if there's a better way
        public static void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // If the scene is the menu, add the label. Abort if not
        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "menu")
            {
                return;
            }

            Debug.Log("Player on menu scene, getting mod version and info");
            CreateModVersionText();
        }

        private static void CreateModVersionText()
        {
            // Clean up old mod_text object
            if (mod_version_gameobject != null)
            {
                Object.Destroy(mod_version_gameobject);
            }

            // Find the main menu
            GameObject menu_canvas = GameObject.Find("Main Menu");

            // Create label
            mod_version_gameobject = new GameObject("OFFRestoredVersionInfo");
            mod_version_gameobject.transform.SetParent(menu_canvas.transform, false);

            // Add Text component
            Text mod_text = mod_version_gameobject.AddComponent<Text>();

            // Get mod info
            string mod_info = $"{OFFMainPlugin.Instance.Info.Metadata.Name} / v{OFFMainPlugin.Instance.Info.Metadata.Version}";
            
            // If we're replacing audio, display how much we're replacing
            if (OFFMainPlugin.ReplaceMusic.Value || OFFMainPlugin.ReplaceSFX.Value)
            {
                mod_info += $"\n{OFFMainPlugin.custom_audio_count} audio files loaded";

                // If files failed to load
                if (OFFMainPlugin.custom_audio_error_count > 0)
                {
                    mod_info += $"\n{OFFMainPlugin.custom_audio_error_count} audio files failed to load";
                }
            }

            // Apply the text
            mod_text.text = mod_info;

            // Find the game font from loaded fonts, prevents needing to load it again
            Font[] loaded_fonts = Resources.FindObjectsOfTypeAll<Font>();
            foreach (Font font in loaded_fonts)
            {
                if (font.name == "pixelham_condensed")
                {
                    // Refer the mod text font to the font of whatever this other element is
                    mod_text.font = font;
                    break;
                }
            }

            // Size, color, and alignment
            mod_text.fontSize = 12;
            mod_text.color = new Color32(214, 85, 24, 255);
            mod_text.alignment = TextAnchor.UpperLeft;

            // Position info in top-left corner
            RectTransform rectTransform = mod_version_gameobject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);
            rectTransform.anchoredPosition = new Vector2(10, -10);
            rectTransform.sizeDelta = new Vector2(400, 50);
        }
    }
}