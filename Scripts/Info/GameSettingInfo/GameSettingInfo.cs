using System;
using System.Collections.Generic;
using cfEngine;
using cfEngine.Info;

namespace cfGodotTemplate.Info;

public class GameSettingInfoManager : ConfigInfoManager<string, GameSettingInfo>
{
    protected override Func<GameSettingInfo, string> keyFn => x => x.key;

    public GameSettingInfoManager(IValueLoader<GameSettingInfo> loader) : base(loader)
    {
    }

    public Res<string, Exception> GetStringValue(string id)
    {
        if (ValueMap.TryGetValue(id, out var info)) {
            return Res.Ok(info.value);
        }

        return Res.Err<string>(new KeyNotFoundException($"Game setting not found: {id}"));
    }

    public Res<int, Exception> GetIntValue(string id)
    {
        if (ValueMap.TryGetValue(id, out var info) && int.TryParse(info.value, out var intValue)) {
            return Res.Ok(intValue);
        }

        return Res.Err<int>(new KeyNotFoundException($"Game setting not found: {id}"));
    }

    public Res<float, Exception> GetFloatValue(string id)
    {
        if (ValueMap.TryGetValue(id, out var info) && float.TryParse(info.value, out var floatValue)) {
            return Res.Ok(floatValue);
        }

        return Res.Err<float>(new KeyNotFoundException($"Game setting not found: {id}"));
    }

    public Res<bool, Exception> GetBoolValue(string id)
    {
        if (ValueMap.TryGetValue(id, out var info) && bool.TryParse(info.value, out var boolValue)) {
            return Res.Ok(boolValue);
        }

        return Res.Err<bool>(new KeyNotFoundException($"Game setting not found: {id}"));
    }
}

[Serializable]
public class GameSettingInfo
{
    public string key { get; set; }
    public string value { get; set; }
}