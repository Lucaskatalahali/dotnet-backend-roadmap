using System.Text;

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

        for(int i = 0; i <= _topOfStack; i++)
        {
            temp[i] = _items[i];
        }
        _items = temp;
    }
    public int Count => _topOfStack + 1;
    public bool IsEmpty() => _topOfStack == -1;
    public void Push(T item)
    {
        if(_topOfStack + 1 >= _items.Length)
            Resize(_items.Length * 2);

        _topOfStack++;
        _items[_topOfStack] = item;
    }
    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("It's not possible to remove elements from empty list");

        T removedItem = _items[_topOfStack];
        _items[_topOfStack] = default!; // Clears the reference to help the Garbage Collector
        _topOfStack--;
        return removedItem;
    }
    public T Top()
    {
        if(IsEmpty())
            throw new InvalidOperationException("It's not possible to get elements from empty list");

        return _items[_topOfStack];
    }
    public void Clear()
    {
        while(_topOfStack != -1)
        {
            _items[_topOfStack] = default!;
            _topOfStack--;
        }
    }
}