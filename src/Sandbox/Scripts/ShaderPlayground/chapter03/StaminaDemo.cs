using GodotGadgets.Extensions;
using GodotGadgets.Tasks;

namespace Sandbox.ShaderPlayground.chapter03;

public partial class StaminaDemo : Sprite2D
{
    StaminaShader _staminaShader = null!;

    readonly Player _player = new();

    public override void _EnterTree()
    {
        _player.StaminaChanged += OnPlayerStaminaChanged;
    }

    public override void _ExitTree()
    {
        _player.StaminaChanged -= OnPlayerStaminaChanged;
    }

    public override void _Ready()
    {
        _staminaShader = new StaminaShader(this.GetMaterialAs<ShaderMaterial>());
        _player.CurrentStamina = 0.5f;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyInputEvent && keyInputEvent.IsReleased())
        {
            if (keyInputEvent.Keycode == Key.Up)
            {
                _player.CurrentStamina += 0.1f;
            }
            else if (keyInputEvent.Keycode == Key.Down)
            {
                _player.CurrentStamina -= 0.1f;
            }
        }
    }

    void OnPlayerStaminaChanged(PropertyChange<float> change)
    {
        _staminaShader.ChangeToVisual(change.Previous, change.Now, this.GetCancellationTokenOnTreeExit());
    }
}