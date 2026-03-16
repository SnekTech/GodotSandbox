namespace SandboxTests.ControllerSupport;

public class BasicSpecs
{
    [Test]
    public async Task MyTest()
    {
        await Assert.That(true).IsTrue();
    }
}