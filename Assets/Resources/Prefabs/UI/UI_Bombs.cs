using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Bombs : MonoBehaviour {
	private Text bombs;
	private Player player;
	
	private void Awake() {
		bombs = GetComponent<Text>();
		player = (Player) FindObjectOfType(typeof(Player));
	}
	
	private void Update() {
		bombs.text = "Bombs: " + player.bombs;
	}
}
