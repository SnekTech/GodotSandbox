namespace Sandbox.ControllerSupport;

public static class ControllerExtensions
{
    extension(InputEventJoypadMotion joypadMotion)
    {
        public bool IsRightJoystick => joypadMotion is { Axis: JoyAxis.RightX or JoyAxis.RightY };

        public Vector2 GetRightJoystickValue() =>
            new(Input.GetJoyAxis(joypadMotion.Device, JoyAxis.RightX),
                Input.GetJoyAxis(joypadMotion.Device, JoyAxis.RightY));

        public bool IsInDeadzone(float deadzone) => joypadMotion.GetRightJoystickValue().IsShorterThan(deadzone);
    }

    extension(Vector2 vector2)
    {
        bool IsShorterThan(float targetLength) => vector2.LengthSquared() < targetLength * targetLength;
    }
}