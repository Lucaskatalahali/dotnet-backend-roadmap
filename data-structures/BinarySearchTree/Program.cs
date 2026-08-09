using BinarySearchTree;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== BINARY SEARCH TREE TEST ===\n");

        var bst = new BST<int>();

        // 1. Testing IsEmpty
        Console.WriteLine($"Is tree empty? {bst.IsEmpty()}"); // True

        // 2. Inserting Elements
        //          105
        //        /     \
        //      67       223
        //     /  \     /   \
        //   54    90  197  546
        //  /                 \
        // 47                 571
        bst.Add(105);
        bst.Add(67);
        bst.Add(223);
        bst.Add(197);
        bst.Add(54);
        bst.Add(47);
        bst.Add(90);
        bst.Add(546);
        bst.Add(571);

        Console.WriteLine($"Is tree empty after insertions? {bst.IsEmpty()}\n"); // False

        // 3. Testing Traversals
        Console.Write("In-Order (Sorted): ");
        bst.PrintInOrder(); // 47 54 67 90 105 197 223 546 571
        Console.WriteLine();

        Console.Write("Pre-Order (Root-L-R): ");
        bst.PrintPreOrder(); // 105 67 54 47 90 223 197 546 571
        Console.WriteLine();

        Console.Write("Post-Order (L-R-Root): ");
        bst.PrintPostOrder(); // 47 54 90 67 197 571 546 223 105
        Console.WriteLine();

        // 4. Testing Height and Search
        Console.WriteLine($"\nTree Height: {bst.Height()}"); // 3
        Console.WriteLine($"Contains 90? {bst.Contains(90)}"); // True
        Console.WriteLine($"Contains 95? {bst.Contains(95)}"); // False

        // 5. Testing Removal (3 Cases)
        Console.WriteLine("\n--- Testing Removal ---");

        // Case 1: Leaf node (47)
        bst.Remove(47);
        Console.Write("After removing 47 (Leaf): ");
        bst.PrintInOrder();
        Console.WriteLine();

        // Case 2: Node with 1 child (546)
        bst.Remove(546);
        Console.Write("After removing 546 (1 Child): ");
        bst.PrintInOrder();
        Console.WriteLine();

        // Case 3: Node with 2 children (Root 105)
        bst.Remove(105);
        Console.Write("After removing 105 (Root): ");
        bst.PrintInOrder();
        Console.WriteLine();

        // 6. Testing Clear
        bst.Clear();
        Console.WriteLine($"\nAfter Clear(), is empty? {bst.IsEmpty()}"); // True
    }
}