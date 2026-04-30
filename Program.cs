namespace B405_Project;

class Program {
    static void Main(string[] _) { 
        Bulk.RunTests(1, 1_000_000);
        
        Interleaved.RunTests(1, 1_000_000, 10);
        
        Dijkstra2.Run(1, 50_000, 0.1);
        
        DecreaseKey.RunTests();
    }
}

