using cfEngine.Logging;
using cfEngine.Serialize;
using cfGodotEngine.Core;
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
    }

    private static void RegisterJsonConverters()
    {
        var converters = JsonSerializer.Instance.OPTIONS.Converters;
    }
}