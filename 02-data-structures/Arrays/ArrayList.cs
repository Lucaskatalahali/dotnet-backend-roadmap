namespace Arrays;

public class ArrayList<T>
{
    private T[] _items;
    private int _count;
    public ArrayList(int capacity)
    {
        if(capacity <= 0)
        throw new ArgumentOutOfRangeException(nameof(capacity)); 
        _count = 0;
        _items = new T[capacity];
    }

    public int Count => _count;
    public int Capacity => _items.Length;

    //Access by index
    public T this[int index]
    {
        get
        {
            if(index < 0 || index >= _count)
            throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];   
        }
        set
        {
            if(index < 0 || index >= _count)
            throw new ArgumentOutOfRangeException(nameof(index));
             _items[index] = value;  
        }
    }
    private void Resize(int newSize)
    {
        T[] temp = new T[newSize];

        Array.Copy(_items, 0, temp, 0, Count);
        _items = temp;
    }

    //Adds an item to the end of the list
    public void Add(T item)
    {
        InsertAt(_count, item);
    }

    public void InsertAt(int index, T item)
    {
        if(index < 0 || index > _count)
            throw new ArgumentOutOfRangeException(nameof(index));           
    
        if(_count == _items.Length) 
            Resize(_items.Length * 2);

        // shifts elements to the right, from back to front
        for(int i = _count; i > index; i--)
        {
            _items[i] = _items[i - 1];
        }
        _items[index] = item;
        _count++;
    }
    
    public T GetAt(int index)
    {
        if(index < 0 || index >= _count) throw new ArgumentOutOfRangeException(nameof(index));
        return _items[index];
    }
    public bool Remove(T item)
    {
        if(IsEmpty) throw new InvalidOperationException();

        int index = IndexOf(item);
        if(index == -1) return false;

        RemoveAt(index);
        return true;
    }

    //Removes an element by index. O(n) — requires shifting elements to the
    //right one position back to close the "gap"
    public bool RemoveAt(int index)
    {
        if(index < 0 || index >= _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));           
        }

        for(int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        _count--; 
        _items[_count] = default!; //just for good practice, because if the type is a reference type, 
        //Garbage Colletor may not dellete it
        return true;  
    }
    public void Clear()
    {
        Array.Clear(_items, 0, Count);
        _count = 0;
    }

    public bool IsEmpty => _count == 0;
    public bool Contains(T item) => IndexOf(item) != -1;

    public int IndexOf(T item)
    {
        for(int i = 0; i < _count; i++)
        {
            if(EqualityComparer<T>.Default.Equals(_items[i], item))
            return i;
        }
        return -1;
    }
    public void Print()
    {
        Console.Write("[");
        for(int i = 0; i < _count; i++)
        {
            Console.Write(_items[i]);
            if(i != _count - 1)
                Console.Write(", ");
            else
                Console.Write("]");
        }
    }
}
