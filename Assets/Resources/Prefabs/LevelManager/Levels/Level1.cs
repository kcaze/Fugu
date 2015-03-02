using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1 : Level {
	public override void Setup() {
		BeginLevel();
		NewWave();
		GenerateRandom(3, 0.0f, "Wanderer");
		NewWave();
		GenerateRandom(3, 0.5f, "Wanderer");
		GenerateRandom(3, 2.0f, "Wanderer");
		NewWave();
		GenerateRandom(10, 1.0f, "Wanderer");
		NewWave();
		GenerateRandom(4, 1.0f, "Chaser");
		NewWave();
		GenerateRandom(4, 0.5f, "Chaser");
		GenerateRandom(6, 3.5f, "Chaser");
		NewWave();
		GenerateRandom(3, 0.5f, "Chaser");
		GenerateRandom(3, 0.5f, "Wanderer");
		GenerateRandom(5, 4.0f, "Wanderer");
		GenerateRandom(3, 4.0f, "Wanderer");
		GenerateRandom(3, 6.0f, "Wanderer");
		NewWave();
		GenerateRandom(7, 2.5f, "Chaser");
		GenerateRandom(7, 2.5f, "Wanderer");
		GenerateRandom(3, 8.5f, "Wanderer");
		GenerateRandom(3, 8.5f, "Chaser");
		GenerateRandom(10, 12.0f, "Wanderer");
		EndLevel();
	}

	private void GenerateRandom(int n, float t, string type) {
		for (int ii = 0; ii < n; ii++) {
			float x = Random.Range(0.0f, LevelManager.instance.levelWidth);
			float y = Random.Range(0.0f, LevelManager.instance.levelHeight);
			AddEnemy(t, x, y, type);
		}
	}
}