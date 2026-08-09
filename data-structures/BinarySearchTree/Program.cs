using BinarySearchTree;

	BST<int> bst = new();
	bst.Add(105);
	bst.Add(67);
	bst.Add(223);
	bst.Add(197);
	bst.Add(54);
	bst.Add(47);
	bst.Add(90);
	bst.Add(546);
	bst.Add(571);
	bst.Add(320);
	bst.PrintPostOrder();
	Console.WriteLine($"Height: {bst.Height().ToString()}");

	if(bst.Contains(95)) Console.WriteLine("Item found");
	else Console.WriteLine("Not found");
