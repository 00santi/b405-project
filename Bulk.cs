namespace B405_Project;

class Bulk {
    public static void RunTests(int NO_TEST_RUNS, int HEAP_SIZE) {
        if (NO_TEST_RUNS == 0) 
            return;
        
        var rand = new Random();
        
        Console.WriteLine("\n-----------------\nBulk Tests");
        Console.WriteLine("N = " + HEAP_SIZE);
        for (int run = 1; run <= NO_TEST_RUNS; run++) {
            var arr = new int[HEAP_SIZE];
            
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next();
            
            long bh1 = BinTest(arr);
            long fh1 = FibTest(arr);
            if (NO_TEST_RUNS > 1) Console.WriteLine("\nTest #" + run);
            Console.WriteLine("Binary Heap: " + bh1 + "ms");
            Console.WriteLine("Fibonacci Heap: " + fh1 + "ms");
        }
    }
    
    static long BinTest(int[] arr) {
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
    
    static long FibTest(int[] arr) {
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