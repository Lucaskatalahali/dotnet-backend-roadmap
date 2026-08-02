namespace ArrayStack;

public class ArrayStack<T>
{
    private T[] _items; 
    private int _topOfStack;
    //private int _size; //I will instead use array lenght property

    public ArrayStack(int size)
    {
        if(size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), "size can't be negative or zero");

        _items = new T[size];
        _topOfStack = -1; // it starts pointing to nothing [index 0] doesn't mean nothing, means the first element
    }

    private void Resize(int newSize)
    {
        T[] temp = new T[newSize];

        Array.Copy(_items, 0, temp, 0, Count);
        _items = temp;
    }
    public int Count => _topOfStack + 1;
    public bool IsEmpty => _topOfStack == -1;
    public void Push(T item)
    {
        if(_topOfStack + 1 >= _items.Length)
            Resize(_items.Length * 2);

        _topOfStack++;
        _items[_topOfStack] = item;
    }
    public T Pop()
    {
        if (IsEmpty)
            throw new InvalidOperationException("It's not possible to remove elements from empty list");

        T removedItem = _items[_topOfStack];
        _items[_topOfStack] = default!; // Clears the reference to help the Garbage Collector
        _topOfStack--;
        return removedItem;
    }
    public T Peek()
    {
        if(IsEmpty)
            throw new InvalidOperationException("It's not possible to get elements from empty list");

        return _items[_topOfStack];
    }
    public void Clear()
    {
        Array.Clear(_items, 0, Count);
        _topOfStack = -1;
    }
}