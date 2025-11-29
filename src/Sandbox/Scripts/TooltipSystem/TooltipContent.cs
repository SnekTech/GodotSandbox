namespace Sandbox.TooltipSystem;

public record TooltipContent(string Title, string Content);

public static class TooltipContentExtensions
{
    extension(TooltipContent)
    {
        public static TooltipContent New(string title, string content) => new(title, content);
    }

    extension(CancellationTokenSource cancellationTokenSource)
    {
        public void CancelAndDispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}