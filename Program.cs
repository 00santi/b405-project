namespace B405_Project;

class Program {
    static void Main(string[] _) { 
        //Bulk.RunTests(1, 1_000_000);
        //Bulk.RunTests(1, 10_000_000);
        
        Interleaved.RunTests(1, 10_000_000, 2);
        Interleaved.RunTests(1, 10_000_000, 3);
        Interleaved.RunTests(1, 10_000_000, 4);
        Interleaved.RunTests(1, 10_000_000, 10);
    

        
        

        /*Dijkstra2.Run(1, 50_000, 0.001);
        Dijkstra2.Run(1, 50_000, 0.01);
        Dijkstra2.Run(1, 50_000, 0.1);
        Dijkstra2.Run(1, 50_000, 0.3);
        Dijkstra2.Run(1, 50_000, 0.5);*/
        Dijkstra2.Run(1, 50_000, 0.8);
        Dijkstra2.Run(1, 50_000, 1);
    }
}

