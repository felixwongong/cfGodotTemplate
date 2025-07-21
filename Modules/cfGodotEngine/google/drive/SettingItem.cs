using Godot;
using Godot.Collections;

namespace cfGodotEngine.GoogleDrive;

[GlobalClass]
public partial class SettingItem: Resource {
    [Export] public string googleFileName;
    [Export] public string assetNameOverride;
    [Export] public string assetFolderPath;
    [Export] public string googleDriveLink;
    [Export] public Array<string> googleFiles;

    public void ClearCache() {
        googleFileName = string.Empty;
    }
}