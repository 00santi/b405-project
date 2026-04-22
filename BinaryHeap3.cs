namespace B405_Project;

public class BinaryHeap3 {
    private List<(int dist, int node)> data;
    private Dictionary<int, int> pos;

    public BinaryHeap3() {
        data = new List<(int dist, int node)>();
        pos = new Dictionary<int, int>();
    }

    public bool IsEmpty() => data.Count == 0;

    public (int dist, int node) Peek() {
        if (data.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        return data[0];
    }

    public void Push(int dist, int node) {
        if (pos.ContainsKey(node)) {
            DecreaseKey(node, dist);
            return;
        }

        data.Add((dist, node));
        int i = data.Count - 1;

        pos[node] = i;
        BubbleUp(i);
    }

    public (int dist, int node) Pop() {
        if (data.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        var result = data[0];
        pos.Remove(result.node);

        int last = data.Count - 1;
        if (last == 0) {
            data.RemoveAt(last);
            return result;
        }

        data[0] = data[last];
        pos[data[0].node] = 0;

        data.RemoveAt(last);

        BubbleDown(0);

        return result;
    }
    
    public void DecreaseKey(int node, int newDist) {
        if (!pos.TryGetValue(node, out int i)) {
            Push(newDist, node);
            return;
        }

        if (newDist >= data[i].dist)
            return;

        data[i] = (newDist, node);
        BubbleUp(i);
    }

    private void BubbleUp(int i) {
        while (i > 0) {
            int parent = (i - 1) / 2;

            if (data[i].dist >= data[parent].dist)
                break;

            Swap(i, parent);
            i = parent;
        }
    }

    private void BubbleDown(int i) {
        while (true) {
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left >= data.Count)
                break;

            int smallest = left;

            if (right < data.Count &&
                data[right].dist < data[left].dist)
            {
                smallest = right;
            }

            if (data[i].dist <= data[smallest].dist)
                break;

            Swap(i, smallest);
            i = smallest;
        }
    }

    private void Swap(int i, int j) {
        (data[i], data[j]) = (data[j], data[i]);

        pos[data[i].node] = i;
        pos[data[j].node] = j;
    }
}