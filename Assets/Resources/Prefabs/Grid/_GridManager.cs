using UnityEngine;
using System.Collections;

public class _GridManager : MonoBehaviour {
	public Grid grid;
	private Player player;
	
	private void Awake () {
		this.player = (Player) FindObjectOfType(typeof(Player));
		this.grid = gameObject.GetComponent<Grid>();
	}
	
	private IEnumerator FillEmpty(int index) {
		yield return new WaitForSeconds(0.15f); //TODO: avoid hardcoded values
		grid.FloodFill2(index, TileEnum.empty);
		yield return null;
	}

	private void Update () {
		int index = this.grid.WorldToGrid(player.transform.position);
		if (index == -1) {
			Debug.LogError("Player outside level bounds!");
			return;
		}
		if (player.trail == TrailEnum.normal) {
			HandleNormalTrail(index);
		}
		else if (player.trail == TrailEnum.slow) {
			HandleSlowTrail(index);
		}

		// let enemies know what tile they're on
		/*for (int ii = 0; ii < LevelManager.instance.enemies.Count; ii++) {
			qEnemy enemy = LevelManager.instance.enemies[ii];
			if (enemy == null || !enemy.isActive) continue;
			int enemyIndex = this.grid.WorldToGrid(enemy.transform.position);
		}*/
	}

	private void HandleNormalTrail(int index) {
		if (this.grid.grid[index] == TileEnum.normal || this.grid.grid[index] == TileEnum.normalCircled) return;
		this.grid.grid[index] = TileEnum.normal;
		
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
		if (enclosed) {
			AudioManager.instance.playCircled();
			grid.FloodFill2(index, TileEnum.normalCircled);
			StartCoroutine(FillEmpty(index));

			GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");	
			for (int ii = 0; ii < enemies.Length; ii++) {
				GameObject enemy = enemies[ii];
				if (enemy == null || !enemy.GetComponent<qObject>().isActive) continue;
				int enemyIndex = this.grid.WorldToGrid(enemy.transform.position);
				if (this.grid.grid[enemyIndex] == TileEnum.normalCircled) {
					Destroy(enemy);
				}
			}
		}
	}

	private void HandleSlowTrail(int index) {
		if (this.grid.grid[index] == TileEnum.slow) return;
		this.grid.grid[index] = TileEnum.slow;
	}
}

public class GridManager : qSingleton<_GridManager> {
}