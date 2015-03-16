using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_CoinCount : MonoBehaviour {
	private Text text;

	private void Awake() {
		text = GetComponent<Text>();
	}
	
	private void Update () {
		text.text = ""+ScoreManager.instance.coins;
	}
}
