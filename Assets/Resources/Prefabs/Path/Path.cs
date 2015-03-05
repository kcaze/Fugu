using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Proper polygon detection (final intersection and color), multi path detection, 
//Class that the motion/player controller calls, handles changes in player input

class Path {
    int size;
    public Node head;
    private Node current;
    public List<Segment> intersections; //Intersection with other paths, used in DFS

    public Path(Segment segment) {
        size = 0;
        head = new Node();
        current = head;
        current.left=segment;
        current.height = 1;
        segment.parent = current;
    }
    //if intersection found returns index; if not then the segment is added to tree and -1 is returned.
    public int insert(Segment segment){
        segment.segment_index = size;
        size++;
        //check for intersection
        if (current.right == null) current.right = segment;
        else {
			while (current.right !=null && current.left.height != current.right.height) {
			//stop when either node is balanced or reached a leaf 
            current = current.right;
        	}
		}
        addToTree(segment);
        int intersect =checkIntersection(segment, head);
        return intersect;
    }
    //inserts a new node above current and adds segment as the current node's sibling
    private void addToTree(Segment segment) {
        Node add = new Node();
        add.height = current.height;
        add.parent = current.parent;
        add.left = current;
        add.right = segment;
        current.parent = add;
        segment.parent = add;
        if (current == head) { 
			head = add;
		}
        else {
			(add.parent).right = add;
		}
        current = add;
        updateFrom(segment);
        while (current !=head &&  current.left.height == current.right.height) {
			current = current.parent;
		}
        //update bounding boxes and tree height
    }

    public void updateFrom(Segment segment) { 
        Node par = segment.parent;
        while (true) { 
            par.updateSize();
            par.height = 1 + Mathf.Max(par.left.height, par.right.height);
            if (par == head){  
          // Debug.Log(size + "+height  " + head.height);
                break;
            }
            par = par.parent;
        }
    }
    //returns index of intersection if exists; -1 if not.
    private int checkIntersection(Segment segment, Node check) {
        if (check == null) return -1;
        if (!segment.intersects_box(check)) return -1;
        if (check.left == null && ((Segment)check).segment_index != segment.segment_index) {
			//is segment
            int i = segment.intersects_segment((Segment)check);
            if (i != -1 && segment.parent != null) {
                segment.truncate_segment((Segment)check);
            }
            return i;
        }
        return Mathf.Max(checkIntersection(segment,check.left), checkIntersection(segment,check.right));
    }
}

class Segment : Node  {
	public float x1,x2,y1,y2;
	public int color;
	public int segment_index;
	public Path path;
		
    public Segment() { }
    public Segment(float x1, float y1, float x2, float y2, int color) {
        left = null; right = null; parent = null; height = 0;
        x = (x1 < x2) ? x1 : x2; ;
        y = (y1 < y2) ? y1 : y2;
        X = (x == x1) ? x2 : x1;
        Y = (y == y1) ? y2 : y1;
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
        this.color = color;
    }
    //returns false for colinear lines too! yay!
    public int intersects_segment(Segment other) {
        //if (Mathf.Pow((x1 - x2) * (other.x1 - other.x2) + (y1 - y2) * (other.y1 - other.y2),2) > 0.95 * (Mathf.Pow((x1 - x2),2)  + Mathf.Pow((y1 - y2),2))
           //                                                                                                                             * (Mathf.Pow((other.x1 - other.x2),2)+ Mathf.Pow(other.y1 - other.y2,2))) return -1;
        if (intersect(x1, y1, x2, y2, other.x1, other.y1, other.x2, other.y2)) {
			return other.segment_index;
		}
		else {
       		return -1;
		}
    }
    private bool intersect(float a1, float a2, float b1, float b2, float c1, float c2,float d1, float d2) {
        return counterclockwise(a1,a2,c1,c2,d1,d2) != counterclockwise(b1,b2,c1,c2,d1,d2) 
            && counterclockwise(a1,a2,b1,b2,c1,c2) != counterclockwise(a1,a2,b1,b2,d1,d2);
    }

    private bool counterclockwise(float a1, float a2, float b1, float b2, float c1, float c2) {
        return (c2-a2) * (b1-a1) > (b2-a2)*(c1-a1);
    }

    public void truncate_segment(Segment other) {
	    float x12 = x1 - x2;
	    float x34 = other.x1 - other.x2;
	    float y12 = y1 - y2;
	    float y34 = other.y1 - other.y2;
	    float c = x12 * y34 - y12 * x34;
	  	// Intersection
	    float a = x1 * y2 - y1 * x2;
	    float b = other.x1* other.y2 - other.y1 * other.x2;
	    float intersect_x = (a * x34 - b * x12) / c;
	    float intersect_y = (a * y34 - b * y12) / c;
	   
	    x2 = intersect_x;
	    y2 = intersect_y;
	    Debug.Log("INTERSECT:  x:" + other.x1 + "->" + intersect_x + "y: "+other.y1 + "->" + intersect_y);
	    other.x1 = x2;
	    other.y1 = y2;
     	//Debug.Log(other.parent.ToString());
    	//Debug.Log(path.ToString());
        path.updateFrom(other);
        path.updateFrom(this);
        /*float hackx = 0.25f*(x1+x2+other.x1+other.x2);
        float hacky = 0.25f*(y1+y2+other.y1+other.y2);
        x2 = hackx;
        other.x1 = hackx;
        y2 = hacky;
        other.y1= hacky;*/
        /*other.x1 = x2;
        other.y1 = y2;*/
    }

};

//class for all line segments and parents in aabb tree
class Node {
	public float x, y, X, Y;//bounding box
	public Node left, right, parent; 
	public int height;

	public Node() {
        left = null; right = null; parent= null;
        height = 0;
    }

    public bool intersects_box(Node other) {
        //returns true if the two aabb boxes intersect in both x and y ranges.
        return !(this.X <= other.x || other.X <= this.x) && !(this.Y <= other.y || other.Y <= this.y);
    }

    public void updateSize() {
        x = left.x;
        y = left.y;
        X = left.X;
        Y = left.Y;
        if(right !=null){
	        x = Mathf.Min(x, right.x);
	        X = Mathf.Max(X, right.X);
	        y = Mathf.Min(y, right.y);
	        Y= Mathf.Max(Y, right.Y);
        }
    }
};

class DisjointCurve {
    public int start, end;
    public Node head;
};

class Polygon {
    uint cycleSize;//number of edges of the polygon
    public int start;
    public Node head;
    bool red;
    bool yellow;
    bool blue;

    public Polygon() {
        red = true;
        yellow = false;
        blue = false;
    }
    public bool isInPolygon(float x, float y) {
        //TODO! Game board dim
        int parity = nIntersection(new Segment(x, y, x, 500.0f, 0), head) * nIntersection(new Segment(x, y, 500.0f, y, 0), head);
            return !(parity % 2 == 0);
    }

    int nIntersection(Segment segment, Node check) {   
        if (check == null) return 0;
        if (!segment.intersects_box(check)) return 0;
        if (check.left == null && ((Segment)check).segment_index >=start && (segment.intersects_segment( (Segment)check)) !=-1)return 1;
        return nIntersection(segment,check.left)+nIntersection(segment,check.right); 

    }
};

struct Vertex {
    double x,y;//point
   // Vertex    next;//used in Polygon
};

