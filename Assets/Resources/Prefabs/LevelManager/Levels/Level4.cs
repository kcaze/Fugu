using UnityEngine;
using System.Collections;

public class Level4 : Level {
	public override void Setup() {
		lw = 30;
		lh = 20;
		BeginLevel();
		NewWave(10000);
		for (int ii = 0; ii < 1; ii++) {
			AddEnemy(0, Random.Range(0, 10), Random.Range(0,10), "Chaser");
		}
		EndLevel();
	}
}
