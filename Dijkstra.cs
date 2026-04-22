namespace B405_Project;

class Dijkstra {
    public static void RunTests(int runs, int n, double density) {
        Console.WriteLine("\n-----------------");
        Console.WriteLine("Dijkstra Heap Benchmark");
        Console.WriteLine($"Nodes: {n}, Density: {density}");

        var rand = new Random(42);

        for (int run = 1; run <= runs; run++) {
            var graph = GenerateGraph(n, density, rand);

            long bin = RunBinary(graph, n);
            long fib = RunFib(graph, n);

            Console.WriteLine($"\nRun #{run}");
            Console.WriteLine($"Binary Heap: {bin} ms");
            Console.WriteLine($"Fib Heap:    {fib} ms");
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
                    int w = rand.Next(1, 20);
                    graph[u].Add((v, w));
                }
            }
        }

        return graph;
    }
    
    static long RunBinary(List<(int to, int weight)>[] graph, int n) {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var dist = new int[n];
        var visited = new bool[n];

        for (int i = 0; i < n; i++)
            dist[i] = int.MaxValue;

        var heap = new BinaryHeap2();

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
                    heap.Push(nd, v); // lazy insert
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
    
    static long RunFib(List<(int to, int weight)>[] graph, int n) {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var dist = new int[n];
        var nodes = new object[n];
        var visited = new bool[n];

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
}