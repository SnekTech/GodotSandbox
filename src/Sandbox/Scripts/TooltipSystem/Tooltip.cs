using GTweensGodot.Extensions;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class Tooltip : Control
{
    internal Task ShowAsync(TooltipContent content, CancellationToken token)
    {
        Header.Text = content.Title;
        Content.Text = content.Content;
        Show();
        
        return this.TweenModulateAlpha(1, 1).PlayAsync(token);
    }

    internal async Task HideAsync(CancellationToken token)
    {
        await this.TweenModulateAlpha(0, 1).PlayAsync(token);
        Hide();
    }
}