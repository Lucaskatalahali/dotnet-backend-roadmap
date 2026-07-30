
using Arrays;


Console.WriteLine("Hello, World!");

ArrayList<int> intList = new ArrayList<int>(4);

if(intList.IsEmpty()) 
Console.WriteLine("Empty");
    intList.Add(2);
    
    Console.WriteLine(intList.Contains(2));
    Console.WriteLine(intList.IndexOf(2));
    Console.WriteLine(intList.Capacity);
    Console.WriteLine(intList.GetAt(0));
    Console.WriteLine(intList.GetAt(1));
