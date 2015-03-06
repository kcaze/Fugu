using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Waves : MonoBehaviour {
	private Text waveInfo;
	
	private void Awake() {
		waveInfo = GetComponent<Text>();
	}
	
	private void Update () {
		int waveNumber = LevelManager.instance.waveNumber+1; // level manager's wave number is 0-indexed
		int totalWaves = LevelManager.instance.level.waves.Count;
		waveInfo.text = "Wave " + waveNumber + " of " + totalWaves;
	}
}
