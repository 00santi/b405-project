using System.Reflection.Metadata;

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
            Console.WriteLine("\nTest #" + run);
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

class Interleaved {
    public static void RunTests(int NO_TEST_RUNS, int HEAP_SIZE, int PUSH_PER_POP) {
        if (NO_TEST_RUNS == 0)
            return;
        
        var rand = new Random();
        
        Console.WriteLine("\n-----------------\nInterleaved Tests");
        Console.WriteLine("N = " + HEAP_SIZE);
        Console.WriteLine(PUSH_PER_POP + " Pushes per pop");
        for (int run = 1; run <= NO_TEST_RUNS; run++) {
            var arr = new int[HEAP_SIZE];
            
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next();
            
            long bh1 = BinTest(arr, PUSH_PER_POP);
            long fh1 = FibTest(arr, PUSH_PER_POP);
            Console.WriteLine("\nTest #" + run);
            Console.WriteLine("Binary Heap: " + bh1 + "ms");
            Console.WriteLine("Fibonacci Heap: " + fh1 + "ms");
        }
    }
    
    static long BinTest(int[] arr, int PUSH_PER_POP) {
        var sw = new System.Diagnostics.Stopwatch();
        var heap = new BinaryHeap();
        int idx = 0;
        int popCount = 0;
        
        sw.Start();
        while (idx < arr.Length || popCount < idx) {
            for (int i = 0; i < PUSH_PER_POP && idx < arr.Length; i++)
                heap.Push(arr[idx++]);
            
            if (popCount >= idx) 
                continue; 
            heap.Pop();
            popCount++;
            
        }
        
        while (popCount < arr.Length) {
            heap.Pop();
            popCount++;
        }
        sw.Stop();
        
        return sw.ElapsedMilliseconds;
    }
    
    static long FibTest(int[] arr, int PUSH_PER_POP) {
        var sw = new System.Diagnostics.Stopwatch();
        var heap = new FibHeap();
        int pushIndex = 0;
        int popCount = 0;
        
        sw.Start();
        while (pushIndex < arr.Length || popCount < pushIndex) {
            for (int i = 0; i < PUSH_PER_POP && pushIndex < arr.Length; i++)
                heap.Push(arr[pushIndex++]);
            
            if (popCount >= pushIndex) 
                continue; 
            heap.Pop();
            popCount++;
            
        }
        
        while (popCount < arr.Length) {
            heap.Pop();
            popCount++;
        }
        sw.Stop();
        
        return sw.ElapsedMilliseconds;
    }
}

class Program {
    static void Main(string[] _) { 
        Bulk.RunTests(3, 1_000_000);
        Bulk.RunTests(2, 10_000_000);
        
        Interleaved.RunTests(3, 1_000_000, 2);
        Interleaved.RunTests(3, 1_000_000, 3);
        Interleaved.RunTests(3, 1_000_000, 4);
        Interleaved.RunTests(3, 1_000_000, 10);
        
        Interleaved.RunTests(2, 10_000_000, 2);
    }
}

