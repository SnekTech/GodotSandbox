using GTweensGodot.Extensions;

namespace Sandbox.TooltipSystem;

[SceneTree]
public partial class Tooltip : Control
{
    private const float FadeDuration = 0.3f;
    
    internal Task ShowAsync(TooltipContent content, Vector2 globalPosition, CancellationToken token)
    {
        Header.Text = content.Title;
        Content.Text = content.Content;
        GlobalPosition = globalPosition;
        Show();
        
        return this.TweenModulateAlpha(1, FadeDuration).PlayAsync(token);
    }

    internal async Task HideAsync(CancellationToken token)
    {
        await this.TweenModulateAlpha(0, FadeDuration).PlayAsync(token);
        Hide();
    }
}