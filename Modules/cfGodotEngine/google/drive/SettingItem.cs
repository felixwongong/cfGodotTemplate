using Godot;
using Godot.Collections;

namespace cfGodotEngine.GoogleDrive;

[Tool]
[GlobalClass]
public partial class SettingItem: Resource {
    [Export] public string fileName;
    [Export] public string assetPath;
    [Export] public string driveLink;
}