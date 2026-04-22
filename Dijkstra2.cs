namespace B405_Project;

public class Dijkstra2 {
    
    public static void Run(int runs, int n, double density) {
        Console.WriteLine("\n-----------------");
        Console.WriteLine("Dijkstra Benchmark");
        Console.WriteLine($"Nodes: {n}, Density: {density}");

        var rand = new Random(42);

        for (int run = 1; run <= runs; run++) {
            var graph = GenerateGraph(n, density, rand);

            Console.WriteLine($"\nRun #{run}");

            long binLazy = RunBinaryLazy(graph, n);
            long fibLazy = RunFibLazy(graph, n);

            Console.WriteLine("== Dijkstra1 (lazy inserts) ==");
            Console.WriteLine($"Binary Heap: {binLazy} ms");
            Console.WriteLine($"Fib Heap:    {fibLazy} ms");

            long binDK = RunBinaryDecreaseKey(graph, n);
            long fibDK = RunFibDecreaseKey(graph, n);

            Console.WriteLine("\n== Dijkstra2 (decrease-key) ==");
            Console.WriteLine($"Binary Heap: {binDK} ms");
            Console.WriteLine($"Fib Heap:    {fibDK} ms");
        }
    }
    
    static List<(int to, int weight)>[] GenerateGraph(int n, double density, Random rand) {
        var graph = new List<(int to, int weight)>[n];

        for (int i = 0; i < n; i++)
            graph[i] = new List<(int to, int weight)>();

        for (int u = 0; u < n; u++) {
            for (int v = 0; v < n; v++) {
                if (u == v) continue;

                if (rand.NextDouble() < density) {
                    graph[u].Add((v, rand.Next(1, 20)));
                }
            }
        }

        return graph;
    }
    
    
    static long RunBinaryLazy(List<(int to, int weight)>[] graph, int n) {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var dist = new int[n];
        var visited = new bool[n];

        for (int i = 0; i < n; i++)
            dist[i] = int.MaxValue;

        var heap = new BinaryHeap2(); // your original lazy heap

        dist[0] = 0;
        heap.Push(0, 0);

        while (!heap.IsEmpty()) {
            var (d, u) = heap.Pop();

            if (visited[u]) continue;
            visited[u] = true;

            foreach (var (v, w) in graph[u]) {
                int nd = d + w;

                if (nd < dist[v]) {
                    dist[v] = nd;
                    heap.Push(nd, v); // repeated inserts
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
    
    static long RunFibLazy(List<(int to, int weight)>[] graph, int n) {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var dist = new int[n];
        var visited = new bool[n];

        for (int i = 0; i < n; i++)
            dist[i] = int.MaxValue;

        var heap = new FibHeap2();

        dist[0] = 0;
        heap.Push(0, 0);

        while (!heap.IsEmpty()) {
            var node = heap.Pop();
            var d = node.dist;
            var u = node.node;

            if (visited[u]) continue;
            visited[u] = true;

            foreach (var (v, w) in graph[u]) {
                int nd = d + w;

                if (nd < dist[v]) {
                    dist[v] = nd;
                    heap.Push(nd, v); // ALSO repeated inserts
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
    
    static long RunFibDecreaseKey(List<(int to, int weight)>[] graph, int n) {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var dist = new int[n];
        var visited = new bool[n];

        var nodes = new object[n];

        for (int i = 0; i < n; i++)
            dist[i] = int.MaxValue;

        var heap = new FibHeap2();

        dist[0] = 0;
        nodes[0] = heap.Push(0, 0);

        while (!heap.IsEmpty()) {
            var node = heap.Pop();
            var d = node.dist;
            var u = node.node;

            if (visited[u]) continue;
            visited[u] = true;

            foreach (var (v, w) in graph[u]) {
                int nd = d + w;

                if (nd < dist[v]) {
                    dist[v] = nd;

                    if (nodes[v] == null)
                        nodes[v] = heap.Push(nd, v);
                    else
                        heap.DecreaseKey((Node2)nodes[v], nd);
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
    
    static long RunBinaryDecreaseKey(List<(int to, int weight)>[] graph, int n) {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var dist = new int[n];
        var visited = new bool[n];

        for (int i = 0; i < n; i++)
            dist[i] = int.MaxValue;

        var heap = new BinaryHeap3(); // your decrease-key heap

        dist[0] = 0;
        heap.Push(0, 0);

        while (!heap.IsEmpty()) {
            var (d, u) = heap.Pop();

            if (visited[u]) continue;
            visited[u] = true;

            foreach (var (v, w) in graph[u]) {
                int nd = d + w;

                if (nd < dist[v]) {
                    dist[v] = nd;
                    heap.DecreaseKey(v, nd);
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}