using Arrays;

ArrayList<int> intList = new ArrayList<int>(4);

intList.Add(5);
intList.Add(3);
intList.RemoveAt(0);
Console.WriteLine(intList.GetAt(0));
Console.WriteLine(intList.GetAt(1));