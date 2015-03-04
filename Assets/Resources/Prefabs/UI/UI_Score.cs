using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Score : MonoBehaviour {
	private Text score;

	private void Awake() {
		score = GetComponent<Text>();
	}

	private void Update () {
		score.text = ScoreManager.instance.score.ToString();
	}
}
