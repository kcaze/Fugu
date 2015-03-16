using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _GridManager : qObject {
	public Grid grid;
	public float trailDuration;
	private Player player;

	protected override void qAwake () {
		this.player = (Player) FindObjectOfType(typeof(Player));
		this.grid = gameObject.GetComponent<Grid>();
	}

	protected override void qStart() {
		this.grid.Initialize(LevelManager.instance.levelWidth, LevelManager.instance.levelHeight);
	}

	private IEnumerator FillEmpty() {
		yield return new WaitForSeconds(0.15f); //TODO: avoid hardcoded values
		for (int ii = 0; ii < grid.gridSize; ii++) {
			if (grid.grid[ii] == TileEnum.normalCircled) {
				grid.grid[ii] = TileEnum.empty;
				grid.durationGrid[ii] = 0;
			}
		}
		yield return null;
	}

	private void Update () {
		if (player.trail == TrailEnum.normal) {
			// TODO: this is probably buggy  
			// attempts to player's position and highlight the intermediate squares
			// it corrects for lag which can cause the player to "teleport".
			// the following is essentially just Wikipedia's pseudocode for Bresenham's algorithm
			Tuple<int,int> prevCoord = grid.Coord(grid.WorldToGrid(player.previousPosition));
			Tuple<int,int> coord = grid.Coord(grid.WorldToGrid(player.transform.position));
			int x0, x1, y0, y1;
			if (prevCoord.first > coord.first) {
				x0 = coord.first;
				x1 = prevCoord.first;
				y0 = coord.second;
				y1 = prevCoord.second;
			} else {
				x0 = prevCoord.first;
				x1 = coord.first;
				y0 = prevCoord.second;
				y1 = coord.second;

			}
			// vertical line
			if (x0 == x1) {
				if (y0 > y1) { y0 = y0+y1; y1 = y0-y1; y0 = y0-y1; } // swap y0 and y1
				for (int y = y0; y <= y1; y++) {
					HandleTrail(x0, y);
				}
			} else {
				float dx = x1 - x0;
				float dy = y1 - y0;
				float err = 0;
				float derr = Mathf.Abs(dy/dx);
				int y = y0;
				for (int x = x0; x <= x1; x++) {
					HandleTrail(x,y);
					err = err + derr;
					while (err >= 0.5) {
						HandleTrail(x,y);
						y = y + (y1 == y0 ? 0 : (y1 > y0 ? 1 : -1));
						err = err - 1;
					}
				}
			}
		}
	}

	private void HandleTrail(int x, int y) {
		int index = grid.Index(x,y);
		if (index == -1) {
			Debug.LogError("Player outside level bounds!");
			return;
		}
		if (grid.grid[index] == TileEnum.normalCircled) return;
		this.grid.grid[index] = TileEnum.normal;
		this.grid.durationGrid[index] = trailDuration;

		int[] dx = {1, 0, -1, 0};
		int[] dy = {0, 1, 0, -1};
		bool enclosed = false;
		for (int ii = 0; ii < 4; ii++) {
			Tuple<int, int> coord = grid.Coord(index);
			coord.first += dx[ii];
			coord.second += dy[ii];
			if (grid.IsEnclosed(grid.Index(coord.first, coord.second), TileEnum.normal)) {
				grid.FloodFill2(grid.Index(coord.first, coord.second), TileEnum.normalCircled);
				enclosed = true;
			}
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		if (enclosed) {
			AudioManager.instance.playCircled();

			for (int ii = 0; ii < grid.gridSize; ii++) {
				if (grid.grid[ii] == TileEnum.normal) {
					grid.grid[ii] = TileEnum.normalCircled;
				}
			}

			StartCoroutine(FillEmpty());
			for (int ii = 0; ii < enemies.Length; ii++) {
				GameObject enemy = enemies[ii];
				if (!enemy.GetComponent<qObject>().isActive) continue;
				Bounds enemyBounds = enemy.collider.bounds;
				Tuple<int,int> bl, tr;
				bl = grid.Coord(grid.WorldToGrid(enemyBounds.min));
				tr = grid.Coord(grid.WorldToGrid(enemyBounds.max));
				if (bl == null && tr == null) continue; // enemy lies outside grid
				if (bl == null || tr == null) { 
					// enemy lies partially outside grid, we'll approximate by using enemy center.
					int enemyIndex = this.grid.WorldToGrid(enemy.transform.position);
					if (this.grid.grid[enemyIndex] == TileEnum.normalCircled) {
						enemy.SendMessage("qDie");
					}
				}
				else {
					for (int jj = bl.first; jj <= tr.first; jj++) {
						for (int kk = bl.second; kk <= tr.second; kk++) {
							int enemyIndex = this.grid.WorldToGrid(new Vector3(jj*grid.tileWidth, 0, kk*grid.tileHeight));
							if (this.grid.grid[enemyIndex] == TileEnum.normalCircled) {
								enemy.SendMessage("qDie");
								goto outside;
							}
						}
					}
				}
				outside: continue;
			}
		}
	}
	
	private void ClearNormal() {
		for (int ii = 0; ii < grid.gridSize; ii++) {
			if (grid.grid[ii] == TileEnum.normal) {
				grid.grid[ii] = TileEnum.empty;
			}
		}
	}
}

public class GridManager : qSingleton<_GridManager> {
}