using cfGodotTemplate.Plugin.EditorProgress;

namespace cfGodotTemplate.Util;

public static class EditorUtility
{
    public static void ShowProgress(float percent, string message)
    {
        EditorProgress.Instance.UpdateProgress(percent, message);
    } 
}