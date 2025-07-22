using System.Collections.Generic;
using System.Linq;
using cfGodotEngine.Info;
using Godot;

namespace cfGodotTemplate.Info;

[Tool]
[GlobalClass]
public partial class GameSettingResourceInfo: ResourceInfo<GameSettingInfo>
{
    public override IEnumerable<GameSettingInfo> GetInfos
    {
        get
        {
            return GetMetaList()
                .Where(meta => !meta.ToString().StartsWith('_'))
                .Select(meta => new GameSettingInfo {
                    key = meta.ToString(),
                    value = GetMeta(meta).AsString()
                });
        }
    }
}