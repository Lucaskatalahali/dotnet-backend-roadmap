using ArrayStack;
ArrayStack<int> stack = new(5);


Console.WriteLine(stack.Count);
Console.WriteLine(stack.IsEmpty());
stack.Push(6);
stack.Push(7);
Console.WriteLine(stack.Count);
Console.WriteLine(stack.Pop());
stack.Clear();
Console.WriteLine(stack.Peek());
stack.Pop();
Console.WriteLine(stack.Peek());
stack.Pop();
Console.WriteLine(stack.Count);