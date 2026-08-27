using SinlgyLinkedList;

  Console.WriteLine("=== 1. Add / ToString ===");
            var list = new SinlgyLinkedList<int>();
            Console.WriteLine($"Empty list ToString(): '{list}'");
            Console.WriteLine($"IsEmpty: {list.IsEmpty()}, Count: {list.Count}");
 
            list.Add(10);
            list.Add(20);
            list.Add(30);
            Console.WriteLine($"After Add(10,20,30): {list}"); // [10, 20, 30]
            list.Reverse();
            Console.WriteLine($"After Reverse the list: {list}"); // [30, 20, 10]
            Console.WriteLine($"Count: {list.Count}");
            list.Reverse();
            Console.WriteLine($"After Reverse the list again: {list}"); // [10, 20, 30]

 
            Console.WriteLine("\n=== 2. InsertAt ===");
            list.InsertAt(0, 5); // insert at index 0
            Console.WriteLine($"InsertAt(0, 5): {list}"); // [5, 10, 20, 30]
 
            list.InsertAt(list.Count, 99); // insert at end
            Console.WriteLine($"InsertAt(Count, 99): {list}"); // [5, 10, 20, 30, 99]
 
            list.InsertAt(2, 15); // insert in the midle
            Console.WriteLine($"InsertAt(2, 15): {list}"); // [5, 10, 15, 20, 30, 99]
 
            Console.WriteLine("\n=== 3. GetFirst / GetLast ===");
            Console.WriteLine($"GetFirst(): {list.GetFirst()}"); // 5
            Console.WriteLine($"GetLast(): {list.GetLast()}");   // 99
 
            Console.WriteLine("\n=== 4. IndexOf / Contains ===");
            Console.WriteLine($"IndexOf(20): {list.IndexOf(20)}");   // 3
            Console.WriteLine($"IndexOf(1000): {list.IndexOf(1000)}"); // -1
            Console.WriteLine($"Contains(15): {list.Contains(15)}"); // True
            Console.WriteLine($"Contains(1000): {list.Contains(1000)}"); // False
 
            Console.WriteLine("\n=== 5. RemoveAt ===");
            list.RemoveAt(0); // remove head (first element)
            Console.WriteLine($"RemoveAt(0): {list}"); // [10, 15, 20, 30, 99]
 
            list.RemoveAt(list.Count - 1); // remove the last
            Console.WriteLine($"RemoveAt(Count-1): {list}"); // [10, 15, 20, 30]
 
            list.RemoveAt(1); // remove in the middle
            Console.WriteLine($"RemoveAt(1): {list}"); // [10, 20, 30]
 
            Console.WriteLine("\n=== 6. Remove(T item) ===");
            bool removed = list.Remove(20);
            Console.WriteLine($"Remove(20) -> {removed}, list: {list}"); // True, [10, 30]
 
            removed = list.Remove(1000);
            Console.WriteLine($"Remove(1000) -> {removed}, list: {list}"); // False, [10, 30]
 
            Console.WriteLine("\n=== 7. Edge cases (bounds) ===");
            TryCatch("RemoveAt(-1)", () => list.RemoveAt(-1));
            TryCatch("RemoveAt(Count) [out of limit]", () => list.RemoveAt(list.Count));
            TryCatch("InsertAt(-1, x)", () => list.InsertAt(-1, 123));
            TryCatch("InsertAt(Count + 1, x) [out of limit]", () => list.InsertAt(list.Count + 1, 123));
 
            Console.WriteLine("\n=== 8. List with one single element ===");
            var single = new SinlgyLinkedList<string>();
            single.Add("only");
            Console.WriteLine($"ToString: {single}");          // [only]
            Console.WriteLine($"GetFirst: {single.GetFirst()}"); // only
            Console.WriteLine($"GetLast: {single.GetLast()}");   // only
            single.RemoveAt(0);
            Console.WriteLine($"After RemoveAt(0): {single} (Count: {single.Count}, IsEmpty: {single.IsEmpty()})");
 
            Console.WriteLine("\n=== 9. Clear() e reuse ===");
            list.Clear();
            Console.WriteLine($"After Clear(): {list} (Count: {list.Count}, IsEmpty: {list.IsEmpty()})");
            list.Add(1);
            list.Add(2);
            Console.WriteLine($"After Add(1,2) post-Clear: {list} (Count: {list.Count})"); // [1, 2], Count: 2
 
            Console.WriteLine("\n=== 10. Empty  list ===");
            var empty = new SinlgyLinkedList<int>();
            TryCatch("GetFirst() in empty list", () => empty.GetFirst());
            TryCatch("GetLast() in empty list", () => empty.GetLast());
            TryCatch("RemoveAt(0) in empty list", () => empty.RemoveAt(0));
 
            Console.WriteLine("\n=== End of tests ===");
        
        // Helper to test if an operation throws exception so that the program don't crash
        static void TryCatch(string label, Action action)
        {
            try
            {
                action();
                Console.WriteLine($"{label}: No exceptiono (verifiy if it was expected!)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{label}: Exception {ex.GetType().Name} -> \"{ex.Message}\"");
            }}

