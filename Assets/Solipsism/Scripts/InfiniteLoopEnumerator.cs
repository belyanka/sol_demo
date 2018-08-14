using System.Collections;
using System.Collections.Generic;

public class InfiniteLoopEnumerator<T> : IEnumerator<T>
{
    private readonly IEnumerator<T> _enumerator;

    public InfiniteLoopEnumerator(IEnumerable<T> enumerable)
    {
        _enumerator = enumerable.GetEnumerator();
    }

    public bool MoveNext()
    {
        if (!_enumerator.MoveNext()) Reset();
        return true;
    }

    public void Reset()
    {
        _enumerator.Reset();
    }

    T IEnumerator<T>.Current
    {
        get { return _enumerator.Current; }
    }

    public object Current
    {
        get { return _enumerator.Current; }
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}