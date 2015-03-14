using UnityEngine;
using System.Collections;

public class Level1 : Level {
	public override void Setup() {
		lw = 25;
		lh = 18;
		BeginLevel();
		// waveset 1
		NewWave(100);
		AddEnemy(0, 4, 4, "Stander");
		AddEnemy(0, lw-4, lh-4, "Stander");
		NewWave(100);
		AddEnemy(0, 4, lh-4, "Stander");
		AddEnemy(0, lw-4, 4, "Stander");
		NewWave(100);
		AddEnemy(0, lw/2, lh/2, "Chaser");
		NewWave(100);
		AddEnemy(0, 5, lh/2, "Chaser");
		NewWave(100);
		AddEnemy(0, lw-5, lh/2, "Chaser");
		NewWave(100);
		AddEnemy(0, lw/2, 5, "Chaser");
		AddEnemy(0, lw/2, lh-5, "Chaser");
		NewWave(100);
		AddEnemy(0, lw/2, lh/2, "Chaser");
		AddEnemy(0, 4, 4, "Stander");
		AddEnemy(0, lw-4, 4, "Stander");
		AddEnemy(0, 4, lh-4, "Stander");
		AddEnemy(0, lw-4, lh-4, "Stander");
		NewWave(100);
		AddEnemy(0, lw/2, lh/2, "Stander");
		AddEnemy(0, 4, 4, "Chaser");
		AddEnemy(0, lw-4, 4, "Chaser");
		AddEnemy(0, 4, lh-4, "Chaser");
		AddEnemy(0, lw-4, lh-4, "Chaser");
		EndLevel();
	}
}
