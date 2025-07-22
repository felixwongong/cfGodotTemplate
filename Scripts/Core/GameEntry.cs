using cfEngine.Core;
using cfEngine.Info;
using cfEngine.IO;
using cfEngine.Logging;
using cfEngine.Serialize;
using cfGodotEngine.Asset;
using cfGodotEngine.Core;
using cfGodotEngine.GoogleDrive;
using cfGodotEngine.Info;
using cfGodotEngine.Util;
using cfGodotTemplate.Info;
using cfGodotTemplate.Util;
using CofyDev.Xml.Doc;
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
        
        InfoBuildByte();

        var game = new Game();
        var monitor = ResourceLoadMonitor.Instance;
    }

    private static void RegisterJsonConverters()
    {
        var converters = JsonSerializer.Instance.OPTIONS.Converters;
    }

    private static void InfoBuildByte()
    {
        DataObject.Decoder.RegisterDecoder(new JsonElementDecoder());

        var editorInfos = new InfoManager[] {
            new GameSettingInfoManager(
                new ResourceInfoLoader<GameSettingInfo>("res://Info/GameSettingInfo/GameSettingInfo.tres")),
        };

        Log.LogInfo($"[GameExtension.InfoBuildByte] Start building info byte files, Count: {editorInfos.Length}");
        foreach (var info in editorInfos) {
            info.LoadInfo();
        }

        var serializer = JsonSerializer.Instance;
        foreach (var infoManager in editorInfos) {
            var allValue = infoManager.GetAllValue();
            var serialized = serializer.Serialize(allValue);
            InfoUtil.CreateStorage(infoManager.infoType).Save("data.json", serialized);
        }

        return;

        ExcelByteLoader<T> CreateExcelLoader<T>()
        {
            return new ExcelByteLoader<T>(
                new LocalFileStorage(Application.assetDataPath),
                new DataObjectEncoder());
        }
    }
}