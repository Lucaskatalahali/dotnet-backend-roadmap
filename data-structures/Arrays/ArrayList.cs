namespace Arrays;

public class ArrayList<T>
{
    private T[] _items;
    public int _count;
    public int Capacity {get; private set;}

    public ArrayList(int capacity)
    {
        Capacity = capacity;
        _count = 0;
        _items = new T[Capacity];
    }

    private void Resize(int newCapacity)
    {
        T[] newArrayList = new T[newCapacity];
        for(int i = 0; i < Capacity; i++)
        {
            newArrayList[i] = _items[i];
        }
        
        _items = newArrayList;
        Capacity = newCapacity;
    }

    public void Add(T item)
    {
        AddAt(_count, item);
    }

    public void AddAt(int index, T item)
    {
        if(index < 0 || index > _count)
        {
            throw new IndexOutOfRangeException();           
        }
        if(_count == Capacity) Resize(Capacity * 2);

        for(int i = _count; i > index; i--)
        {
            _items[i] = _items[i - 1];
        }
        _items[index] = item;
        _count++;
    }
    
    public T GetAt(int index)
    {
        try
        {
            return _items[index];
        }
        catch (IndexOutOfRangeException)
        {
            throw;
        }
    }

    public bool Remove(T item)
    {
        if(IsEmpty()) throw new InvalidOperationException();

        int index = IndexOf(item);
        if(index == -1) return false;

        RemoveAt(index);
        return true;
    }
    public bool RemoveAt(int index)
    {
        if(index < 0 || index >= _count)
        {
            throw new IndexOutOfRangeException();           
        }

        for(int i = index; i < _count; i++)
        {
            _items[i] = _items[i + 1];
        }
        _count--; 
        return true;  
    }
    public void Clear()
    {
        _count = 0;
    }

    public bool IsEmpty()
    {
        return _count == 0;
    }
    public bool Contains(T item)
    {
        foreach(T thisItem in _items)
        {
            if(EqualityComparer<T>.Default.Equals(thisItem, item))
            return true;
        }   
        return false;
    }

    public int IndexOf(T item)
    {
        for(int i = 0; i < _count; i++)
        {
            if(EqualityComparer<T>.Default.Equals(_items[i], item))
            return i;
        }
        return -1;
    }
}