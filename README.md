# Per Profile Saves

## Overview

A mod for Lethal Company that lets you modify the save location and therefor by default allows you to have different save files for each mod profile.

The main utility of this mod is to have different save files based on different modpacks/profiles, it should be compatible with other mods as long as the ES3 location type or directory is not modified.

Note: when first installing this mod you must manualy move your current save files to `BepInEx/saves` (where the `SavePath` config points to) if you wish to keep your old saves

Note: this was only tested using the Gale mod manager, it should work with other mod managers but im not sure.

## Config

The `SavePath` config option determines where to store the save files relative to the Bepinex folder, you can also provide a absolute path.

By default the saves are stored in `BepInEx/saves` but it can be changed in configs.

If your unsure where you set the save location you can check the console/log to see the absolute path of where it is stored.

Note: the folowing characters are automatically escaped "[(.*?)]" therefor paths such as the Gale app data cannot be specified with the absolute path. (anyway why would you just use the default path)

## License
This mod is licensed under the MIT license.
