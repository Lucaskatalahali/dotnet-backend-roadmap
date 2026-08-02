namespace LinkedStack;

public class LinkedStack<T>
{
    private Node<T>? _topOfStack;
    private int _count;
    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public LinkedStack()
    {
        _topOfStack = null;
        _count = 0;
    }

    public void Push(T data)
    {
        _topOfStack = new Node<T>(data, _topOfStack);
        _count++;
    }

    public T Pop()
    {
        if(IsEmpty)
            throw new InvalidOperationException("Can not remove from empty list");

        Node<T>? removedNode = _topOfStack; 
        T removedData = removedNode!.Data; //here top (removedNode) isn't null or default
        _topOfStack = _topOfStack!.Next; //Garbage Collector handles the deallocation of all unlinked nodes in the chain.
        _count--;

        return removedData;
    }

    public T Peek()
    {
        if(IsEmpty)
            throw new InvalidOperationException("Can not peek an empty list");
        return _topOfStack!.Data;
    }

    public void Clear()
    {
        _topOfStack = null;
        _count = 0;
    }
}