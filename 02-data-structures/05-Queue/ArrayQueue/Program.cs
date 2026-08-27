using ArrayQueue;

class Program
{
    static void Main()
    {
        Console.WriteLine("    TESTING ARRAY QUEUE (CIRCULAR)      ");
        Console.WriteLine("========================================\n");

        // 1. Initializing with capacity 3 to force circular wrap and resize
        var queue = new ArrayQueue<string>(3);

        Console.WriteLine("--- 1. Testing Enqueue() ---");
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");

        Console.WriteLine($"Queue size: {queue.Count} | Capacity: {queue.Capacity}");
        Console.WriteLine($"Peek front item: '{queue.Peek()}' (Expected: First)");

        Console.WriteLine("\n--- 2. Testing Dequeue() & Circular Wrapping ---");
        Console.WriteLine($"Dequeued: '{queue.Dequeue()}'"); // Removes "First", _head advances
        Console.WriteLine($"Dequeued: '{queue.Dequeue()}'"); // Removes "Second", _head advances

        // Tail will wrap around to index 0 because _head moved forward!
        Console.WriteLine("Enqueueing 'Fourth' and 'Fifth' (wraps around)...");
        queue.Enqueue("Fourth");
        queue.Enqueue("Fifth");

        Console.WriteLine($"New front item (Peek): '{queue.Peek()}' (Expected: Third)");

        Console.WriteLine("\n--- 3. Triggering Resize() ---");
        queue.Enqueue("Sixth"); // Forces array to double its capacity
        Console.WriteLine($"Queue Count: {queue.Count} | New Capacity: {queue.Capacity} (Expected: 6)");

        Console.WriteLine("\n--- 4. Emptying Queue ---");
        while (!queue.IsEmpty)
        {
            Console.WriteLine($"Dequeued: '{queue.Dequeue()}'");
        }

        Console.WriteLine($"\nIs queue empty? {queue.IsEmpty} (Expected: True)");
    }
}