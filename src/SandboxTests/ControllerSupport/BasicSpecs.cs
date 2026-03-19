using Godot;
using Sandbox.ControllerSupport;

namespace SandboxTests.ControllerSupport;

public record VectorToDirectionTestData(Vector2 Vec2, CompassDirection Direction, float Deadzone = 0.2f);

public class BasicSpecs
{
    [Test]
    [MethodDataSource(nameof(GetBasicTestData))]
    public async Task selection_direction_from_vec2(VectorToDirectionTestData testData)
    {
        var (vec2, expected, deadzone) = testData;

        var actual = CompassDirection.FromVector2(vec2, deadzone);

        await Assert.That(actual).IsEqualTo(expected);
    }

    public static IEnumerable<Func<VectorToDirectionTestData>> GetBasicTestData()
        =>
        [
            () => new VectorToDirectionTestData(new Vector2(0.5f, 0), CompassDirection.East),
            () => new VectorToDirectionTestData(new Vector2(-0.5f, 0), CompassDirection.West),
            () => new VectorToDirectionTestData(new Vector2(0.5f, 0.4f), CompassDirection.Southeast),
            () => new VectorToDirectionTestData(new Vector2(0f, 0.4f), CompassDirection.South),
            () => new VectorToDirectionTestData(new Vector2(0f, -0.4f), CompassDirection.North),
            () => new VectorToDirectionTestData(new Vector2(0f, 0f), CompassDirection.None),
            
            // deadzone data
            () => new VectorToDirectionTestData(new Vector2(0.2f, 0), CompassDirection.None, 0.3f),
            () => new VectorToDirectionTestData(new Vector2(0.4f, 0), CompassDirection.East, 0.3f),
            () => new VectorToDirectionTestData(new Vector2(0.4f, 0.2f), CompassDirection.East, 0.3f),
            () => new VectorToDirectionTestData(new Vector2(0.2f, 0.4f), CompassDirection.South, 0.3f),
        ];
}