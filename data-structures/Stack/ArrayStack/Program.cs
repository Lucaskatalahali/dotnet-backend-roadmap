using ArrayStack;
ArrayStack<int> stack = new(5);


Console.WriteLine(stack.Count);
Console.WriteLine(stack.IsEmpty());
stack.Push(6);
stack.Push(7);
Console.WriteLine(stack.Count);
stack.Clear();
Console.WriteLine(stack.Top());
stack.Pop();
Console.WriteLine(stack.Top());
stack.Pop();
Console.WriteLine(stack.Count);