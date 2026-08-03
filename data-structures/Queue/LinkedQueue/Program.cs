using LinkedQueue;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== TESTING LINKED QUEUE ===");

        var queue = new LinkedQueue<string>();

        // 1. Testing Enqueue & Print
        Console.Write("Enqueueing items: ");
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        queue.Print(); // Output: First, Second, Third

        Console.WriteLine($"\nCount: {queue.Count} | Peek front: '{queue.Peek()}'");

        // 2. Testing Dequeue (FIFO)
        Console.WriteLine($"\nDequeued: '{queue.Dequeue()}' (Expected: First)");
        Console.Write("Queue content after Dequeue: ");
        queue.Print(); // Output: Second, Third

        // 3. Testing Clear
        queue.Clear();
        Console.WriteLine($"\n\nCleared Queue! Is Empty? {queue.IsEmpty} | Count: {queue.Count}");

        // 4. Testing Exception
        try
        {
            queue.Dequeue();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"[SUCCESS] Exception caught: {ex.Message}");
        }
    }
}