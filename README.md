# OFF Restored

A sound modding plugin built on [BepInEx](https://github.com/BepInEx/BepInEx) for the [2025 remake of OFF](https://store.steampowered.com/app/3339880/OFF/). It allows for easy sound replacement by making every sound overridable with WAV files stored in the plugin's folders.

-------------------------------------

## Building

This project requires Visual Studio 2022 and .NET 4.7.1 as a baseline, however all of the references to OFF's and BepInEx's DLLs in `OffRestored.csproj` are relative to my local system. In order to build this plugin yourself, you must ensure the paths to the DLLs are correct.

This was my first plugin made in BepInEx, so my process was a bit whacked out when it came to referencing dependencies. I plan to soon update the references to use NuGet where it can, hopefully minimizing direct DLL references beyond the necessities.

## Download

The builds of this plugin are hosted on the [OFF Restored Itch.io page](https://ferase.itch.io/off-ost-restored) and will be in line with changes made here.

In the future, a release build with only the DLL will be released here with features to better define all the sound files in the game that can be replaced, allowing this mod to function much better on its own as a general sound modding tool.

## Credits

- **Ferase** / Audio editing, porting, and patching (OST)
- **[xemdev](https://xemdev.itch.io/)** / Audio editing, porting, and patching (SFX)
- **Pinedyne** / Testing
- **[Shaymoo](https://shaymoo.net/)** / Testing
- **[OctaHeart](https://octaheart.tumblr.com/)** / Testing
