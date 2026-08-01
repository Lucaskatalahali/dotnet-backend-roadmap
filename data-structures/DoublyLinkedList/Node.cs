namespace DoublyLinkedList;

public class Node<T>
{
    public T Data {get; }
    public Node<T>? Next {get; set;}
    public Node<T>? Previous {get; set;}

    public Node(T data, Node<T>? next = null, Node<T>? previous = null)
    {
        Data = data;
        Next = next;
        Previous = previous;
    }
}
