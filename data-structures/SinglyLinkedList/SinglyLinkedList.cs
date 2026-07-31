using System.Text;

namespace SinlgyLinkedList;

public class SinlgyLinkedList<T>
{
    private Node<T>? _head;
    private int _size;
    public int Count => _size; //it will be used not as a method
    public bool IsEmpty() => _size == 0; //it will be used as a method
    public SinlgyLinkedList()
    {
        _head = null; 
        _size = 0;
    }
    //Function to get the previous Node given an index
    private Node<T>? GetPreviousByIndex(int index)
    {
        if(index < 0 || index > _size)
            throw new ArgumentOutOfRangeException(nameof(index), $"Index can not be less than zero or greater than {_size}");

        Node<T>? temp = _head;
        for(int i = 0; i < index - 1; i++)
        {
            temp = temp?.Next;
        }
        return temp;
    }
    public void Add(T item)
    {
        InsertAt(_size, item);
    }
    public void InsertAt(int index, T item)
    {
        if (index == 0) 
            _head = new Node<T>(item, _head);
        else
        {
            Node<T> newNode = new(item);
            Node <T>? prev = GetPreviousByIndex(index);
            newNode.Next = prev?.Next;
            prev?.Next = newNode; //This line compiles normally starting with C# 14 (.NET 10).

            //Alternatively use this line if your C# version shows error:
            /*if (prev is not null)
            prev.Next = newNode;*/
        } 
        _size++;
    }
    public void RemoveAt(int index)
    {
        if(IsEmpty())
            throw new InvalidOperationException("It is not possible to remove elements from an empty list.");

        if (index < 0 || index >= _size)
        throw new ArgumentOutOfRangeException(nameof(index));
        
        if(index == 0)
        {
            _head = _head!.Next; 
        }
        else
        {
           Node<T>? prev = GetPreviousByIndex(index); 
           prev?.Next = prev?.Next?.Next; //This line compiles normally starting with C# 14 (.NET 10).

            /* Alternatively use this line if your C# version shows error:
            if (prev is not null)
            prev.Next = prev.Next?.Next;*/
        }
        _size--;
    }
    public bool Remove(T item)
    {     
        int index = IndexOf(item);
        if(index == -1) 
            return false; 

        RemoveAt(index);
        return true;
    }
    public int IndexOf(T item)
    {
        Node<T>? temp = _head;
        int index = 0;
        while(temp is not null)
        {
            if (item!.Equals(temp.Data))
                return index;
            index++;
            temp = temp.Next;
        }

        return -1;
    }
    public T GetFirst()
    {
        if(IsEmpty()) //here it could be if _size == 0, because isEmpty uses this conditional.
            throw new InvalidOperationException("It is not possible to get elements from an empty list.");

        return _head!.Data; //Head won't be null here
    }
    public T GetLast()
    {
        if(IsEmpty())
            throw new InvalidOperationException("It is not possible to get elements from an empty list.");
        
        Node<T>? last = GetPreviousByIndex(_size); //This already returns the last node (position _size - 1).
        return last!.Data; //last won't be null here
    }
    public bool Contains(T item) => IndexOf(item) != -1;       
    public void Clear()
    {
        _head = null;
        _size = 0;
    }

    public void Reverse()
    {
        Node<T>? currentNode = _head;
        Node<T>? previousNode = null;
        Node<T>? nextNode;
        while(currentNode is not null)
        {
            nextNode = currentNode.Next;
            currentNode.Next = previousNode;
            previousNode = currentNode;
            currentNode = nextNode;
        } 
        _head = previousNode;
    }
    public override string ToString()
    {
        if(IsEmpty()) return "";
        var txt = new StringBuilder();
        txt.Append('[');
        Node<T>? temp = _head;
        while(temp is not null)
        {
            txt.Append($"{temp.Data}"); 
            temp = temp.Next;
            if(temp is not null)
                txt.Append(", ");
            else
                txt.Append(']'); 
        }

        return txt.ToString();
    }
}