using UnityEngine;
using System.Collections;

public class Level4 : Level {
	public override void Setup() {
		lw = 35;
		lh = 5;
		BeginLevel();
		NewWave(100);
		AddEnemy(0, lw-5, lh/2.0f, "Attracter");
		NewWave(100);
		AddEnemy(0, 5, lh/2.0f, "Attracter");
		AddEnemy(0, lw-5, lh/2.0f, "Chaser");
		NewWave(100);
		AddEnemy(0, lw-5, lh/2.0f, "Attracter");
		AddEnemy(0, 5, lh/2.0f, "Twohitter");
		NewWave(100);
		AddEnemy(0, lw-5, lh/2.0f, "Stander");
		AddEnemy(0, lw-4, lh/2.0f, "Chaser");
		AddEnemy(0, lw-6, lh/2.0f, "Chaser");
		AddEnemy(0, lw-5, lh/2.0f+1, "Chaser");
		AddEnemy(0, lw-5, lh/2.0f-1, "Chaser");
		AddEnemy(0, 5, lh/2.0f, "Attracter");
		NewWave(100);
		AddEnemy(0, 5, lh/2.0f, "Standspawner");
		AddEnemy(0, 4, lh/2.0f, "Shooter");
		AddEnemy(0, 6, lh/2.0f, "Shooter");
		AddEnemy(0, 5, lh/2.0f+1, "Shooter");
		AddEnemy(0, 5, lh/2.0f-1, "Shooter");
		AddEnemy(0, lw-5, lh/2.0f, "Attracter");

		NewWave(100);
		for (int ii = 1; ii <= 8; ii++) {
			AddEnemy(0, ii, lh-1, "Chaser");
		}
		for (int ii = 1; ii <= 8; ii++) {
			AddEnemy(0, (lw-ii), 1, "Chaser");
		}
		NewWave(100);
		for (int ii = 1; ii <= 8; ii++) {
			AddEnemy(0, (lw-ii), lh-1, "Chaser");
		}
		for (int ii = 1; ii <= 8; ii++) {
			AddEnemy(0, ii, 1, "Chaser");
		}
		AddEnemy(0,lw/2.0f, lh/2.0f, "Attracter");
		NewWave(100);
		AddEnemy(0, lw/2.0f + 3, 1, "Stander");
		AddEnemy(0, lw/2.0f + 3, 2, "Stander");
		AddEnemy(0, lw/2.0f + 3, 3, "Stander");
		AddEnemy(0, lw/2.0f - 3, 4, "Stander");		
		AddEnemy(0, lw/2.0f - 3, 3, "Stander");
		AddEnemy(0, lw/2.0f - 3, 2, "Stander");
		AddEnemy(0, 5, lh/2.0f, "Attracter");
		AddEnemy(0, lw-5, lh/2.0f, "Twohitter");
		NewWave(100);
		AddEnemy(0, lw/2.0f+1, 1, "Shooter");
		AddEnemy(0, lw/2.0f-1, 2, "Standspawner");
		AddEnemy(0, lw/2.0f+1, 3, "Shooter");
		AddEnemy(0, lw/2.0f-1, 4, "Standspawner");
		NewWave(100);
		for (int ii = 1; ii < 5; ii++) {
			AddEnemy(0, 1, ii, "Chaser");
			AddEnemy(0, lw-1, ii, "Chaser");
		}
		AddEnemy(0, lw/2.0f, lh/2.0f, "Twohitter");
		NewWave(100);
		for (int ii = 0; ii < 12; ii++) {
			if (ii % 3 == 0) continue;
			AddEnemy(0, lw-ii, lh/2.0f, ii%3 == 1 ? "Shooter" : "Standspawner");
		}
		AddEnemy(0, 3, lh/2.0f, "Twohitter");
		NewWave(100);
		for (int ii = 1; ii < 15; ii++) {
			AddEnemy(0, ii, ii/3.0f, "Chaser");
		}
		for (int ii = 1; ii < 15; ii++) {
			AddEnemy(0, 5+ii, ii/3.0f, "Chaser");
		}
		AddEnemy(0, 1, lh-1, "Attracter");
		AddEnemy(0, lw-1.5f, 1.5f, "Shooter");
		/*NewWave(100);
		for (int ii = 1; ii < 15; ii++) {
			AddEnemy(0, lw-ii, ii/3.0f, "Chaser");
		}
		for (int ii = 1; ii < 15; ii++) {
			AddEnemy(0, lw-(5+ii), ii/3.0f, "Chaser");
		}
		AddEnemy(0, lw-1, lh-1, "Attracter");
		AddEnemy(0, 1.5f, 1.5f, "Twohitter");*/
		EndLevel();
	}
}
