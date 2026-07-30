namespace Arrays;

public class ArrayList<T>
{
    private T[] _items;
    private int _count;
    private int _capacity;

    public ArrayList(int capacity)
    {
        if(capacity <= 0)
        throw new ArgumentOutOfRangeException(); 
        _capacity = capacity;
        _count = 0;
        _items = new T[_capacity];
    }

    public int Count => _count;
    public int Capacity => _capacity;

    private void Resize(int newCapacity)
    {
        T[] newArrayList = new T[newCapacity];
        for(int i = 0; i < _count; i++)
        {
            newArrayList[i] = _items[i];
        }
        
        _items = newArrayList;
        _capacity = newCapacity;
    }

    public void Add(T item)
    {
        AddAt(_count, item);
    }

    public void AddAt(int index, T item)
    {
        if(index < 0 || index > _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));           
        }
        if(_count == _capacity) Resize(_capacity * 2);

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
            throw new ArgumentOutOfRangeException(nameof(index));           
        }

        for(int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        _count--; 
        _items[_count] = default!; //just for good practice, because if the type is a class, Garbage Colletor may not dellete it
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
        for(int i = 0; i < _count; i++)
        {
            if(EqualityComparer<T>.Default.Equals(_items[i], item))
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