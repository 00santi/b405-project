namespace B405_Project;

public class BinaryHeap2 {
    List<(int dist, int node)> data;

    public BinaryHeap2() {
        data = new List<(int dist, int node)>();
    }

    public void Push(int dist, int node) {
        data.Add((dist, node));
        BubbleUp(data.Count - 1);
    }

    void BubbleUp(int i) {
        while (i > 0) {
            int parent = (i - 1) / 2;

            if (data[i].dist >= data[parent].dist)
                break;

            (data[i], data[parent]) = (data[parent], data[i]);
            i = parent;
        }
    }

    public (int dist, int node) Peek() {
        if (data.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        return data[0];
    }

    public (int dist, int node) Pop() {
        if (data.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        var result = data[0];
        data[0] = data[data.Count - 1];
        data.RemoveAt(data.Count - 1);

        if (data.Count > 0)
            BubbleDown(0);

        return result;
    }

    void BubbleDown(int i) {
        for (;;) {
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left >= data.Count)
                return;

            int child = left;

            if (right < data.Count && data[right].dist < data[left].dist)
                child = right;

            if (data[i].dist <= data[child].dist)
                return;

            (data[i], data[child]) = (data[child], data[i]);
            i = child;
        }
    }

    public bool IsEmpty() {
        return data.Count == 0;
    }
}