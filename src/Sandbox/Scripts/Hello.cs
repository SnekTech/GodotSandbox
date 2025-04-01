using Godot;

namespace Sandbox;

public partial class Hello : Node
{
    public override void _Ready()
    {
        GD.Print("Hello from sandbox!");
    }
}