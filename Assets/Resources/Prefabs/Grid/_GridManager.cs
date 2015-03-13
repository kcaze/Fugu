using UnityEngine;
using System.Collections;

public class _GridManager : qObject {
	public Grid grid;
	public float trailDuration;
	private Player player;
	private LineRenderer line;
	private int npoints;
	private Vector3 prevPos;
	private float lineThreshold;

	protected override void qAwake () {
		this.player = (Player) FindObjectOfType(typeof(Player));
		this.grid = gameObject.GetComponent<Grid>();
		this.npoints = 0;
		this.line  = GetComponent<LineRenderer>();
		this.prevPos = player.transform.position;
		this.lineThreshold = 0.3f;
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
		int index = this.grid.WorldToGrid(player.transform.position);
		if (index == -1) {
			Debug.LogError("Player outside level bounds!");
			return;
		}
		HandleTrail(index);
		if ((player.transform.position-this.prevPos).magnitude > lineThreshold) {
			this.line.SetVertexCount(npoints+1);
			this.line.SetPosition(npoints, player.transform.position);
			npoints++;
			this.prevPos = player.transform.position;
		}
	}

	private void HandleTrail(int index) {
		if (this.grid.grid[index] == TileEnum.normal && player.trail == TrailEnum.normal) {
			this.grid.durationGrid[index] = trailDuration;
		}
		if (this.grid.grid[index] == TileEnum.normal || this.grid.grid[index] == TileEnum.normalCircled) return;
		if (player.trail != TrailEnum.normal) return;
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
				grid.FloodFill1(grid.Index(coord.first, coord.second), TileEnum.normal);
				enclosed = true;
			}
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		if (enclosed) {
			AudioManager.instance.playCircled();
			float tileCount = 0;
			for (int ii = 0; ii < grid.gridSize; ii++) {
				if (grid.grid[ii] == TileEnum.normal) {
					grid.grid[ii] = TileEnum.normalCircled;
					tileCount++;
				}
			}
			StartCoroutine(FillEmpty());
			ScoreManager.instance.tileFraction = tileCount / grid.gridSize;
			for (int ii = 0; ii < enemies.Length; ii++) {
				GameObject enemy = enemies[ii];
				if (!enemy.GetComponent<qObject>().isActive) continue;
				Bounds enemyBounds = enemy.collider.bounds;
				Tuple<int,int> bl, tr;
				bl = grid.Coord(grid.WorldToGrid(enemyBounds.min));
				tr = grid.Coord(grid.WorldToGrid(enemyBounds.max));
				if (bl == null && tr == null) continue; // enemy lies outside grid
				if (bl == null) bl = tr; // if enemy partially lies outside of grid
				if (tr == null) tr = bl;
				for (int jj = bl.first; jj <= tr.first; jj++) {
					for (int kk = bl.second; kk <= tr.second; kk++) {
						int enemyIndex = this.grid.WorldToGrid(new Vector3(jj, 0, kk));
						if (this.grid.grid[enemyIndex] == TileEnum.normalCircled) {
							enemy.SendMessage("qDie");
							goto outside;
						}
					}
				}
				outside: continue;
			}
			ScoreManager.instance.tileFraction = 1; // default is 1, so bombs will have no tile multiplier
			this.line.SetVertexCount(0);
			npoints = 0;
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