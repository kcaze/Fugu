using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public int gridWidth;
	public int gridHeight;
	public float tileWidth;
	public float tileHeight;
	public TileEnum[] grid { get; private set; }
	public int gridSize { get; private set; }

	private void Awake() {
		this.gridSize = this.gridWidth*this.gridHeight;
		this.grid = new TileEnum[this.gridSize];
		for (int ii = 0; ii < this.gridSize; ii++) {
			this.grid[ii] = TileEnum.empty;
		}
	}

	private bool IsValidIndex(int index) {
		return (0 <= index && index < this.gridSize) ? true : false;
	}

	private bool IsValidCoord(int x, int y) {
		return (0 <= x && x < this.gridWidth && 0 <= y && y < this.gridHeight) ? true : false;
	}

	/* Converts (x,y) grid coordinate to grid index. 
	 * Returns -1 if lies outside of grid. */
	public int Index(int x, int y) {
		if (!this.IsValidCoord(x,y)) return -1;
		return y*this.gridWidth + x;
	}

	/* Converts grid index to grid coordinate (x,y). 
	 * Returns null if lies outside of grid. */
	public Tuple<int, int> Coord(int index) {
		if (!this.IsValidIndex(index)) return null;
		return new Tuple<int,int> (index%this.gridWidth, index/this.gridWidth);
	}
	
	/* Converts a point in world space to grid index. 
	 * The y-coordinate is ignored.
	 * Returns -1 if lies outside of grid. */
	public int WorldToGrid(Vector3 worldPoint) {
		Vector3 localPoint = transform.InverseTransformPoint(worldPoint);
		int x = (int)(localPoint.x/this.tileWidth);
		int y = (int)(localPoint.z/this.tileHeight);
		return this.Index(x,y);
	}

	/* Sets the tile at position index to type.*/
	public void SetTile(int index, TileEnum type) {
		if (!this.IsValidIndex(index)) {
			Debug.Log("Attempted to update grid at invalid index " + index);
			return;
		}
		if (this.grid[index] == type || this.grid[index] == TileEnum.normalCircled) {
			return;
		}
		this.grid[index] = type;

	}

	/* Tests if the tile at position index is enclosed by tiles of type tileType.
	 * Returns false if the tile at position index is already of type tileType.
	 */
	public bool IsEnclosed(int index, TileEnum tileType) {
		if (!this.IsValidIndex(index) || this.grid[index] == tileType) return false;

		bool ret = true;
		int[] dx = {1, 0, -1, 0};
		int[] dy = {0, 1, 0, -1};
		List<int> seen = new List<int> ();
		List<TileEnum> seenTypes = new List<TileEnum> ();
		List<int> todo = new List<int> ();
		seen.Add(index);
		seenTypes.Add(this.grid[index]);
		todo.Add(index);
		for (int ii = 0; ii < todo.Count;) {
			int count = todo.Count;
			for (; ii < count; ii++) {
				Tuple<int, int> coord = this.Coord(todo[ii]);
				for (int jj = 0; jj < 4; jj++) {
					index = this.Index(coord.first+dx[jj], coord.second+dy[jj]);
					if (!IsValidIndex(index)) {
						ret = false;
						goto loopBreak;
					}
					if (this.grid[index] != tileType) {
						seen.Add(index);
						seenTypes.Add(this.grid[index]);
						todo.Add(index);
						this.grid[index] = tileType;
					}
				}
			}
		}
		loopBreak:
		for (int ii = 0; ii < seen.Count; ii++) {
			this.grid[seen[ii]] = seenTypes[ii];
		}
		return ret;
	}

	/* Flood fills from position index using type tileType.
	 * Overwrites tiles not of tileType and treats tiles of tileType as boundary.*/
	public void FloodFill1(int index, TileEnum tileType) {
		if (!this.IsValidIndex(index) || this.grid[index] == tileType) return;
		
		int[] dx = {1, 0, -1, 0};
		int[] dy = {0, 1, 0, -1};
		List<int> todo = new List<int> ();
		todo.Add(index);
		this.grid[index] = tileType;
		for (int ii = 0; ii < todo.Count;) {
			int count = todo.Count;
			for (; ii < count; ii++) {
				Tuple<int, int> coord = this.Coord(todo[ii]);
				for (int jj = 0; jj < 4; jj++) {
					index = this.Index(coord.first+dx[jj], coord.second+dy[jj]);
					if (!IsValidIndex(index)) {
						continue;
					}
					if (this.grid[index] != tileType) {
						todo.Add(index);
						this.grid[index] = tileType;
					}
				}
			}
		}
	}

	/* Flood fills from position index using type tileType.
	 * Only overwrites tiles of the type found at index. Treats other tiletypes as boundaries*/
	public void FloodFill2(int index, TileEnum tileType) {
		if (!this.IsValidIndex(index)) return;
		if (tileType == this.grid[index]) return;
		
		int[] dx = {1, 0, -1, 0};
		int[] dy = {0, 1, 0, -1};
		List<int> todo = new List<int> ();
		TileEnum type = this.grid[index];
		todo.Add(index);
		this.grid[index] = tileType;
		for (int ii = 0; ii < todo.Count;) {
			int count = todo.Count;
			for (; ii < count; ii++) {
				Tuple<int, int> coord = this.Coord(todo[ii]);
				for (int jj = 0; jj < 4; jj++) {
					index = this.Index(coord.first+dx[jj], coord.second+dy[jj]);
					if (!IsValidIndex(index) || this.grid[index] != type) {
						continue;
					}
					todo.Add(index);
					this.grid[index] = tileType;
				}
			}
		}
	}
}