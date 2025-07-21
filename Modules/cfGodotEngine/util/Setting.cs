using Godot;

namespace cfGodotEngine.Util;

public abstract partial class Setting<T>: Resource where T : Setting<T>
{
    public static T GetSetting() 
    {
        var path = $"res://Settings/{typeof(T).Name}.tres";
        var setting = ResourceLoader.Load<T>(path);
        if (setting == null)
        {
            GD.PushError($"Setting {typeof(T).Name} not found in path ({path}).");
            return null;
        }
        return setting;
    }
}