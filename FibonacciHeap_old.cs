namespace B405_Project;

public class FibHeap_old {
    private sealed class Node {
        public int Key;
        public int Degree;
        public bool Mark;

        public Node? Parent;
        public Node? Child;
        public Node Left;
        public Node Right;

        public Node(int key) {
            Key = key;
            Left = this;
            Right = this;
        }
    }

    private Node? min;
    private int count;

    public int Count => count;

    public void Push(int value) {
        var node = new Node(value);
        if (min == null)
            min = node;
        else {
            InsertIntoRootList(node);
            if (node.Key < min.Key)
                min = node;
        }
        count++;
    }

    public int Peek() {
        if (min == null)
            throw new InvalidOperationException("Heap is empty.");

        return min.Key;
    }

    public int Pop() {
        if (min == null)
            throw new InvalidOperationException("Heap is empty.");

        Node z = min;
        bool onlyRoot = z.Right == z;
        Node? nextRoot = onlyRoot ? null : z.Right;

        Node? firstChild = null;

        if (z.Child != null) {
            firstChild = z.Child;
            Node start = z.Child;
            Node x = start;

            do {
                Node next = x.Right;

                x.Parent = null;
                x.Mark = false;
                x.Left = x;
                x.Right = x;

                InsertIntoRootList(x);

                x = next;
            } while (x != start);

            z.Child = null;
        }
        
        RemoveFromList(z);
        count--;

        if (count == 0) {
            min = null;
            return z.Key;
        }

        if (onlyRoot) 
            min = firstChild ?? throw new InvalidOperationException("Heap structure became invalid.");
        else {
            min = nextRoot!;
            Consolidate();
        }

        return z.Key;
    }

    private void InsertIntoRootList(Node node) {
        node.Parent = null;
        node.Mark = false;

        if (min == null) {
            node.Left = node;
            node.Right = node;
            min = node;
            return;
        }

        node.Left = min;
        node.Right = min.Right;
        min.Right.Left = node;
        min.Right = node;
    }

    private static void RemoveFromList(Node node) {
        if (node.Right == node)
            return;

        node.Left.Right = node.Right;
        node.Right.Left = node.Left;
    }

    private void Consolidate() {
        var roots = new List<Node>();
        Node start = min!;
        Node w = start;

        do {
            roots.Add(w);
            w = w.Right;
        } while (w != start);

        Node?[] a = new Node?[64];

        foreach (Node x0 in roots) {
            Node x = x0;
            int d = x.Degree;

            while (true) {
                if (d >= a.Length)
                    Array.Resize(ref a, a.Length * 2);

                if (a[d] == null)
                    break;

                Node y = a[d]!;
                a[d] = null;

                if (x.Key > y.Key)
                    (x, y) = (y, x);

                Link(y, x);
                d++;
            }

            a[d] = x;
        }

        min = null;

        for (int i = 0; i < a.Length; i++) {
            Node? node = a[i];
            if (node == null)
                continue;

            node.Left = node;
            node.Right = node;

            if (min == null)
                min = node;
            else {
                InsertIntoRootList(node);
                if (node.Key < min.Key)
                    min = node;
            }
        }
    }

    private void Link(Node y, Node x) {
        RemoveFromList(y);

        y.Parent = x;
        y.Mark = false;

        if (x.Child == null) {
            x.Child = y;
            y.Left = y;
            y.Right = y;
        }
        else {
            Node c = x.Child;

            y.Left = c;
            y.Right = c.Right;
            c.Right.Left = y;
            c.Right = y;
        }

        x.Degree++;
    }
}