using System;
using Celeste;
using Celeste.Mod;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System.Linq;

namespace Celeste.Mod.AnchorHelper
{

    public class AnchorHelperModule : EverestModule
    {
        public static AnchorHelperModule Instance { get; private set; }

        public override Type SettingsType => typeof(AnchorHelperModuleSettings);
        public static AnchorHelperModuleSettings Settings => (AnchorHelperModuleSettings)Instance._Settings;

        public override Type SessionType => typeof(AnchorHelperModuleSession);
        public static AnchorHelperModuleSession Session => (AnchorHelperModuleSession)Instance._Session;

        public override Type SaveDataType => typeof(AnchorHelperModuleSaveData);
        public static AnchorHelperModuleSaveData SaveData => (AnchorHelperModuleSaveData)Instance._SaveData;

        public const string LoggerTag = nameof(AnchorHelperModule);

        public AnchorHelperModule()
        {
            Instance = this;
#if DEBUG
            Logger.SetLogLevel(LoggerTag, LogLevel.Verbose);
#else
            Logger.SetLogLevel(LoggerTag, LogLevel.Info);
#endif
        }

        public override void Load()
        {
            Everest.Events.LevelLoader.OnLoadingThread += AddAnchorManager;
        }

        public override void Unload()
        {
            Everest.Events.LevelLoader.OnLoadingThread -= AddAnchorManager;
        }

        private void AddAnchorManager(Level level)
        {
            if (!Session.ManagerDeleted && !level.Tracker.GetEntities<AnchorManager>().Any())
            {
                level.Add(new AnchorManager());
            }
        }
    }
}
