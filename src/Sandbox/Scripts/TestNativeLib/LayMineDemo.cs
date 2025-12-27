using Dumpify;
using MineSweeperTools;

namespace Sandbox.TestNativeLib;

[SceneTree]
public partial class LayMineDemo : Node
{
    public override void _Ready()
    {
        var bombMatrix =
            LayMineEngine.LayMineSolvable(16, 30, 99, 1, 1);

        _.Label.Text = bombMatrix.DumpText();
    }
}