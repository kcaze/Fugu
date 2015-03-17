using UnityEngine;
using System.Collections;

public class ComboTimer : MonoBehaviour {
	private MeshFilter meshFilter;
	Quaternion rotation;

	void Awake() {
		rotation = transform.rotation; // save initial rotation
		meshFilter = gameObject.GetComponent<MeshFilter>();
	}

	void Update() {
		GenerateMesh(ScoreManager.instance.comboMeter/ScoreManager.instance.comboDuration*360, 0.9f, 1.1f, 120);
		transform.rotation = rotation;
	}

	void GenerateMesh(float angle, float inRadius, float outRadius, int divisions) {
		renderer.enabled = angle != 0;
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[2*divisions+6];
		Vector2[] uv = new Vector2[2*divisions+6];
		int[] triangles = new int[6*divisions+9];
		vertices[0] = new Vector3(0, 0, outRadius);
		vertices[1] = new Vector3(inRadius*Mathf.Cos(Mathf.PI/2+Mathf.PI/divisions), 0, inRadius*Mathf.Sin(Mathf.PI/2+Mathf.PI/divisions));
		vertices[2] = new Vector3(outRadius*Mathf.Cos(Mathf.PI/2+2*Mathf.PI/divisions), 0, outRadius*Mathf.Sin(Mathf.PI/2+2*Mathf.PI/divisions));
		triangles[0] = 0;
		triangles[1] = 1;
		triangles[2] = 2;
		uv[0] = new Vector2(0.8f,0.8f);
		uv[1] = new Vector2(0.2f,0.2f);
		uv[2] = new Vector2(0.8f,0.8f);
		for (int ii = 1; ii < angle/180*divisions+2; ii+=2) {
			int idx = (ii+1)/2;
			vertices[2*idx+1] = new Vector3(inRadius*Mathf.Cos(Mathf.PI/2+Mathf.PI/divisions*ii), 0, inRadius*Mathf.Sin(Mathf.PI/2+Mathf.PI/divisions*ii));
			vertices[2*idx+2] = new Vector3(outRadius*Mathf.Cos(Mathf.PI/2+Mathf.PI/divisions*(ii+1)), 0, outRadius*Mathf.Sin(Mathf.PI/2+Mathf.PI/divisions*(ii+1)));
			uv[2*idx+1] = new Vector2(0.2f,0.2f);
			uv[2*idx+2] = new Vector2(0.8f,0.8f);
			triangles[6*idx-3] = 2*idx-1;
			triangles[6*idx-2] = 2*idx+1;
			triangles[6*idx-1] = 2*idx;
			triangles[6*idx+0] = 2*idx;
			triangles[6*idx+1] = 2*idx+1;
			triangles[6*idx+2] = 2*idx+2;
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		meshFilter.mesh = mesh;

	}
}
