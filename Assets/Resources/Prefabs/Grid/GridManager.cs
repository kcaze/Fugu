using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {
	private Grid grid;
	private Player player;
	
	private void Start () {
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
		if (index != -1 && player.trail == TrailEnum.normal) {
			this.grid.SetTile(index, TileEnum.normal);
			
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
				// generate light effects (this approach does NOT work)
				/*for (int x = 0; x < grid.gridWidth; x++) {
					for (int y = 0; y < grid.gridHeight; y++) {
						if (grid.grid[grid.Index(x,y)] == TileEnum.normalCircled) {
							GameObject lightObject = Instantiate(Resources.Load("Prefabs/Lights/GridLight")) as GameObject;
							Vector3 position = new Vector3((x+0.5f)*grid.tileWidth, 0.5f, (y+0.5f)*grid.tileHeight);
							lightObject.transform.position = position;
							lightObject.light.intensity = 4;
							lightObject.light.range = 2*Mathf.Max(grid.tileWidth, grid.tileHeight);
							Debug.Log("Hi");
						}
					}
				}*/

				// kill enemies
				for (int ii = 0; ii < LevelManager.instance.enemies.Count; ii++) {
					qEnemy enemy = LevelManager.instance.enemies[ii];
					if (enemy == null || !enemy.isActive) continue;
					int enemyIndex = this.grid.WorldToGrid(enemy.transform.position);
					if (this.grid.grid[enemyIndex] == TileEnum.normalCircled) {
						enemy.Circled(TileEnum.normalCircled);
					}
				}
				StartCoroutine(FillEmpty(index));
			}
		}
	}
}