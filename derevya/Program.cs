using System;
using System.Diagnostics;

public class AVLTree
{
    private class Node
    {
        public int data; 
        public int height; 
        public Node left; 
        public Node right; 

        public Node(int item)
        {
            data = item;
            height = 1;
            left = right = null;
        }
    }

    private Node root; 

    public AVLTree()
    {
        root = null;
    }

    public bool Find(int key)
    {
        return FindRec(root, key);
    }

    private bool FindRec(Node node, int key)
    {
        if (node == null)
        {
            return false; 
        }
        if (key == node.data)
        {
            return true; 
        }
        else if (key < node.data)
        {
            return FindRec(node.left, key); 
        }
        else
        {
            return FindRec(node.right, key);
        }
    }

    public void Insert(int item)
    {
        root = InsertRec(root, item);
    }

    private Node InsertRec(Node node, int item)
    {
        if (node == null)
        {
            return new Node(item);
        }

        if (item < node.data)
        {
            node.left = InsertRec(node.left, item);
        }
        else if (item > node.data)
        {
            node.right = InsertRec(node.right, item);
        }
        else
        {
            return node; 
        }

        node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1 && item < node.left.data)
        {
            return RightRotate(node);
        }

        if (balanceFactor < -1 && item > node.right.data)
        {
            return LeftRotate(node);
        }
        if (balanceFactor > 1 && item > node.left.data)
        {
            node.left = LeftRotate(node.left);
            return RightRotate(node);
        }

        if (balanceFactor < -1 && item < node.right.data)
        {
            node.right = RightRotate(node.right);
            return LeftRotate(node);
        }

        return node;
    }

    public void Remove(int item)
    {
        root = RemoveRec(root, item);
    }

    private Node RemoveRec(Node node, int item)
    {
        if (node == null)
        {
            return node;
        }

        if (item < node.data)
        {
            node.left = RemoveRec(node.left, item);
        }
        else if (item > node.data)
        {
            node.right = RemoveRec(node.right, item);
        }
        else
        {
            if (node.left == null || node.right == null)
            {
                Node temp = node.left ?? node.right;
                if (temp == null)
                {
                    temp = node;
                    node = null;
                }
                else
                {
                    node = temp;
                }

                temp = null;
            }
            else
            {
                Node temp = FindMin(node.right);
                node.data = temp.data;
                node.right = RemoveRec(node.right, temp.data);
            }
        }

        if (node == null)
        {
            return node;
        }


        node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));


        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1 && GetBalanceFactor(node.left) >= 0)
        {
            return RightRotate(node);
        }

        if (balanceFactor > 1 && GetBalanceFactor(node.left) < 0)
        {
            node.left = LeftRotate(node.left);
            return RightRotate(node);
        }

        if (balanceFactor < -1 && GetBalanceFactor(node.right) <= 0)
        {
            return LeftRotate(node);
        }

        if (balanceFactor < -1 && GetBalanceFactor(node.right) > 0)
        {
            node.right = RightRotate(node.right);
            return LeftRotate(node);
        }

        return node;
    }

    public void InfixTraverse()
    {
        InfixTraverseRec(root);
        Console.WriteLine();
    }

    private void InfixTraverseRec(Node node)
    {
        if (node != null)
        {
            InfixTraverseRec(node.left);
            Console.Write(node.data + " ");
            InfixTraverseRec(node.right);
        }
    }

    public void PrefixTraverse()
    {
        PrefixTraverseRec(root);
        Console.WriteLine();
    }

    private void PrefixTraverseRec(Node node)
    {
        if (node != null)
        {
            Console.Write(node.data + " ");
            PrefixTraverseRec(node.left);
            PrefixTraverseRec(node.right);
        }
    }

    public void PostfixTraverse()
    {
        PostfixTraverseRec(root);
        Console.WriteLine();
    }

    private void PostfixTraverseRec(Node node)
    {
        if (node != null)
        {
            PostfixTraverseRec(node.left);
            PostfixTraverseRec(node.right);
            Console.Write(node.data + " ");
        }
    }

    private int GetHeight(Node node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.height;
    }

    private int GetBalanceFactor(Node node)
    {
        if (node == null)
        {
            return 0;
        }
        return GetHeight(node.left) - GetHeight(node.right);
    }

    private Node LeftRotate(Node node)
    {
        Node rightChild = node.right;
        Node leftGrandchild = rightChild.left;

        rightChild.left = node;
        node.right = leftGrandchild;

        node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
        rightChild.height = 1 + Math.Max(GetHeight(rightChild.left), GetHeight(rightChild.right));

        return rightChild;
    }

    private Node RightRotate(Node node)
    {
        Node leftChild = node.left;
        Node rightGrandchild = leftChild.right;

        leftChild.right = node;
        node.left = rightGrandchild;

        node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
        leftChild.height = 1 + Math.Max(GetHeight(leftChild.left), GetHeight(leftChild.right));

        return leftChild;
    }

    private Node FindMin(Node node)
    {
        Node current = node;
        while (current.left != null)
        {
            current = current.left;
        }
        return current;
    }
}

