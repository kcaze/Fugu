using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TileEnum {empty, normal, normalCircled};

[System.Serializable]
public class Tile {
	public TileEnum type;
	public Texture2D texture;
	[System.NonSerialized]
	public Rect rect;
}

public class Tiles : MonoBehaviour {
	public Tile[] tileTypes;

	[System.NonSerialized]
	public Tile[] list;
	[System.NonSerialized]
	public Dictionary<TileEnum, Tile> dict;

	private void Awake() {
		TileEnum[] types = (TileEnum[])System.Enum.GetValues(typeof(TileEnum));
		this.list = new Tile[types.Length];
		this.dict = new Dictionary<TileEnum, Tile> ();
		for (int ii = 0; ii < types.Length; ii++) {
			int count = 0;
			for (int jj = 0; jj < this.tileTypes.Length; jj++) {
				if (this.tileTypes[jj].type == types[ii]) {
					count++;
					this.list[ii] = this.tileTypes[jj];
					this.dict[types[ii]] = this.tileTypes[jj];
				}
			}
			if (count == 0) {
				Debug.LogError("No tile of type " + types[ii] + " supplied!");
			}
			else if (count > 1) {
				Debug.LogError("Multiple tiles of type " + types[ii] + " supplied!");
			}
		}
	}
}