using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Bombs : MonoBehaviour {
	private UICanvas canvas;
	private Player player;

	private void Awake() {
		player = (Player) FindObjectOfType(typeof(Player));
		canvas = GetComponent<UICanvas>();
	}
	
	private void Update() {
		canvas.n_bombs = player.bombs;
	}
}
