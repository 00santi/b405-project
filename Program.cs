namespace B405_Project;

class Program {
    static void Main(string[] args) {
        var rand = new Random();
        int no_runs = 3;
        
        Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~\nN = 1,000,000");
        for (int run = 1; run <= no_runs; run++) {
            var arr = new int[1_000_000];
            
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next();
            
            long bh1 = TestBinaryHeap(arr);
            long fh1 = TestFibHeap(arr);
            Console.WriteLine("Test #" + run);
            Console.WriteLine("Binary Heap: " + bh1 + "ms");
            Console.WriteLine("Fibonacci Heap: " + fh1 + "ms");
        }
        
        
        Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~\nN = 10,000,000");
        for (int run = 1; run <= no_runs; run++) {
            var arr = new int[10_000_000];
            
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next();
            
            long bh1 = TestBinaryHeap(arr);
            long fh1 = TestFibHeap(arr);
            Console.WriteLine("Test #" + run);
            Console.WriteLine("Binary Heap: " + bh1 + "ms");
            Console.WriteLine("Fibonacci Heap: " + fh1 + "ms\n");
        }
    }

    static long TestBinaryHeap(int[] arr) {
        var sw = new System.Diagnostics.Stopwatch();
        var heap = new BinaryHeap();
        
        sw.Start();
        foreach (int n in arr) 
            heap.Push(n);
        
        for (int i = 0; i < arr.Length; i++)
            heap.Pop();
        sw.Stop();
        
        return sw.ElapsedMilliseconds;
    }

    static long TestFibHeap(int[] arr) {
        var sw = new System.Diagnostics.Stopwatch();
        var heap = new FibHeap();
        
        sw.Start();
        foreach (int n in arr) 
            heap.Push(n);
        
        for (int i = 0; i < arr.Length; i++)
            heap.Pop();
        sw.Stop();
        
        return sw.ElapsedMilliseconds;
    }
}

