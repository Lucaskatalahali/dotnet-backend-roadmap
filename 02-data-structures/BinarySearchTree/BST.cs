namespace BinarySearchTree;
public class BST<T> where T : IComparable<T>
{
    private Node? _root;

    public BST()
    {
        _root = null;
    }

    public bool IsEmpty() => _root == null;

    public void Add(T data)
    {
        _root = AddRecursive(_root, data);
    }

    private Node AddRecursive(Node? current , T data)
    {
        if(current == null) return new Node(data);

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
    
    private Node? RemoveRecursive(Node? current, T data)
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

    private T GetMinValue(Node node)
    {
        Node? current = node;

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
    private bool SearchRecursive(Node? _current, T data)
    {
        if(_current is null) 
            return false;
        
        if(_current.Data.CompareTo(data) == 0)
            return true;
        
        return _current.Data.CompareTo(data) > 0
            ? SearchRecursive(_current.Left, data)
            : SearchRecursive(_current.Right, data);
    }

    public void PrintInOrder()
    {
        InOrderRecursive(_root);
    }

    private void InOrderRecursive(Node? node)
    {
        if(node is not null)
        {
            InOrderRecursive(node.Left);
            Console.Write($"{node.Data} ");
            InOrderRecursive(node.Right);   
        }
    }

    public void PrintPreOrder()
    {
        PreOrderRecursive(_root);
    }

    private void PreOrderRecursive(Node? node)
    {
        if(node is not null)
        {
            Console.Write($"{node.Data} ");
            PreOrderRecursive(node.Left);
            PreOrderRecursive(node.Right);
        }
    }

    public void PrintPostOrder()
    {
        PostOrderRecursive(_root);
    }

    private void PostOrderRecursive(Node? node)
    {
        if(node is not null)
        {
            PostOrderRecursive(node.Left);
            PostOrderRecursive(node.Right);
            Console.Write($"{node.Data} ");
        }
    }

    public int Height()
    {
        return Height(_root);
    }

    private int Height(Node? node)
    {
        if(node is null)
            return -1;

        return 1 + Math.Max(Height(node?.Left), Height(node?.Right));
    }

    public void Clear()
    {
        _root = null;
    }

    //private class for NODE
    private class Node
    {
        public T Data {get; set;}
        public Node? Left{get; set;}
        public Node? Right{get; set;} 

        public Node(T data, Node? left = null, Node? right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }
    
}