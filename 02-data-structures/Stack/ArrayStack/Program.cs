using ArrayStack;

Console.WriteLine("    TESTING MY ARRAY STACK (C#)         ");
Console.WriteLine("========================================\n");

// 1. Instantiating with a small initial capacity (2) to test Resize
var stack = new ArrayStack<string>(2);
Console.WriteLine($"Stack created! Is empty? {stack.IsEmpty} | Count: {stack.Count}");

// 2. Testing Push and Automatic Resize
Console.WriteLine("\n--- 1. Testing Push() & Resize ---");
Console.WriteLine("Pushing: 'Item 1'");
stack.Push("Item 1");

Console.WriteLine("Pushing: 'Item 2'");
stack.Push("Item 2");

// Here the initial capacity (2) will be exceeded and Resize() will be triggered
Console.WriteLine("Pushing: 'Item 3' (should trigger Resize!)");
stack.Push("Item 3");

Console.WriteLine($"Current Count: {stack.Count} (Expected: 3)");
Console.WriteLine($"Is empty? {stack.IsEmpty} (Expected: False)");

// 3. Testing Peek
Console.WriteLine("\n--- 2. Testing Peek() ---");
Console.WriteLine($"Top element (Peek): '{stack.Peek()}' (Expected: Item 3)");
Console.WriteLine($"Count after Peek: {stack.Count} (Should not change, expected: 3)");

// 4. Testing Pop
Console.WriteLine("\n--- 3. Testing Pop() ---");
Console.WriteLine($"Popped: '{stack.Pop()}'"); // Removes Item 3
Console.WriteLine($"New top after Pop: '{stack.Peek()}' (Expected: Item 2)");
Console.WriteLine($"Current Count: {stack.Count} (Expected: 2)");

// 5. Testing Clear
Console.WriteLine("\n--- 4. Testing Clear() ---");
stack.Clear();
Console.WriteLine($"Stack cleared! IsEmpty: {stack.IsEmpty} (Expected: True)");
Console.WriteLine($"Current Count: {stack.Count} (Expected: 0)");

// 6. Testing Exceptions
Console.WriteLine("\n--- 5. Testing Exceptions ---");

try
{
    Console.WriteLine("Attempting to Pop() from an empty stack...");
    stack.Pop();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"[SUCCESS] Exception caught on Pop: {ex.Message}");
}

try
{
    Console.WriteLine("Attempting to Peek() an empty stack...");
    stack.Peek();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"[SUCCESS] Exception caught on Peek: {ex.Message}");
}

try
{
    Console.WriteLine("Attempting to instantiate stack with size 0...");
    var invalidStack = new ArrayStack<int>(0);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[SUCCESS] Exception caught in Constructor: {ex.ParamName}");
}

Console.WriteLine("\n========================================");
Console.WriteLine("       ALL TESTS COMPLETED!             ");
