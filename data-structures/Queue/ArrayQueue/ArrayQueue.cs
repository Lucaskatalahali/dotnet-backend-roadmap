namespace ArrayQueue;

public class ArrayQueue<T>
{
    private T[] _items;
    private int _head;
    private int _tail;
    private int _count;

    public ArrayQueue(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

        _items = new T[capacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }

    public int Count => _count;
    public int Capacity => _items.Length;
    public bool IsEmpty => _count == 0;

    // Add to the end
    public void Enqueue(T item)
    {
        if (_count == _items.Length)
            Resize(_items.Length * 2);

        _items[_tail] = item;
        
        // Uses the % operator to wrap around the array if the end is reached
        _tail = (_tail + 1) % _items.Length; 
        _count++;
    }

    // Remove from the beggining and return the removed element
    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Cannot dequeue from an empty queue.");

        T item = _items[_head];
        _items[_head] = default!; // Clears the reference for the Garbage Collector.

        // Moves the start pointer using circular wrapping
        _head = (_head + 1) % _items.Length;
        _count--;

        return item;
    }

    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Cannot peek an empty queue.");

        return _items[_head];
    }

    public void Clear()
    {
        if (_count > 0)
        {
            // Clears occupied positions
            if (_head < _tail)
            {
                Array.Clear(_items, _head, _count);
            }
            else
            {
                // If the queue has wrapped around the array, clear both parts
                Array.Clear(_items, _head, _items.Length - _head);
                Array.Clear(_items, 0, _tail);
            }
        }

        _head = 0;
        _tail = 0;
        _count = 0;
    }

    // Resizes and reorganizes the array into the correct linear format
    private void Resize(int newCapacity)
    {
        T[] newArray = new T[newCapacity];

        // Copies the elements, aligning the Head at position 0 of the new array
        for (int i = 0; i < _count; i++)
        {
            newArray[i] = _items[(_head + i) % _items.Length];
        }

        _items = newArray;
        _head = 0;
        _tail = _count; // The new tail is located immediately after the last copied element
    }
}