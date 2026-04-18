namespace B405_Project;

public class BinaryHeap { 
    List<int> data;

    public BinaryHeap() {
        data = new List<int>();
    }

    public void Push(int value) {
        data.Add(value);
        BubbleUp(data.Count - 1);
    }

    void BubbleUp(int i) {
        while (i > 0) {
            int parent = (i - 1) / 2;
            if (data[i] >= data[parent]) 
                break;
            (data[i], data[parent]) = (data[parent], data[i]);
            i = parent;
        }
    }
    
    public int Peek() {
        return data[0];
    }

    public int Pop() {
       int result = data[0];
       data[0] =  data[data.Count - 1];
       data.RemoveAt(data.Count - 1);
       BubbleDown(0);
       return result;
    }

    void BubbleDown(int i) {
        for (;;) {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left >= data.Count) 
                return;
            
            int child = right;
            if (right >= data.Count || data[left] < data[right])
                child = left;
            
            if (data[child] > data[i])
                return;
            (data[i], data[child]) = (data[child], data[i]);
            i = child;
        }
    }
}
