using Arrays;
class Program
{
    static void Main()
    {
        Console.WriteLine("    TESTING CUSTOM ARRAYLIST (C#)       ");
        Console.WriteLine("========================================\n");

        // 1. Initializing ArrayList with low capacity (2) to test Resize
        var list = new ArrayList<string>(2);
        Console.WriteLine($"List created! Is Empty? {list.IsEmpty} | Count: {list.Count} | Capacity: {list.Capacity}");

        // 2. Testing Add() and Dynamic Resize
        Console.WriteLine("\n--- 1. Testing Add() & Resize ---");
        list.Add("Alpha");
        list.Add("Beta");
        Console.WriteLine($"Capacity before overflow: {list.Capacity}");

        // Triggering Resize (Capacity should double from 2 to 4)
        list.Add("Charlie");
        Console.WriteLine($"Added 'Charlie'. Count: {list.Count} | Capacity: {list.Capacity} (Expected Capacity: 4)");
        
        Console.Write("Current List Content: ");
        list.Print();
        Console.WriteLine();

        // 3. Testing Indexer (this[]) and GetAt()
        Console.WriteLine("\n--- 2. Testing Indexer & GetAt() ---");
        Console.WriteLine($"Item at index 0: '{list[0]}'");
        Console.WriteLine($"Item at index 2 (GetAt): '{list.GetAt(2)}'");

        // Modifying item via indexer
        list[1] = "Beta Updated";
        Console.WriteLine($"Updated index 1 to '{list[1]}'");

        // 4. Testing InsertAt()
        Console.WriteLine("\n--- 3. Testing InsertAt() ---");
        Console.WriteLine("Inserting 'Inserted Item' at index 1...");
        list.InsertAt(1, "Inserted Item");
        
        Console.Write("List Content after InsertAt: ");
        list.Print();
        Console.WriteLine($"\nNew Count: {list.Count}");

        // 5. Testing IndexOf() and Contains()
        Console.WriteLine("\n--- 4. Testing IndexOf() & Contains() ---");
        Console.WriteLine($"Contains 'Charlie'? {list.Contains("Charlie")} (Expected: True)");
        Console.WriteLine($"Index of 'Charlie': {list.IndexOf("Charlie")} (Expected: 3)");
        Console.WriteLine($"Contains 'Zebra'? {list.Contains("Zebra")} (Expected: False)");

        // 6. Testing RemoveAt() and Remove()
        Console.WriteLine("\n--- 5. Testing RemoveAt() & Remove() ---");
        Console.WriteLine("Removing element at index 1...");
        list.RemoveAt(1);

        Console.Write("List after RemoveAt(1): ");
        list.Print();
        Console.WriteLine();

        Console.WriteLine("Removing item 'Charlie' directly...");
        bool removed = list.Remove("Charlie");
        Console.WriteLine($"'Charlie' removed? {removed}");

        Console.Write("List after Remove('Charlie'): ");
        list.Print();
        Console.WriteLine();

        // 7. Testing Clear()
        Console.WriteLine("\n--- 6. Testing Clear() ---");
        list.Clear();
        Console.WriteLine($"List cleared! IsEmpty: {list.IsEmpty} | Count: {list.Count}");

        // 8. Testing Exception Handling
        Console.WriteLine("\n--- 7. Testing Exceptions ---");
        try
        {
            Console.WriteLine("Attempting to access out-of-bounds index...");
            var item = list[10];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"[SUCCESS] Caught Exception: {ex.ParamName}");
        }

        try
        {
            Console.WriteLine("Attempting to Remove from an empty list...");
            list.Remove("Alpha");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"[SUCCESS] Caught Exception on Remove: {ex.Message}");
        }

        Console.WriteLine("       ALL TESTS COMPLETED!             ");
        Console.WriteLine("========================================");
    }
}