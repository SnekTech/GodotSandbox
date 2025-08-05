using GodotGadgets.Extensions;
using R3;

namespace Sandbox.Rx;

public static class RxExtensions
{
    public static IDisposable Dump<T>(this Observable<T> source, string name)
    {
        return source.Subscribe(
            value => $"{name}-->{value}".DumpGd(),
            ex => $"{name} failed-->{ex.Message}".DumpGd(),
            (_) => $"{name} completed".DumpGd());
    }
}