namespace Sandbox.ControllerSupport;

public enum CompassDirection
{
    None = 0, // 无方向/中心
    North, // 北
    Northeast, // 东北
    East, // 东
    Southeast, // 东南
    South, // 南
    Southwest, // 西南
    West, // 西
    Northwest, // 西北
}

public static class SelectionDirectionExtensions
{
    extension(CompassDirection)
    {
        public static CompassDirection FromVector2(Vector2 input, float deadzone = 0.2f)
        {
            var x = input.X;
            var y = input.Y;

            // 死区处理
            if (Mathf.Abs(x) < deadzone && Mathf.Abs(y) < deadzone)
                return CompassDirection.None;

            // 确定水平方向
            var isRight = x > deadzone;
            var isLeft = x < -deadzone;

            // 确定垂直方向
            var isUp = y < -deadzone; // 注意：游戏坐标系中上通常是负Y
            var isDown = y > deadzone;

            var result = (isUp, isDown, isRight, isLeft) switch
            {
                (true, _, true, _) => CompassDirection.Northeast,
                (true, _, _, true) => CompassDirection.Northwest,
                (_, true, true, _) => CompassDirection.Southeast,
                (_, true, _, true) => CompassDirection.Southwest,
                (true, _, _, _) => CompassDirection.North,
                (_, true, _, _) => CompassDirection.South,
                (_, _, true, _) => CompassDirection.East,
                (_, _, _, true) => CompassDirection.West,
                _ => CompassDirection.None,
            };

            return result;
        }
    }
}