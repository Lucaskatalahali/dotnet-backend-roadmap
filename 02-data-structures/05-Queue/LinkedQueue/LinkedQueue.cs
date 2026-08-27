namespace LinkedQueue;
public class LinkedQueue<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;
    private int _count;

    public LinkedQueue()
    {
        _head = null;
        _tail = null;
        _count = 0;
    }
    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public void Enqueue(T item)
    {
        if (IsEmpty)
        {
            _head = _tail = new Node<T>(item);
        }
        else
        {
           _tail!.Next = new Node<T>(item); 
           _tail = _tail.Next;
        }
        _count++;
    }

    public T Dequeue()
    {
        if(IsEmpty)
        throw new InvalidOperationException("Can not dequeue from an empty queue.");

        T removedData = _head!.Data;
        _head = _head!.Next;
        _count--;

        if(IsEmpty)
            _tail = null;

        return removedData;
    }

    public T Peek()
    {
        if(IsEmpty)
            throw new InvalidOperationException("Can not peek an empty queue");
        
        return _head!.Data;
    }

    public void Clear()
    {
        _head = _tail = null;
        _count = 0;
    }

    public void Print()
    {
        Node<T>? current = _head;
        while(current is not null)
        {
            Console.Write(current.Data);
            current = current.Next;
            if(current is not null)
                Console.Write(", ");
        }  
    }

    private class Node<TNode> //I didn't use T as parametre because it's alreaby being used by external class
    {
        public TNode Data{get;}
        public Node<TNode>? Next{get; set;}

        public Node(TNode data, Node<TNode>? next = null)
        {
            Data = data;
            Next = next;
        }
    }
}