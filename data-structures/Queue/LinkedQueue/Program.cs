using LinkedQueue;

LinkedQueue<int> queue = new();

Console.WriteLine(queue.Count);
Console.WriteLine(queue.IsEmpty);
queue.Print();
queue.Enqueue(4);
queue.Print();
Console.WriteLine(queue.Peek());
Console.WriteLine(queue.Dequeue());

