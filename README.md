# OFF Restored

A sound modding plugin built on [BepInEx](https://github.com/BepInEx/BepInEx) for the [2025 remake of OFF](https://store.steampowered.com/app/3339880/OFF/). It allows for easy sound replacement by making every sound overridable with WAV files stored in the plugin's folders.

THe main plugin is hosted on [Itch.io](https://ferase.itch.io/off-ost-restored) and comes bundled with restored music and sound from the original RPG maker version of OFF.

-------------------------------------

## Building

### Requirements

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/) (if you prefer compiling in an IDE)
- [.NET Standard 2.1 (via .NET Core 2.1)](https://dotnet.microsoft.com/en-us/download/dotnet/2.1)
- [BepInEx NuGet source](https://nuget.bepinex.dev/)
- Direct references to OFF's own assemblies (place in the `lib/` directory)
  - Assembly-CSharp.dll
  - UnityEngine.dll
  - UnityEngine.UI.dll
  - DoTween.dll (a NuGet package exists but wouldn't compile with .NET Standard 2.1)
  - DoTween.Modules.dll (a NuGet package exists but wouldn't compile with .NET Standard 2.1)

### Notes

Besides the above requirements, NuGet should resolve all other dependency issues assuming the BepInEx source is added. For OFF's assemblies, it would be best to copy the above DLL files from `OFF/OFF_Data/Managed/` into the `lib/` folder for easy referencing.

### Compiling

You can either compile from Visual Studio by choosing `Build > Build Solution` at the top toolbar or by opening the project's directory in your terminal and entering:

```cmd
dotnet build
```

### Sound List & Info

Please check the [sound info readme](https://github.com/Ferase/OFFRestored/blob/main/README_SoundInfo.md) for a clear list of all the audio in the game and guidelines for using OFF Restored to replace sounds in the game.

## Download

The builds of this plugin are hosted on the [OFF Restored Itch.io page](https://ferase.itch.io/off-ost-restored) and will be in line with changes made here.

In the future, a release build with only the DLL will be released here with features to better define all the sound files in the game that can be replaced, allowing this mod to function much better on its own as a general sound modding tool.

## Credits

- **Ferase** / Audio editing, porting, and patching (OST)
- **[xemdev](https://xemdev.itch.io/)** / Audio editing, porting, and patching (SFX)
- **Pinedyne** / Testing
- **[Shaymoo](https://shaymoo.net/)** / Testing
- **[OctaHeart](https://octaheart.tumblr.com/)** / Testing
