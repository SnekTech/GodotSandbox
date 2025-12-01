using GodotGadgets.Extensions;

namespace Sandbox.TooltipSystem;

[GlobalClass]
public sealed partial class Area2DTooltipTrigger : TooltipTrigger
{
    private Area2D _parent = null!;
    private Rect2 _collisionShapeRect;

    public override Rect2 TargetGlobalRect => _collisionShapeRect with { Position = _parent.GlobalPosition };

    public override void _EnterTree()
    {
        _parent = GetParent<Area2D>();
        _collisionShapeRect = _parent.CollisionShape.GetShape().GetRect();
        ResetCollisionRectOriginToTopLeft(_collisionShapeRect.Size);

        _parent.MouseEntered += OnMouseEntered;
        _parent.MouseExited += OnMouseExited;
    }

    public override void _ExitTree()
    {
        _parent.MouseEntered -= OnMouseEntered;
        _parent.MouseExited -= OnMouseExited;
    }

    private void ResetCollisionRectOriginToTopLeft(Vector2 rectSize)
    {
        _parent.CollisionShape.Position = rectSize / 2;
    }
}