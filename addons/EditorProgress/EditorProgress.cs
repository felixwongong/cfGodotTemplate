#if TOOLS
using Godot;

namespace cfGodotTemplate.Plugin.EditorProgress;

[Tool]
public partial class EditorProgress : EditorPlugin
{
    public static EditorProgress Instance { get; private set; }
    
    private ProgressPopup _progressPopup;
    private bool showedProgress = false;

    public override void _EnterTree()
    {
        _progressPopup = new ProgressPopup();
        showedProgress = false;
        Instance = this;
    }

    public void UpdateProgress(float percent, string text)
    {
        if (!showedProgress)
        {
            GetTree().Root.AddChild(_progressPopup);
            _progressPopup.ShowProgress(text, percent);
        }
        else
        {
            if (percent > 100)
                FinishProgress();
            else 
                _progressPopup.UpdateProgress(text, Mathf.Clamp(percent, 0, 100));
        }
    }

    public void FinishProgress()
    {
        _progressPopup?.ClosePopup();
        GetTree().Root.RemoveChild(_progressPopup);
        showedProgress = false;
    }

    public override void _ExitTree()
    {
        _progressPopup?.QueueFree();
        showedProgress = false;
        Instance = null;
    }
}
#endif
