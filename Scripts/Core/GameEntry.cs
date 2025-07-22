using cfEngine.Core;
using cfEngine.Logging;
using cfEngine.Serialize;
using cfGodotEngine.Core;
using cfGodotEngine.GoogleDrive;
using Godot;

namespace cfGodotTemplate.Core;

public partial class GameEntry: Node
{
    [Export] private LogLevel _logLevel = LogLevel.Debug;
    
    private GameEntry()
    {
        RegisterJsonConverters();
        
        Log.SetLogger(new GodotLogger());
        Log.SetLogLevel(_logLevel);
        Log.LogInfo($"GameEntry initialized. Log level set to: {_logLevel}.");

#if CF_GOOGLE_DRIVE
        var mirrorSetting = DriveMirrorSetting.GetSetting();
        if (mirrorSetting.refreshOnEnterPlayMode) 
            mirrorSetting.Refresh();
#endif
        
        Game.Current.InfoBuildByte();
    }

    private static void RegisterJsonConverters()
    {
        var converters = JsonSerializer.Instance.OPTIONS.Converters;
    }
}