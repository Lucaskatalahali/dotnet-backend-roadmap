using SinlgyLinkedList;
SinlgyLinkedList<int> list = new();

Console.WriteLine(list.Contains(3)); //false
Console.WriteLine(list.Count);
list.Add(3);
Console.WriteLine(list.GetFirst());//error
Console.WriteLine(list.GetLast());//error
Console.WriteLine(list.Count);
list.RemoveAt(0);//error
Console.WriteLine(list.Count);
list.Add(1);
list.Add(2);
list.Add(3);
list.Add(4);





