using System.Collections;

public class ListExt : IList<double>, IList
{

    private readonly List<double> _inner = new List<double>();

    public IEnumerator<double> GetEnumerator()
    {
        return _inner.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(double item)
    {
        _inner.Add(item);
    }



    public int Add(object value)
    {
        Add((double)value);
        return Count - 1; // Return the index where the item was added
    }

    public bool Contains(object value)
    {
        return Contains((double)value);
    }

    void IList.Clear()
    {
        _inner.Clear();
    }
    void IList.RemoveAt(int index)
    {
        _inner.RemoveAt(index);
    }

    object IList.this[int index]
    {
        get => this[index];
        set => this[index] = (double)value;
    }

    bool IList.IsReadOnly => false;

    int ICollection.Count => Count;

    public int IndexOf(object value)
    {
        return IndexOf((double)value);
    }

    public void Insert(int index, object value)
    {
        Insert(index, (double)value);
    }

    public void Remove(object value)
    {
        Remove((double)value);
    }



    public bool IsFixedSize => throw new NotImplementedException();

    void ICollection<double>.Clear()
    {
        _inner.Clear();
    }

    public bool Contains(double item)
    {
        return _inner.Contains(item);
    }

    public void CopyTo(double[] array, int arrayIndex)
    {
        _inner.CopyTo(array, arrayIndex);
    }

    public bool Remove(double item)
    {
        return _inner.Remove(item);
    }

    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }



    public object SyncRoot => throw new NotImplementedException();
    public bool IsSynchronized => throw new NotImplementedException();

    int Count => _inner.Count;
    int ICollection<double>.Count => Count;


    bool ICollection<double>.IsReadOnly => false;

    public int IndexOf(double item)
    {
        return _inner.IndexOf(item);
    }

    public void Insert(int index, double item)
    {
        _inner.Insert(index, item);
    }

    void IList<double>.RemoveAt(int index)
    {
        _inner.RemoveAt(index);
    }

    public double this[int index]
    {
        get => _inner[index];
        set => _inner[index] = value;
    }
}