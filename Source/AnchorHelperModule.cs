using System;

namespace Celeste.Mod.AnchorHelper;

public class AnchorHelperModule : EverestModule {
    public static AnchorHelperModule Instance { get; private set; }

    public override Type SettingsType => typeof(AnchorHelperModuleSettings);
    public static AnchorHelperModuleSettings Settings => (AnchorHelperModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(AnchorHelperModuleSession);
    public static AnchorHelperModuleSession Session => (AnchorHelperModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(AnchorHelperModuleSaveData);
    public static AnchorHelperModuleSaveData SaveData => (AnchorHelperModuleSaveData) Instance._SaveData;

    public AnchorHelperModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(AnchorHelperModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(AnchorHelperModule), LogLevel.Info);
#endif
    }

    public const string LoggerTag = nameof(AnchorHelperModule);

    public override void Load() {
        // TODO: apply any hooks that should always be active
    }

    public override void Unload() {
        // TODO: unapply any hooks applied in Load()
    }
}