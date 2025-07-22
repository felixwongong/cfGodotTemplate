using cfEngine.Core;
using cfEngine.Info;
using cfEngine.IO;
using cfEngine.Logging;
using cfEngine.Serialize;
using cfGodotEngine.Core;
using cfGodotEngine.Info;
using cfGodotEngine.Util;
using cfGodotTemplate.Info;
using cfGodotTemplate.Util;
using CofyDev.Xml.Doc;

namespace cfGodotTemplate.Core;

public static partial class GameExtension
{
    public static void InfoBuildByte(this Game game)
    {
        Log.SetLogger(new GodotLogger());
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