using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : MonoBehaviour {
	public float seconds_per_sample;
	public int vertexcount;
	[System.NonSerialized]
	public int max_segments; //Set max for path length--clears when run out
	public float board_dim;//gamescene dimension for ray intersecting with polygon
	//private int current_path_index;//Indicates which path we're on
	private int current_color;//Indicates color type
	private int n_segments;//Number of segments in total
	//List<Path> Paths;//All Paths drawn up to now
	Path currentPath;
	private Polygon closedRegion;
	private Player player;
	float vx,vz;
	
	float y;
	float posx, posz;
	float prevx, prevz;
	int index;
	LineRenderer lineRenderer;
	
	public void Start() {
		vx = 0; vz = 0;
		y = transform.position.y;
		index = 0;
		lineRenderer = GetComponent<LineRenderer>();
		max_segments = vertexcount;
		lineRenderer.SetVertexCount(vertexcount);
		InvokeRepeating("PathDraw", 0.0f, seconds_per_sample);
		player = (Player) FindObjectOfType(typeof(Player));
		//Paths = new List<Path> ();
		//current_path_index = 0;
		current_color = 0;
		n_segments = 0;
		currentPath = null;
		closedRegion = null;
	}

	void Update () {
		vx = player.velocityHorizontal;
		vz = player.velocityVertical;
	}

	void PathDraw() {
		if(vx !=0 ||vz !=0){
			prevx = posx;
			prevz = posz;
			posx=player.transform.position.x;
			posz=player.transform.position.z;
			y = player.transform.position.y;
			for (int i = vertexcount - 1; i > index-1 ; i--) {
				lineRenderer.SetPosition(i, new Vector3(posx, y, posz));
			}
			index++;
			//USAGE OF PATH MANAGER: if add segment returns anything other than -1 we have a closed region, and calls to inPolygon can be made
			//Color will be implemented
			if (addSegment(prevx, prevz, posx, posz) != -1) {
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
				for (int ii = 0; ii < enemies.Length; ii++) {
					if (!enemies[ii].GetComponent<qObject>().isActive) continue;
					Vector3 position = enemies[ii].transform.position;
					if (inPolygon(position.x, position.z)) Destroy(enemies[ii]);
				}
				changePath();
				index = 0;
			}
		}
	}
	
	public void changeColor(int color) {
		current_color = color;
	}
	//returns index of intersection in curve if there exists one, -1 if there isn't.
	public int addSegment(float x1, float y1, float x2, float y2) { 
		Segment segment = new Segment(x1,y1,x2,y2, current_color);
		if (currentPath == null) {
			currentPath = new Path(segment);
			segment.path = currentPath;
			Debug.Log(currentPath.ToString());
			return -1;
		}
		segment.path = currentPath;
		int i = currentPath.insert(segment);
		if (i != -1) {
			Debug.Log("INTERSECT");
			closedRegion = new Polygon();
			closedRegion.start = i;
			closedRegion.head = currentPath.head;
		}
		n_segments++;
		return i;
	}
	
	public void changePath() {
		// Paths.Add(currentPath);
		closedRegion = null;
		currentPath = null;
		//current_path_index++;
	}
	public bool inPolygon(float x, float y) { 
		if (closedRegion == null) return false;
		return closedRegion.isInPolygon(x,y); 
	}
	//public  void color(){}
	//public void destroyCycleAndPaths(Path path){}
};