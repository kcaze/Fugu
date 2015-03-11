using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Combo : MonoBehaviour {
	private Text combo;

	private void Awake() {
		combo = GetComponent<Text>();
	}
	
	private void Update() {
		combo.text = "x" + ScoreManager.instance.combo;
	}
}
