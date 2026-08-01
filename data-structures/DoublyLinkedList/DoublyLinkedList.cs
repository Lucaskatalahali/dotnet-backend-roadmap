using System.Text;

namespace DoublyLinkedList;

public class DoublyLinkedList<T>
{
    private Node<T>? _head;
    private int _size;

    public DoublyLinkedList()
    {
        _head = null;
        _size = 0;
    }
    public int Count => _size;
    public bool IsEmpty() => _size == 0;

    private Node<T>? GetPreviousByIndex(int index)
    {
        if(index < 0 || index > _size)
            throw new ArgumentOutOfRangeException(nameof(index), $"Index can not be less than zero or greater than {_size}");

        Node<T>? previous = _head;
        for(int i = 1; i < index; i++)
        {
            previous = previous?.Next;
        } 
        return previous;
    }
    public void Add(T data) //This function add at the end by passing size as argument to InsertAt func.
    {
        InsertAt(_size, data);
    }
    public void InsertAt(int index, T data)
    {
        if(index < 0 || index > _size)
            throw new ArgumentOutOfRangeException(nameof(index), $"Index can not be less than zero or greater than {_size}");

        if (IsEmpty())
        {
            _head = new Node<T>(data);
        }
        else if(index == 0)
        {
            Node<T> newNode = new(data, next: _head, previous: null);
            _head?.Previous = newNode;
            _head = newNode;
        }
        else
        {
            Node<T>? previous = GetPreviousByIndex(index);
            Node<T> newNode = new(data, next: previous?.Next, previous: previous); //The contructor of Node assigns next and previous 
    
            previous?.Next?.Previous = newNode;
            previous?.Next = newNode;
        }
        _size++;
    }
    public bool Remove(T data)
    {
        int index = IndexOf(data);
        if(index == -1)
            return false;

        RemoveAt(index);
        return true;
    }
    public void RemoveAt(int index)
    {
        if(IsEmpty())
            throw new InvalidOperationException("It is not possible to remove elements from an empty list.");

        if(index < 0 || index >= _size)// GetPreviousByIndex doesn't verify if index == size. We need to verify it here
            throw new ArgumentOutOfRangeException(nameof(index)); 

        if(index == 0)
        {
            _head = _head?.Next;
            _head?.Previous = null;
        }
        else
        {
            Node<T>? previous = GetPreviousByIndex(index);  
            previous?.Next = previous?.Next?.Next;
            previous?.Next?.Previous = previous; //"?" makes null pointers be save here.
        }
        _size--;
    }
    public int IndexOf(T data)
    {
        int index = 0;
        Node<T>? current = _head;
        while(current is not null)
        {
            if(current.Data!.Equals(data))
                return index;
            current = current.Next;
            index++;
        }
        return -1;
    }
    public T GetFirst()
    {
        if(IsEmpty())
            throw new InvalidOperationException("It is not possible to get elements from an empty list.");

        return _head!.Data; //head will not be null here
    }
    public T GetLast()
    {
        if(IsEmpty())
            throw new InvalidOperationException("It is not possible to get elements from an empty list.");
        
        Node<T>? current = _head;
        while(current!.Next is not null)
        {
            current = current.Next;
        }
        return current!.Data;
    }
    public bool Contains(T data) => IndexOf(data) != -1;
    public void Clear()
    {
        _head = null;
        _size = 0;
    }

    /*public void Reverse()
    {
        
    }*/
    public override string ToString()
    {
        if(IsEmpty())
            return "";

        var txt = new StringBuilder();
        txt.Append('[');
        Node<T>? current = _head; 

        while(current is not null)
        {
            if(current.Next is null)
                txt.Append($"{current.Data}]");
            else    
                txt.Append($"{current.Data}, ");
            current = current.Next;
        }
        return txt.ToString();
    }
}