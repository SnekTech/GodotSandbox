namespace Sandbox.CustomDrawing;

[SceneTree]
public partial class CustomDrawingDemo : Node2D
{
    
    public override void _Draw()
    {
        // DrawLine(Vector2.Zero, Vector2.One * 200, Colors.Aqua, 1);
        DrawCircle(Vector2.Zero, 30, Colors.Aqua);
    }
}
