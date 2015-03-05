using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Lives : MonoBehaviour {
	private Text lives;

	private void Awake() {
		lives = GetComponent<Text>();
	}

	private void Update() {
		lives.text = "Lives: " + LevelManager.instance.lives;
	}
}