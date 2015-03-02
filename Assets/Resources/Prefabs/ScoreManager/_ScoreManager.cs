using UnityEngine;
using System.Collections;

public class _ScoreManager : MonoBehaviour {
	[System.NonSerialized]
	public int score;

	private void Awake() {
		score = 0;
	}
}

public class ScoreManager : qSingleton<_ScoreManager> {
}