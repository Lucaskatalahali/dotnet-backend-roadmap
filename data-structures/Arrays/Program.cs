using Arrays;

ArrayList<int> array = new(2);

array.Add(10);
array.Add(20);
array.Add(30); // here it forces a resize (capacity was 2)
array.Print();  // [ 10, 20, 30 ]
Console.WriteLine();
Console.WriteLine($"Count: {array.Count}, Capacity: {array.Capacity}");

array.InsertAt(1, 15);
array.Print(); // [ 10, 15, 20, 30 ]
Console.WriteLine();

array.RemoveAt(0);
array.Print(); // [ 15, 20, 30 ]
Console.WriteLine();
Console.WriteLine($"GetAt(1): {array.GetAt(1)}");

Console.WriteLine($"IndexOf(30): {array.IndexOf(30)}");
Console.WriteLine($"Contains(99): {array.Contains(99)}");
Console.WriteLine($"Contains(20): {array.Contains(20)}");