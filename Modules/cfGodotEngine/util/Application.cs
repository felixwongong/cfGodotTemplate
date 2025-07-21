using Godot;

namespace cfGodotEngine.Util;

public static class Application 
{
    public static string dataPath => ProjectSettings.GlobalizePath("res://");
    
    public static string persistentDataPath => ProjectSettings.GlobalizePath("user://");
}