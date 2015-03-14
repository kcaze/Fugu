using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : Level {
	public override void Setup() {
		lw = 30;
		lh = 14;
		BeginLevel();
		NewWave(100);
		AddEnemy(0, 5, lh/2, "Shooter");
		AddEnemy(0, lw-5, lh/2, "Shooter");
		NewWave(100);
		AddEnemy(0, lw/2, lh/2, "Shooter");
		AddEnemy(0, 5, lh/2, "Stander");
		AddEnemy(0, lw-5, lh/2, "Stander");
		NewWave(100);
		for (int ii = 0; ii < 3; ii++) {
			AddEnemy(0, lw/2 + (ii-1)*5, lh-3, "Chaser");
		}
		NewWave(100);
		for (int ii = 0; ii < 3; ii++) {
			AddEnemy(0, lw/2 + (ii-1)*5, 3, "Chaser");
		}
		NewWave(100);
		for (int ii = 0; ii < 3; ii++) {
			AddEnemy(0, 4, lh/2 + (ii-1)*3, "Stander");
		}
		AddEnemy(0, lw-4, lh/2, "Chaser");
		NewWave(100);
		for (int ii = 0; ii < 3; ii++) {
			AddEnemy(0, 4, lh/2 + (ii-1)*3, "Chaser");
		}
		AddEnemy(0, lw-4, lh/2, "Stander");
		NewWave(100);
		AddEnemy(0, 3, 3, "Shooter");
		AddEnemy(0, lw-3, 3, "Shooter");
		AddEnemy(0, 3, lh-3, "Shooter");
		AddEnemy(0, lw-3, lh-3, "Shooter");
		NewWave(100);
		AddEnemy(0, lw/2, lh/2, "Standspawner");
		NewWave(100);
		AddEnemy(0, 8, lh-4, "Standspawner");
		AddEnemy(0, lw-8, 4, "Standspawner");
		NewWave(100);
		AddEnemy(0, lw-8, lh-4, "Shooter");
		AddEnemy(0, 8, 4, "Shooter");
		NewWave(100);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw/2+10*Mathf.Cos(ii*Mathf.PI/4), 0.5f+10*Mathf.Sin(ii*Mathf.PI/4), "Chaser");
		}
		NewWave(100);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw/2+10*Mathf.Cos(Mathf.PI+ii*Mathf.PI/4), (lh-0.5f)+10*Mathf.Sin(Mathf.PI+ii*Mathf.PI/4), "Chaser");
		}
		NewWave(100);
		for (int ii = 1; ii < lh; ii+=2) {
			AddEnemy(0, lw/2, ii, "Chaser");
		}
		AddEnemy(0, lw-3, lh/2-1, "Standspawner");
		AddEnemy(0, lw-3, lh/2+1, "Shooter");
		AddEnemy(0, 3, lh/2-1, "Shooter");
		AddEnemy(0, 3, lh/2+1, "Standspawner");
		EndLevel();
	}
}