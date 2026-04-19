using System.Reflection.Metadata;

namespace B405_Project;



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

