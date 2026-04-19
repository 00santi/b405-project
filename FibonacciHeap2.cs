namespace B405_Project;

class Node2 {
    public int dist;
    public int node;
    public Node2 parent;
    public Node2 child;
    public Node2 left;
    public Node2 right;
    public int degree;
    public bool mark;
    
    public Node2(int d, int v) {
        dist = d;
        node = v;
        parent = null;
        child = null;
        left = this;
        right = this;
        degree = 0;
        mark = false;
    }
}

class FibHeap2 {
    public Node2 min;
    public int n;

    public FibHeap2() {
        min = null;
        n = 0;
    }
    
    public Node2 Push(int dist, int node) {
        Node2 x = new Node2(dist, node);

        if (min == null)
            min = x;
        else {
            x.left = min;
            x.right = min.right;
            min.right.left = x;
            min.right = x;

            if (x.dist < min.dist) 
                min = x;
        }

        n++;
        return x;
    }
    
    void RemoveFromList(Node2 x) {
        x.left.right = x.right;
        x.right.left = x.left;
    }
    
    public Node2 Pop() {
        if (min == null) 
            return null;

        Node2 z = min;
        Node2 child = z.child;

        if (child != null) {
            Node2 start = child;
            Node2 x = child;

            do {
                Node2 next = x.right;
                
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

            Node2 x = min;
            Node2 start = min;

            do {
                if (x.dist < min.dist) 
                    min = x;
                x = x.right;
            } while (x != start);
        }
        
        n--;
        Consolidate();
        return z;
    }
    
    void Link(Node2 y, Node2 x) {
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
        if (min == null || n <= 1) 
            return;

        int maxDegree = (int)(Math.Log(n, 2) * 2) + 2;
        var degreeTable = new Node2[maxDegree];
    
        var rootList = new List<Node2>();

        var x = min;
        if (x != null) {
            var start = x;

            do {
                rootList.Add(x);
                x = x.right;
            }
            while (x != start);
        }

        min = null;

        foreach (var w in rootList) {
            x = w;
            int d = x.degree;

            while (degreeTable[d] != null) {
                var y = degreeTable[d];

                if (x.dist > y.dist) {
                    var temp = x;
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
            var node = degreeTable[i];

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

                    if (node.dist < min.dist) {
                        min = node;
                    }
                }
            }
        }
    }
    
    void Cut(Node2 x, Node2 y) {
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
    
    void CascadingCut(Node2 x) {
        Node2 y = x.parent;

        if (y != null) {
            if (x.mark == false)
                x.mark = true;
            else {
                Cut(x, y);
                CascadingCut(y);
            }
        }
    }
    
    public void DecreaseKey(Node2 x, int d) {
        if (d > x.dist) 
            return;

        x.dist = d;
        Node2 y = x.parent;
        if (y != null && x.dist < y.dist) {
            Cut(x, y);
            CascadingCut(y);
        }

        if (min == null || x.dist < min.dist)
            min = x;
    }
}