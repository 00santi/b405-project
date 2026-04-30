namespace B405_Project;

class DecreaseKey {
    public static void RunTests() {
        Run(3, 1_000_000, 1);
        Run(3, 1_000_000, 5);
        Run(3, 1_000_000, 20);
    }
    public static void Run(int runs, int n, int decreasesPerNode) {
        Console.WriteLine("\n-----------------");
        Console.WriteLine("Decrease-Key Benchmark");
        Console.WriteLine($"Nodes: {n}, Decrease-Key ops per node: {decreasesPerNode}");
        Console.WriteLine($"Total decrease-key ops: {n * decreasesPerNode}");

        long totalBinary = 0;
        long totalFib = 0;

        for (int run = 1; run <= runs; run++) {
            int seed = 1000 + run;

            int[] initial = BuildInitialKeys(n);
            BuildOperations(n, decreasesPerNode, seed, out int[] opNodes, out int[] opDeltas);

            long binTime = RunBinary(initial, opNodes, opDeltas, out int[] binOut);
            long fibTime = RunFib(initial, opNodes, opDeltas, out int[] fibOut);

            bool ok = IsNonDecreasing(binOut) &&
                      IsNonDecreasing(fibOut) &&
                      SameArray(binOut, fibOut);

            totalBinary += binTime;
            totalFib += fibTime;

            Console.WriteLine($"\nRun #{run}");
            Console.WriteLine($"Binary Heap:    {binTime} ms");
            Console.WriteLine($"Fibonacci Heap: {fibTime} ms");
            Console.WriteLine($"Correctness:    {(ok ? "OK" : "FAILED")}");
        }

        Console.WriteLine("\n== Average ==");
        Console.WriteLine($"Binary Heap:    {totalBinary / runs} ms");
        Console.WriteLine($"Fibonacci Heap: {totalFib / runs} ms");
    }

    static int[] BuildInitialKeys(int n) {
        var initial = new int[n];

        for (int i = 0; i < n; i++)
            initial[i] = 1_000_000_000 + i;

        return initial;
    }

    static void BuildOperations(int n, int decreasesPerNode, int seed,
        out int[] opNodes, out int[] opDeltas) {
        int totalOps = n * decreasesPerNode;
        opNodes = new int[totalOps];
        opDeltas = new int[totalOps];

        var rand = new Random(seed);

        for (int i = 0; i < totalOps; i++) {
            opNodes[i] = rand.Next(n);
            opDeltas[i] = rand.Next(1, 1000);
        }
    }

    static long RunBinary(int[] initial, int[] opNodes, int[] opDeltas, out int[] popped) {
        int n = initial.Length;
        var current = new int[n];
        Array.Copy(initial, current, n);

        var heap = new BinaryHeap3();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        for (int i = 0; i < n; i++)
            heap.Push(initial[i], i);

        for (int i = 0; i < opNodes.Length; i++) {
            int node = opNodes[i];
            current[node] -= opDeltas[i];
            heap.DecreaseKey(node, current[node]);
        }

        popped = new int[n];
        for (int i = 0; i < n; i++) {
            var x = heap.Pop();
            popped[i] = x.dist;
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    static long RunFib(int[] initial, int[] opNodes, int[] opDeltas, out int[] popped) {
        int n = initial.Length;
        var current = new int[n];
        Array.Copy(initial, current, n);

        var heap = new FibHeap2();
        var handles = new Node2[n];
        var sw = System.Diagnostics.Stopwatch.StartNew();

        for (int i = 0; i < n; i++)
            handles[i] = heap.Push(initial[i], i);

        for (int i = 0; i < opNodes.Length; i++) {
            int node = opNodes[i];
            current[node] -= opDeltas[i];
            heap.DecreaseKey(handles[node], current[node]);
        }

        popped = new int[n];
        for (int i = 0; i < n; i++) {
            var x = heap.Pop();
            popped[i] = x.dist;
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    static bool IsNonDecreasing(int[] arr) {
        for (int i = 1; i < arr.Length; i++) {
            if (arr[i] < arr[i - 1])
                return false;
        }

        return true;
    }

    static bool SameArray(int[] a, int[] b) {
        if (a.Length != b.Length)
            return false;

        for (int i = 0; i < a.Length; i++) {
            if (a[i] != b[i])
                return false;
        }

        return true;
    }
}