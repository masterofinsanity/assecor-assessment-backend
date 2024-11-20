


namespace AssecorAssessmentBackendTests;

public struct MockAsyncEnumerable<T> : IAsyncEnumerable<T>
{
    private readonly T[] _items;

    public MockAsyncEnumerable(params T[] items) {
        _items = items;
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new MockAsyncEnumerator<T>(_items);;
    }
}

public sealed class MockAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly T[] _items;
    private readonly int _count;
    private int _currentPosition = 0;


    public T Current => _items[_currentPosition];

    public MockAsyncEnumerator(T[] items) {
        _items = items;
        _count = items.Length;
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> MoveNextAsync()
    {
        if (_currentPosition < _count) {
            _currentPosition += 1;
            return ValueTask.FromResult(true);
        }

        return ValueTask.FromResult(false);
    }
}