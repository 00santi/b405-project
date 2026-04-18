namespace B405_Project;

class Program {
    static void Main(string[] args) {
        long bh1 = TestBinaryHeap(1_000_000);
        long fh1 = TestFibHeap(1_000_000);
        long bh2 = TestBinaryHeap(10_000_000);
        long fh2 = TestFibHeap(10_000_000);
        //long bh3 = TestBinaryHeap(100_000_000);
        //long fb3 = TestFibHeap(100_000_000);
        Console.WriteLine("N = 1,000,000");
        Console.WriteLine("Binary Heap: " + bh1 + "ms");
        Console.WriteLine("Fibonacci Heap: " + fh1 + "ms\n");
        Console.WriteLine("N = 10,000,000");
        Console.WriteLine("Binary Heap: " + bh2 + "ms");
        Console.WriteLine("Fibonacci Heap: " + fh2 + "ms");
    }

    static long TestBinaryHeap(int N) {
        var rand = new Random();
        var sw = new System.Diagnostics.Stopwatch();
        var heap = new BinaryHeap();
        
        sw.Start();
        for (int i = 0; i < N; i++) 
            heap.Push(rand.Next());
        
        for (int i = 0; i < N; i++)
            heap.Pop();
        sw.Stop();
        
        return sw.ElapsedMilliseconds;
    }

    static long TestFibHeap(int N) {
        var rand = new Random();
        var sw = new System.Diagnostics.Stopwatch();
        var heap = new FibHeap();
        
        sw.Start();
        for (int i = 0; i < N; i++)
            heap.Push(rand.Next());

        for (int i = 0; i < N; i++)
            heap.Pop();
        sw.Stop();
        
        return sw.ElapsedMilliseconds;
    }
}

