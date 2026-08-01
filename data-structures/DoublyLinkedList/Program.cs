using DoublyLinkedList;

DoublyLinkedList<int> list = new();

list.Add(1);
Console.WriteLine(list.Contains(1));
list.Remove(1);
Console.WriteLine(list.Contains(1));
Console.WriteLine(list.Count);
list.Add(3);
list.Add(-4);
list.Add(5);
Console.WriteLine(list);
list.RemoveAt(0);
Console.WriteLine(list);
Console.WriteLine(list.Count);
Console.WriteLine(list.GetFirst());
Console.WriteLine(list.GetLast());
list.RemoveAt(0);
Console.WriteLine(list.GetFirst());
Console.WriteLine(list.GetLast());