public class BinarySearchTree
{
    private class Node
    {
        public int data; 
        public Node left; 
        public Node right; 

        public Node(int item)
        {
            data = item;
            left = right = null;
        }
    }

    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    public bool Find(int key)
    {
        Node current = root;
        while (current != null)
        {
            if (key == current.data)
            {
                return true; 
            }
            else if (key < current.data)
            {
                current = current.left;
            }
            else
            {
                current = current.right; 
            }
        }
        return false; 
    }


    public void Insert(int item)
    {
        root = InsertRec(root, item);
    }


    private Node InsertRec(Node root, int item)
    {
        if (root == null)
        {
            root = new Node(item);
            return root;
        }
        if (item < root.data)
        {
            root.left = InsertRec(root.left, item);
        }
        else if (item > root.data)
        {
            root.right = InsertRec(root.right, item);
        }
        return root;
    }

    public void Remove(int key)
    {
        root = RemoveRec(root, key);
    }

    private Node RemoveRec(Node root, int key)
    {
        if (root == null)
        {
            return root;
        }
        if (key < root.data)
        {
            root.left = RemoveRec(root.left, key);
        }
        else if (key > root.data)
        {
            root.right = RemoveRec(root.right, key);
        }
        else
        {
            // Узел со значением для удаления найден

            // Узел с одним или без потомков
            if (root.left == null)
            {
                return root.right;
            }
            else if (root.right == null)
            {
                return root.left;
            }

            root.data = MinValue(root.right);

            root.right = RemoveRec(root.right, root.data);
        }
        return root;
    }

    private int MinValue(Node root)
    {
        int minVal = root.data;
        while (root.left != null)
        {
            minVal = root.left.data;
            root = root.left;
        }
        return minVal;
    }

    public void InfixTraverse()
    {
        InfixTraverseRec(root);
        Console.WriteLine();
    }

    private void InfixTraverseRec(Node root)
    {
        if (root != null)
        {
            InfixTraverseRec(root.left);
            Console.Write(root.data + " ");
            InfixTraverseRec(root.right);
        }
    }

    public void PrefixTraverse()
    {
        PrefixTraverseRec(root);
        Console.WriteLine();
    }
    private void PrefixTraverseRec(Node root)
    {
        if (root != null)
        {
            Console.Write(root.data + " ");
            PrefixTraverseRec(root.left);
            PrefixTraverseRec(root.right);
        }
    }

    public void PostfixTraverse()
    {
        PostfixTraverseRec(root);
        Console.WriteLine();
    }

    private void PostfixTraverseRec(Node root)
    {
        if (root != null)
        {
            PostfixTraverseRec(root.left);
            PostfixTraverseRec(root.right);
            Console.Write(root.data + " ");
        }
    }
}

public class QuickSort
{
    private int[] arr;

    public QuickSort()
    {
        arr = new int[0];
    }

    public void Insert(int item)
    {
        Array.Resize(ref arr, arr.Length + 1);
        arr[arr.Length - 1] = item;
    }

    public bool Find(int key)
    {
        return Array.IndexOf(arr, key) != -1;
    }

