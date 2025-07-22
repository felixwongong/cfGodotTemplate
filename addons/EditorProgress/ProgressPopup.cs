namespace cfGodotTemplate.Plugin.EditorProgress;

using Godot;

[Tool]
public partial class ProgressPopup : Window
{
    private ProgressBar _progressBar;

    public override void _Ready()
    {
        Title = "Working...";
        Size = new Vector2I(500, 80);

        var vbox = new VBoxContainer()
        {
            Alignment = BoxContainer.AlignmentMode.Center,
            SizeFlagsHorizontal = Control.SizeFlags.ExpandFill,
            SizeFlagsVertical = Control.SizeFlags.ExpandFill
        };
        
        vbox.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        
        AddChild(vbox);

        _progressBar = new ProgressBar
        {
            MinValue = 0,
            MaxValue = 100
        };
        
        vbox.AddChild(_progressBar);
    }

    public void ShowProgress(string message, float percent)
    {
        Title = message;
        _progressBar.Value = percent;
        PopupCentered();
    }

    public void UpdateProgress(string message, float percent)
    {
        Title = message;
        _progressBar.Value = percent;
    }

    public void ClosePopup()
    {
        Hide();
    }
}
