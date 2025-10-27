# OFF Restored

A sound modding plugin built on [BepInEx](https://github.com/BepInEx/BepInEx) for the [2025 remake of OFF](https://store.steampowered.com/app/3339880/OFF/). It allows for easy sound replacement by making every sound overridable with WAV files stored in the plugin's folders.

The main plugin is hosted on [Itch.io](https://ferase.itch.io/off-ost-restored) and comes bundled with restored music and sound from the original RPG maker version of OFF.

-------------------------------------

## Download, Install, & Usage

There are two available dowload sources for the plugin.

### Releases (DLL Only)

You can visit the [releases](https://github.com/Ferase/OFFRestored/releases) page to download the plugin DLL.

To install:

1. [Download BepInEx](https://github.com/BepInEx/BepInEx/releases/latest)
2. Extract the BepInEx ZIP to OFF's install directory
3. [Download the DLL](https://github.com/Ferase/OFFRestored/releases/latest)
4. Create a folder named `OFFRestored` inside `OFF/BepInEx/plugins/`
5. Place the `OFFRestored.dll` file inside the new `OFFRestored` folder
6. Run the game once

Running the game will cause the plugin to create `Music` and `SFX` folders inside of the `OFFRestored` folder. This is where you will place any music or sound effects you want to replace. Check the [sound info readme](https://github.com/Ferase/OFFRestored/blob/main/README_SoundInfo.md) for a listing of all sounds in the game along with some helpful info.

The plugin will also create a config at `OFF/BepInEx/config/com.ferase.offrestored.cfg` where you can change various settings and enable/disable features.

### Restored Music + SFX Pack

Alternatively, you can visit the [OFF Restored Itch.io page](https://ferase.itch.io/off-ost-restored) to download a version bundled with audio from the 2008 version of OFF. This includes the original soundtrack by Alias Conrad Coldwood as well as the classic RPG Maker sound effects.

Install instructions can be found on the same page.

### Sound List & Info

Please check the [sound info readme](https://github.com/Ferase/OFFRestored/blob/main/README_SoundInfo.md) for a clear list of all the audio in the game and guidelines for using OFF Restored to replace sounds in the game.

### Config

The plugin creates a config file at `OFF/BepInEx/config/com.ferase.offrestored.cfg` where some functions of the plugin can optionally be turned on or off.

These options include:

- **ShowVersionInfoOnTitle** (true/false, default true)
  - If true, The mod name and version will be shown on the top-left of the title screen alongside how many audio files have been loaded
- **ReplaceMusic** *(true/false, default true)*
  - If true, music will be replaced with anything in BepInEx/plugins/OFFRestored/Music
- **DedanLoopFix** *(true/false, default true)*
  - If true, removes the hardcoded loop points for Dedan's battle theme (Dedan2.wav). If you're not replacing that file, set this to false
- **DisableCreditsLoop** *(true/false, default true)*
  - If true, The credits music will not loop
- **ReplaceSFX** *(true/false, default true)*
  - If true, sound effects will be replaced with anything in `BepInEx/plugins/OFFRestored/SFX`
- **NoATBSound** *(true/false, default true)*
  - If true, removes the sound that plays at the start of an ally's turn in battle

## Building

### Requirements

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/) (if you prefer compiling in an IDE)
- [.NET Standard 2.1 (via .NET Core 2.1)](https://dotnet.microsoft.com/en-us/download/dotnet/2.1)
- [BepInEx NuGet source](https://nuget.bepinex.dev/)
- Direct references to OFF's own assemblies (create a new folder named `lib` inside of this project and place them there)
  - Assembly-CSharp.dll
  - UnityEngine.dll
  - UnityEngine.UI.dll
  - DoTween.dll (a NuGet package exists but wouldn't compile with .NET Standard 2.1)
  - DoTween.Modules.dll (a NuGet package exists but wouldn't compile with .NET Standard 2.1)

### Notes

Besides the above requirements, NuGet should resolve all other dependency issues assuming the BepInEx source is added. For OFF's assemblies, it would be best to create a `lib` folder inside the project's directory and copy the relevant DLLs from `OFF/OFF_Data/Managed/` there.

### Compiling

You can either compile from Visual Studio by choosing `Build > Build Solution` at the top toolbar or by opening the project's directory in your terminal and entering:

```cmd
dotnet build
```

## Credits

- **Ferase** / Audio editing, porting, and patching (OST)
- **[xemdev](https://xemdev.itch.io/)** / Audio editing, porting, and patching (SFX)
- **Pinedyne** / Testing
- **[Shaymoo](https://shaymoo.net/)** / Testing
- **[OctaHeart](https://octaheart.tumblr.com/)** / Testing