    public void Remove(int item)
    {
        int index = Array.IndexOf(arr, item);

        if (index != -1)
        {
            for (int i = index; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            Array.Resize(ref arr, arr.Length - 1);
        }
    }

    public void Print()
    {
        foreach (int item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public void QuickSortAlgorithm()
    {
        QuickSortRecursion(0, arr.Length - 1);
    }

    private void QuickSortRecursion(int low, int high)
    {
        if (low < high)
        {
            int partitionIndex = Partition(low, high);
            QuickSortRecursion(low, partitionIndex - 1);
            QuickSortRecursion(partitionIndex + 1, high);
        }
    }

    private int Partition(int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        int swapTemp = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = swapTemp;
        return i + 1;
    }
}

public class CustomArray
{
    private int[] array;
    private int size;

    public CustomArray(int capacity)
    {
        array = new int[capacity];
        size = 0;
    }

    public void Insert(int element)
    {
        if (size < array.Length)
        {
            array[size] = element;
            size++;
        }
        else
        {
            Console.WriteLine("Массив заполнен. Невозможно добавить больше элементов.");
        }
    }

    public void Remove(int element)
    {
        int index = Array.IndexOf(array, element);
        if (index != -1)
        {
            for (int i = index; i < size - 1; i++)
            {
                array[i] = array[i + 1];
            }
            size--;
        }
        else
        {
            Console.WriteLine("Элемент для удаления не найден в массиве.");
        }
    }

    public int FindbyNum(int element)
    {
        int index = Array.IndexOf(array, element);
        if (index != -1)
        {
            return index;
        }
        else
        {
            Console.WriteLine("Элемент не найден в массиве.");
            return -1;
        }
    }

    public void FindbyInd(int index)
    {
        if (index >= 0 && index < size)
        {
            Console.WriteLine("Элемент по индексу " + index + ": " + array[index]);
        }
        else
        {
            Console.WriteLine("В массиве нет элемента с таким индексом");
        }
    }

    public void Print()
    {
        foreach (int item in array)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();

        Console.WriteLine("AVL дерево");
        AVLTree avlTree = new AVLTree();

        sw.Start();
        for (int i = 0; i < 200; i++)
        {
            int value = new Random().Next(1, 1000); 
            avlTree.Insert(value);
        }
        sw.Stop();
        Console.WriteLine($"AVLTree.Insert 200 значений: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        avlTree.Find(90);
        sw.Stop();
        Console.WriteLine($"AWLTree.Find: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        avlTree.Remove(30);
        sw.Stop();
        Console.WriteLine($"AWLTree.Remove: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("Инфиксный обход:");
        sw.Start();
        avlTree.InfixTraverse();
        sw.Stop();
        Console.WriteLine($"AWLTree.InfixTraverse: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("Префиксный обход:");
        sw.Start();
        avlTree.PrefixTraverse();
        sw.Stop();
        Console.WriteLine($"AWLTree.PrefixTraverse: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("Постфиксный обход:");
        sw.Start();
        avlTree.PostfixTraverse();
        sw.Stop();
        Console.WriteLine($"AWLTree.PostfixTraverse: {sw.Elapsed}");
        sw.Reset();



        Console.WriteLine("\nБинарное дерево поиска");
        BinarySearchTree binarySearchTree = new BinarySearchTree();

        sw.Start();
        for (int i = 0; i < 200; i++)
        {
            int value = new Random().Next(1, 1000);
            binarySearchTree.Insert(value);
        }
        sw.Stop();
        Console.WriteLine($"BinarySearchTree.Insert для 200 значений: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        binarySearchTree.Find(90);
        sw.Stop();
        Console.WriteLine($"BinarySearchTree.Find: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        binarySearchTree.Remove(30);
        sw.Stop();
        Console.WriteLine($"binarySearchTree.Remove: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("Инфиксный обход (в порядке возрастания):");
        sw.Start();
        binarySearchTree.InfixTraverse();
        sw.Stop();
        Console.WriteLine($"binarySearchTree.InfixTraverse: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("Префиксный обход:");
        sw.Start();
        avlTree.PrefixTraverse();
        sw.Stop();
        Console.WriteLine($"binarySearchTree.PrefixTraverse: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("Постфиксный обход:");
        sw.Start();
        binarySearchTree.PostfixTraverse();
        sw.Stop();
        Console.WriteLine($"binarySearchTree.PostfixTraverse: {sw.Elapsed}");
        sw.Reset();



        Console.WriteLine("\nОтсортированный QuickSort массив");
        QuickSort quickSort = new QuickSort();

        sw.Start();
        for (int i = 0; i < 200; i++)
        {
            int value = new Random().Next(1, 1000);
            quickSort.Insert(value);
        }
        sw.Stop();
        Console.WriteLine($"quickSort.Insert 200 значений: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        quickSort.Find(90);
        sw.Stop();
        Console.WriteLine($"quickSort.Find: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        quickSort.Remove(30);
        sw.Stop();
        Console.WriteLine($"quickSort.Remove: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("Отсортированный QuickSort массив");
        sw.Start();
        quickSort.Print();
        sw.Stop();
        Console.WriteLine($"quickSort.Print: {sw.Elapsed}");
        sw.Reset();



        Console.WriteLine("\nНе отсортированный CustomArray массив");
        CustomArray сustomArray = new CustomArray(210);

        sw.Start();
        for (int i = 0; i < 200; i++)
        {
            int value = new Random().Next(1, 1000); //
            сustomArray.Insert(value);
        }
        sw.Stop();
        Console.WriteLine($"сustomArray.Insert 200 значений: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        сustomArray.Remove(30);
        sw.Stop();
        Console.WriteLine($"CustomArray.Remove: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        сustomArray.FindbyNum(90);
        sw.Stop();
        Console.WriteLine($"CustomArray.FindbyNum: {sw.Elapsed}");
        sw.Reset();

        sw.Start();
        сustomArray.FindbyInd(1);
        sw.Stop();
        Console.WriteLine($"CustomArray.FindbyInd: {sw.Elapsed}");
        sw.Reset();

        Console.WriteLine("CustomArray массив:");
        sw.Start();
        сustomArray.Print();
        sw.Stop();
        Console.WriteLine($"CustomArray.Print: {sw.Elapsed}");
        sw.Reset();
    }
}