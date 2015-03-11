using UnityEngine;
using System.Collections;

public class _ScoreManager : MonoBehaviour {
	[System.NonSerialized]
	public int score;
	[System.NonSerialized]
	public int combo;
	[System.NonSerialized]
	public float tileFraction;

	private void Awake() {
		score = 0;
		combo = 1;
	}

	public int ComputeScore(int baseScore) {
		float tileMultiplier = 1+Mathf.Log(2-tileFraction, 2)*4;
		return Mathf.CeilToInt(baseScore*combo*tileMultiplier);
	}
}

public class ScoreManager : qSingleton<_ScoreManager> {
}