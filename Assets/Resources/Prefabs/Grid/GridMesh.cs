/* In the future, the Grid instance should be constructed from the
 * GridMesh instance, instead of the other way around. GridMesh
 * should take the mesh supplied in the gameObject's Mesh Filter
 * and perform all operations on that. Grid should then take the
 * supplied mesh and generate an undirected graph from that.
 * 
 * Tiles, Grid, and GridMesh should all be reusable components, and
 * all the actual custom game logic should go inside GridManager.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Manages all properties of the grid's mesh, including vertices, triangles, and uvs. */
public class GridMesh : MonoBehaviour{
	private Grid grid;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private Tiles tiles;
	private Mesh mesh;
	private Vector3[] vertices;
	private Vector2[] uv;
	private int[] triangles;
	private Dictionary<TileEnum, Rect> uvRect;

	private void Awake() {
		this.grid = gameObject.GetComponent<Grid>();
		this.meshFilter = gameObject.GetComponent<MeshFilter>();
		this.meshRenderer = gameObject.GetComponent<MeshRenderer>();
		this.tiles = (Tiles) gameObject.GetComponent<Tiles> ();
	}

	// textures is an array of possible textures that could be applied to a tile
	private void Start() {
		this.mesh = new Mesh();
		this.vertices = new Vector3[grid.gridSize*4];
		this.uv = new Vector2[grid.gridSize*4];
		this.triangles = new int[grid.gridSize*6];
		this.uvRect = new Dictionary<TileEnum, Rect> ();

		// set up material for mesh
		Texture2D textureAtlas = new Texture2D(2048, 2048); // TODO: avoid hardcoded values
		Texture2D[] textures = new Texture2D[tiles.list.Length];
		for (int ii = 0; ii < this.tiles.list.Length; ii++) {
			textures[ii] = this.tiles.list[ii].texture;
		}
		Rect[] rects = textureAtlas.PackTextures(textures, 0, 2048);
		for (int ii = 0; ii < this.tiles.list.Length; ii++) {
			this.uvRect[this.tiles.list[ii].type] = rects[ii];
		}
		this.meshRenderer.material = new Material(Shader.Find("Custom/AlphaIllum"));
		//this.meshRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
		this.meshRenderer.material.SetTexture("_MainTex", textureAtlas);

		// set up mesh
		Rect uvRect = this.uvRect[TileEnum.empty];
		int[] dx = {0,1,0,1};
		int[] dy = {0,0,1,1};
		int[] tri = {0,2,1,3,1,2};
		for (int y = 0; y < this.grid.gridHeight; y++) {
			for (int x = 0; x < this.grid.gridWidth; x++) {
				for (int ii = 0; ii < 4; ii++) {
					Vector3 vertPos = new Vector3((x+dx[ii])*grid.tileWidth, 0, (y+dy[ii])*grid.tileHeight);
					Vector3 uvPos = uvRectToUvPos(uvRect, dx[ii], dy[ii]);
					this.vertices[grid.Index(x,y)*4+ii]  = vertPos;
					this.uv[grid.Index(x,y)*4+ii] = uvPos;
				}
				for (int ii = 0; ii < 6; ii++) {
					this.triangles[grid.Index(x,y)*6+ii] = grid.Index(x,y)*4+tri[ii];
				}
			}
		}
		this.mesh.vertices = this.vertices;
		this.mesh.uv = this.uv;
		this.mesh.triangles = this.triangles;
		this.mesh.RecalculateNormals();
		this.meshFilter.mesh = this.mesh;
	}
	
	private void Update() {
		int[] dx = {0,1,0,1};
		int[] dy = {0,0,1,1};
		for (int y = 0; y < this.grid.gridHeight; y++) {
			for (int x = 0; x < this.grid.gridWidth; x++) {
				Rect uvRect = this.uvRect[this.grid.grid[this.grid.Index(x,y)]];
				for (int ii = 0; ii < 4; ii++) {
					Vector2 uv = uvRectToUvPos(uvRect, dx[ii], dy[ii]);
					this.uv[grid.Index(x,y)*4+ii] = uv;

					// TODO: This is just a small example of a cool wave effect on the grid, not meant to actually
					// be in the game.
					//this.vertices[grid.Index(x,y)*4+ii].y = 0.5f*Mathf.Sin(((x-y+dx[ii]-dy[ii])%2)*Mathf.PI/2.0f+1.0f*Time.time);
					//this.vertices[grid.Index(x,y)*4+ii].x += 0.01f*Mathf.Sin(Time.time + ((y+dy[ii])%2)*Mathf.PI/2);
				}
			}
		}
		this.mesh.uv = this.uv;
		this.mesh.vertices = this.vertices;
		this.mesh.RecalculateNormals();
		this.meshFilter.mesh = this.mesh;
	}

	private Vector2 uvRectToUvPos (Rect uvRect, int dx, int dy) {
		Vector2 uv = new Vector2(uvRect.xMin+uvRect.width*dx, uvRect.yMin+uvRect.height*dy);
		//uv.x += ((dx == 0) ? 0.005f : -0.005f);
		//uv.y += ((dy == 0) ? 0.005f : -0.005f);
		return uv;
	}
}