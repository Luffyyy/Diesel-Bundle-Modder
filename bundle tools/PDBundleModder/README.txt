 PAYDAY 2 Bundle Modder v1.15
 
 This tool is designed to simplify the creation and application of visual
modifications to the PAYDAY series of games.
 
 
 DISCLAIMER: We are not responsible for what you do with this tool. If you get
yourself banned using this, that is on you.
 
 
 Setup:
 When your first launch the application, it will give you some important
information, you should read it. In case you missed it(shame on you for clicking through so
quickly,) it details that this tool modifies game files, and while it creates backups,
you should always validate your game after OVERKILL has released a patch, let the
game update to completion, and then reapply any mods you wish to keep. Failure to
do so may result in undefined behaviour during gameplay, such as missing assets,
crashing, or corrupt textures.
 Next, you must select your game assets folder. It should be under Program
Files->Steam->SteamApps->common->PAYDAY 2. It will then validate that you have selected the correct folder.
 
 Applying a mod:
 Go to "Apply Mod" tab.
 Choose "Open Mod", select the mod package that you wish to apply, and then read
the description of the mod.
 Click "Apply Mod".
 Wait.
 Wait.
 Wait.
 Wait.
 Enjoy.
 
 Applying mods in a bulk:
 Go to "Bulk Apply Mods" tab.
 Choose "Add Mod(s)", select the mod package that you wish to apply. Repeat this step for all mods
that you wish to apply in a bulk.
 Click "Apply Mods".
 Wait.
 Wait.
 Wait.
 Wait.
 Enjoy.
 
 Uninstalling Mods:
 Go to "Manage Mods" tab.
 Select mods from the list to be removed. (Note: You can hold CTRL and click on mods, to select multiple mods).
 Select your uninstall method. Uninstalling Individual Bundle Entries will restore modded files inside bundles.
Uninstalling Entire Bundles will restore original bundles.
 Click "Uninstall Mod(s)".
 Wait.
 Wait.
 Wait.
 Wait.
 Enjoy.
 
 
 Creating a mod:
 Enter the JPMod filename, or the file path relative to the extract folder. If
using JPMod file names with non-hex file extensions, make sure that you know the
engine specific file extension related to the file, as you will need to use that.
.dds should be .texture for example.
 Unchecked the "Use JPMod Style Names" checkbox if using file paths.
 Browse for the file you want to replace the above bundle file with.
 If your file needs to respect language filters(your JPMod file name has
something other than .0. in the center, or your file name has a third piece when
extracted using my tool) you should check the Language Specific File checkbox to avoid
replacing unrelated files.
 Choose Add Replacement File
 Repeat as required by your mod.
 Enter the name of your mod, your name, and then a description of what you
changed.
 Select the version for which your mod is deigned for. This is to ensure tool compatibility
with mods for the end users.
 Click "Create Mod" save it somewhere. Distribute as you see fit. Test it
first.
 
 
 Technical details:
 Mod packages are simple zip files, and may be modified using a simple archive
tool if you so desire to do so. This is entirely unsupported, but should work. Mod
metadata is stored using JSON. Don't edit this file unless you are 100% sure you know
what you are doing.

 Changes:
 - 1.1:
  - Made backups only take place once.
  - Added game version detection. Tool removes the backups folder when the game
    version changes to make patches easier.
 - 1.11:
  - Fixed crash on startup when asset backup folder doesn't exist.
  - Fixed crash when applying a second mod. (Using deleted object.)
 - 1.12:
  - Fix for bundle files that use the length field.
 - 1.13: 
  - Updated for use with new bundle system
 - 1.14:
  - Added script functionality.
 - 1.15:
  - Added functionality to apply multiple mods at the same time.
  - Added functionality to remove installed mods.
  - Added functionality to save and load PDMod Projects.
  - Added functionality to remove any selected replacement entry while creating a mod.
  - Added an automatic replacement for "Bundle File Name" of '\' with '/'.
  - Added safety checks for installing/removing mods.
  - Added window resize functionality (up to 600 in height).
  - Changed "Source File" in Added Files to represent actual in-game file path.
  - Fixed tool patching files after being closed.
  - Fixed PDMod files not being released after applying mods.
  - Removed "Rewrite all_x" functionality.
 - 1.15 Fix 1:
  - Resolved a crash issue with regard to selecting specific versions while creating a mod.
  - Fixed up error messages while adding multiple mods to be installed.

  
 Cheers,
 Zwagoth and I am not a spy...
