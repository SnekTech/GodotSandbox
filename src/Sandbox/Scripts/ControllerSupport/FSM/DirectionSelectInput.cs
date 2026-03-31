namespace Sandbox.ControllerSupport.FSM;

abstract record DirectionSelectInput;

record Start : DirectionSelectInput;

record Cancel : DirectionSelectInput;

record Move(Vector2 AxisValue) : DirectionSelectInput;

record NoInput : DirectionSelectInput;

static class DirectionSelectInputExtensions
{
    extension(DirectionSelectInput)
    {
        internal static DirectionSelectInput FromInputEvent(InputEvent inputEvent) =>
            inputEvent switch
            {
                InputEventJoypadMotion or InputEventJoypadButton =>
                    DirectionSelectInput.FromControllerInput(inputEvent),
                _ => new NoInput(),
            };

        static DirectionSelectInput FromControllerInput(InputEvent inputEvent) =>
            inputEvent switch
            {
                InputEventJoypadButton { ButtonIndex: JoyButton.LeftShoulder, Pressed: true } => new Start(),
                InputEventJoypadButton { ButtonIndex: JoyButton.LeftShoulder, Pressed: false } => new Cancel(),
                InputEventJoypadMotion { IsRightJoystick: true } joypadMotion =>
                    new Move(joypadMotion.GetRightJoystickValue()),
                _ => new NoInput(),
            };
    }
}