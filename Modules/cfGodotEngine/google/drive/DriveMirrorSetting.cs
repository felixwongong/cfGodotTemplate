using cfGodotEngine.Util;
using Godot;
using Godot.Collections;

namespace cfGodotEngine.GoogleDrive;

[GlobalClass]
public partial class DriveMirrorSetting : Setting<DriveMirrorSetting> {
    [Export(PropertyHint.File, "*.json")] private string _serviceAccountCredentialJsonPath;

    public string serviceAccountCredentialJson {
        get
        {
            if (!FileAccess.FileExists(_serviceAccountCredentialJsonPath)) 
            {
                DriveUtil.godotLogger.LogError($"[GDriveMirrorSetting.serviceAccountCredentialJson] File does not exist at path: {_serviceAccountCredentialJsonPath}");
                return string.Empty;
            }

            using var file = FileAccess.Open(_serviceAccountCredentialJsonPath, FileAccess.ModeFlags.Read);
            return file.GetAsText();
        }
    }

    [Export] public string changeChecksumToken = string.Empty;
    [Export] public Array<SettingItem> items;
    [Export] public bool refreshOnEnterPlayMode;

    private System.Collections.Generic.Dictionary<string, SettingItem> _settingMap = new();
    public System.Collections.Generic.Dictionary<string, SettingItem> settingMap {
        get
        {
            UpdateSettingMap();
            return _settingMap;
        }
    }

    private void Refresh() {
        DriveUtil.godotLogger.LogInfo("[GDriveMirrorSetting.Refresh] refresh started");
        DriveMirror.instance.RefreshWithProgressBar().ContinueWith(task => {
            if (task.IsFaulted) {
                DriveUtil.godotLogger.LogError($"[GDriveMirrorSetting.Refresh] refresh failed: {task.Exception}");
            }
            else {
                DriveUtil.godotLogger.LogInfo("[GDriveMirrorSetting.Refresh] refresh succeed");
            }
        });
    }

    private void ForceRefreshAll() {
        DriveUtil.godotLogger.LogInfo("[GDriveMirrorSetting.ClearAllAndRefresh] clear all and refresh started");
        DriveMirror.instance.ClearAllAndRefreshWithProgressBar().ContinueWith(task => {
            if (task.IsFaulted) {
                DriveUtil.godotLogger.LogError($"[GDriveMirrorSetting.ClearAllAndRefresh] clear all and refresh failed: {task.Exception}");
            }
            else {
                DriveUtil.godotLogger.LogInfo("[GDriveMirrorSetting.ClearAllAndRefresh] clear all and refresh succeed");
            }
        });
    }

    private void UpdateSettingMap() 
    {
        _settingMap.Clear();

        foreach (var item in items) {
            if (string.IsNullOrEmpty(item.googleDriveLink)) continue;
            var getUrlInfo = DriveUtil.ParseUrl(item.googleDriveLink);
            if (getUrlInfo.TryGetError(out var error)) {
                DriveUtil.godotLogger.LogException(error);
                continue;
            }

            if (!getUrlInfo.TryGetValue(out var urlInfo))
                continue;

            if (!_settingMap.TryAdd(urlInfo.fileId, item)) {
                var existing = _settingMap[item.googleDriveLink];
                DriveUtil.godotLogger.LogError($"Duplicate googleDriveId ({item.googleDriveLink}) found for ({existing.assetFolderPath}) and ({item.assetFolderPath}), ");
            }
        }
    }
}