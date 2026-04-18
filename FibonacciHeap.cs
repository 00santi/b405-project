namespace B405_Project;

class Node {
    public int key;
    public Node parent;
    public Node child;
    public Node left;
    public Node right;
    public int degree;
    public bool mark;
    
    public Node(int k) {
        key = k;
        parent = null;
        child = null;
        left = this;
        right = this;
        degree = 0;
        mark = false;
    }
}

class FibHeap {
    public Node min;
    public int n;

    public FibHeap() {
        min = null;
        n = 0;
    }
    
    public void Push(int key) {
        Node x = new Node(key);

        if (min == null)
            min = x;
        else {
            x.left = min;
            x.right = min.right;
            min.right.left = x;
            min.right = x;

            if (x.key < min.key) 
                min = x;
        }

        n++;
    }
    
    void RemoveFromList(Node x) {
        x.left.right = x.right;
        x.right.left = x.left;
    }
    
    public Node Pop() {
        if (min == null) return null;

        Node z = min;
        Node child = z.child;

        if (child != null) {
            Node start = child;
            Node x = child;

            do {
                Node next = x.right;
                
                x.left.right = x.right;
                x.right.left = x.left;
                
                x.left = min;
                x.right = min.right;
                min.right.left = x;
                min.right = x;

                x.parent = null;

                x = next;
            } while (x != start);

            z.child = null;
        }

        RemoveFromList(z);
        if (z == z.right) 
            min = null;
        else {
            min = z.right;

            Node x = min;
            Node start = min;

            do {
                if (x.key < min.key) 
                    min = x;
                x = x.right;
            } while (x != start);
        }
        
        n--;
        Consolidate();
        return z;
    }
    
    void Link(Node y, Node x) {
        y.left.right = y.right;
        y.right.left = y.left;
        
        y.left = y;
        y.right = y;
        
        y.parent = x;

        if (x.child == null)
            x.child = y;
        else {
            y.left = x.child;
            y.right = x.child.right;
            x.child.right.left = y;
            x.child.right = y;
        }

        x.degree++;
        y.mark = false;
    }
    
    void Consolidate() {
        if (min == null || n <= 1) return;

        int maxDegree = (int)(Math.Log(n, 2) * 2) + 2;
        Node[] degreeTable = new Node[maxDegree];
    
        List<Node> rootList = new List<Node>();

        Node x = min;
        if (x != null) {
            Node start = x;

            do {
                rootList.Add(x);
                x = x.right;
            }
            while (x != start);
        }

        min = null;

        foreach (Node w in rootList) {
            x = w;
            int d = x.degree;

            while (degreeTable[d] != null) {
                Node y = degreeTable[d];

                if (x.key > y.key) {
                    Node temp = x;
                    x = y;
                    y = temp;
                }

                Link(y, x);

                degreeTable[d] = null;
                d++;
            }

            degreeTable[d] = x;
        }
    
        for (int i = 0; i < degreeTable.Length; i++) {
            Node node = degreeTable[i];

            if (node != null) {
                if (min == null) {
                    node.left = node;
                    node.right = node;
                    min = node;
                }
                else {
                    node.left = min;
                    node.right = min.right;
                    min.right.left = node;
                    min.right = node;

                    if (node.key < min.key) {
                        min = node;
                    }
                }
            }
        }
    }
    
    void Cut(Node x, Node y) {
        // y is parent of x
        
        if (x.right == x)
            y.child = null;
        else {
            x.left.right = x.right;
            x.right.left = x.left;

            if (y.child == x)
                y.child = x.right;
        }

        y.degree--;

        x.left = min;
        x.right = min.right;
        min.right.left = x;
        min.right = x;

        x.parent = null;
        x.mark = false;
    }
    
    void CascadingCut(Node x) {
        Node y = x.parent;

        if (y != null) {
            if (x.mark == false)
                x.mark = true;
            else
            {
                Cut(x, y);
                CascadingCut(y);
            }
        }
    }
    
    public void DecreaseKey(Node x, int k) {
        if (k > x.key) return;

        x.key = k;
        Node y = x.parent;
        if (y != null && x.key < y.key) {
            Cut(x, y);
            CascadingCut(y);
        }

        if (min == null || x.key < min.key)
            min = x;
    }
}