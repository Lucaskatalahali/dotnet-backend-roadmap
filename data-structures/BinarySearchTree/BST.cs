namespace BinarySearchTree;
public class BST<T> where T : IComparable<T>
{
    private Node<T>? _root;

    public BST()
    {
        _root = null;
    }

    public bool IsEmpty() => _root == null;

    public void Add(T data)
    {
        _root = AddRecursive(_root, data);
    }

    private Node<T> AddRecursive(Node<T>? current , T data)
    {
        if(current == null) return new Node<T>(data);

        else if(current.Data.CompareTo(data) > 0)
            current.Left = AddRecursive(current.Left, data);

        else if(current.Data.CompareTo(data) < 0)
            current.Right = AddRecursive(current.Right, data);
        
        //if equal (duplicated) we will ignore it in order to keep unique values.
        return current; //returning root so that _root points to it again.
    }

    public void Remove(T data)
    {
        _root = RemoveRecursive(_root, data);
    }
    
    private Node<T>? RemoveRecursive(Node<T>? current, T data)
    {
        if(current is null) 
            return null;

        if(current.Data.CompareTo(data) > 0) 
            current.Left = RemoveRecursive(current.Left, data);

        else if(current.Data.CompareTo(data) < 0) current.Right = RemoveRecursive(current.Right, data);

        else
        {
            //found the node to be removed
            //1st case: leaf node
            if(current.Left is null && current.Right is null)
                return null;

            //2nd case: There is one child:
            if(current.Left is null)
                return current.Right;
            
            if(current.Right is null)
                return current.Left;

            //3rd case: node has two children 
            T minRightValue = GetMinValue(current.Right);
            current.Data = minRightValue;

            //remove duplicated value;
            current.Right = RemoveRecursive(current.Right, minRightValue);
        }

        return current;
    }

    private T GetMinValue(Node<T> node)
    {
        Node<T>? current = node;

        while(current.Left is not null)
        {
            current = current.Left;
        }

        return current.Data;
    }

    public bool Contains(T data)
    {
        return SearchRecursive(_root, data);
    }
    private bool SearchRecursive(Node<T>? _current, T data)
    {
        if(_current is null) 
            return false;
        
        if(_current.Data.CompareTo(data) == 0)
            return true;
        
        return _current.Data.CompareTo(data) > 0
            ? SearchRecursive(_current.Left, data)
            : SearchRecursive(_current.Right, data);
    }

    public void InOrder()
    {
        PrintInOrder(_root);
        Console.WriteLine();
    }

    private void PrintInOrder(Node<T>? node)
    {
        if(node is not null)
        {
            PrintInOrder(node.Left);
            Console.WriteLine(node.Data);
            PrintInOrder(node.Right);   
        }
    }

    //private class for NODE
    private class Node<TNode> where TNode: IComparable<TNode>
    {
        public TNode Data {get; set;}
        public Node<TNode>? Left{get; set;}
        public Node<TNode>? Right{get; set;} 

        public Node(TNode data, Node<TNode>? left = null, Node<TNode>? right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }
    
}