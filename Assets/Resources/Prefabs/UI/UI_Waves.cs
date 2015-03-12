using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Waves : MonoBehaviour {
	private int total;
    private int prevwave;
    private UICanvas canvas;
	
	private void Awake() {
		canvas= GetComponent<UICanvas>();
        prevwave = 0;
	}
	
	private void Update () {
		total= LevelManager.instance.level.waves.Count;
		int currentwave = LevelManager.instance.waveNumber+1; // level manager's wave number is 0-indexed
        if (prevwave == currentwave) return;
        canvas.levelprogress = ((float)currentwave) / (float)total;
        prevwave = currentwave;
	}
}
