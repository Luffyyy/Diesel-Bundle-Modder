using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDBundleModPatcher
{
    /// <summary>
    ///     The program settings.
    /// </summary>
    public class ProgramSettings
    {
        #region Fields

        /// <summary>
        ///     The assets_folder.
        /// </summary>
        public string AssetsFolder = "";

        public string CurrentVersion;

        public int BackupType;

        public string Game;

        public string GameShortName;

        public string GameProcess;

        public int GameSteamID;

        public bool GameSupportsOverride = false;

        public bool AdvancedUser;

        public int PatchBuffer = 2;

        public bool OverrideFolder = true;

        public bool OverrideFolderDummies = true;

        public bool OverrideFolderShared = true;

        public bool Autocompletion = true;

        public bool PlaySoundOnCompletion = true;

        public bool RunGameOnCompletion = false;

        public bool CheckForUpdatesOnLaunch = true;

        public bool AlwaysPasswordProtect = true;

        public bool ShowOverrideMods = true;

        public int ThemeSelection = 0;

        public bool WriteConsole = false;

        //Extractor settings
        public string CustomExtractPath;

        public bool ShowExtractProblem = true;

        public string ListLogFile;

        public bool IgnoreExistingFiles = false;

        public SerializableDictionary<string, string> ExtensionConversion = new SerializableDictionary<string, string>();

        #endregion
    }
}
