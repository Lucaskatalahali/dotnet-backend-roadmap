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
Console.WriteLine(list.GetLast());
list.Add(4);
Console.Write(list.ToString());
Console.WriteLine();

list.Clear();
Console.Write(list.ToString());

list.Add(1); list.Add(2); // size = 2
//list.RemoveAt(2); // deveria lançar exceção — índice 2 não existe (só 0 e 1)
Console.WriteLine(list.Count); // imprime 1, mas nada foi de fato removido!



