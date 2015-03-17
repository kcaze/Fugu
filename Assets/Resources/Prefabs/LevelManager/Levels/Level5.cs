using UnityEngine;
using System.Collections;

public class Level5 : Level {
	public override void Setup() {
		lw = 30;
		lh = 20;
		BeginLevel();
		NewWave(100);
		for (int ii = 0; ii < 20; ii++) {
			AddEnemy(0, lw/2+9*Mathf.Cos(ii*Mathf.PI/10), lh/2+9*Mathf.Sin(ii*Mathf.PI/10), "Chaser");
		}
		NewWave(100);
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/4, 1+1.5f*ii, "Stander");
			AddEnemy(0, 3*lw/4, 1+1.5f*ii, "Stander");
			AddEnemy(0, lw/2, lh-(1+1.5f*ii), "Stander");
		}
		AddEnemy(0, lw/4, lh-4, "Chaser");
		AddEnemy(0, lw/2, 4, "Chaser");
		AddEnemy(0, 3*lw/4, lh-4, "Chaser");
		AddEnemy(0, 3, 3, "Shooter");
		AddEnemy(0, lw-3, 3, "Standspawner");
		AddEnemy(0, 3, lh-3, "Standspawner");
		AddEnemy(0, lw-3, lh-3, "Shooter");
		NewWave(100);
		for (int ii = 1; ii < 30; ii++) {
			AddEnemy(0, ii, 2, "Chaser");
			AddEnemy(0, ii, lh-2, "Chaser");
		}
		NewWave(100);
		for (int ii = 2; ii < 20; ii += 2) {
			AddEnemy(0, ii % 4 == 0 ? 3 : 6, ii, ii % 4 == 0 ? "Shooter" : "Stander");
			AddEnemy(0, lw-(ii % 4 == 0 ? 3 : 6), ii, ii % 4 == 0 ? "Standspawner" : "Shooter");
		}
		AddEnemy(0, lw/2, 2, "Twohitter");
		AddEnemy(0, lw/2, lh-2, "Twohitter");
		NewWave(100);
		for (int ii = 10; ii <= 50; ii++) {
			float angle = ii*Mathf.PI/10;
			AddEnemy(0, lw/2+0.6f*angle*Mathf.Cos(angle), lh/2+0.6f*angle*Mathf.Sin(angle), ii%10 == 0 ? "Twohitter" : "Chaser");
		}
		NewWave(100);
		for (int ii = 3; ii <= 7; ii += 1) {
			AddEnemy(0, lw/20.0f*ii, lh/20.0f*ii, "Stander");
			AddEnemy(0, lw/20.0f*ii, lh/20.0f*(20-ii), "Stander");
			AddEnemy(0, lw-lw/20.0f*ii, lh-lh/20.0f*ii, "Stander");
			AddEnemy(0, lw-lw/20.0f*ii, lh-lh/20.0f*(20-ii), "Stander");
		}
		AddEnemy(0, lw/2, 1, "Twohitter");
		AddEnemy(0, lw/2, lh-1, "Twohitter");
		AddEnemy(0, 5, lh/2, "Twohitter");
		AddEnemy(0, lw-5, lh/2, "Twohitter");
		AddEnemy(0, lw/2-0.5f, 4, "Shooter");
		AddEnemy(0, lw/2+0.5f, 4, "Standspawner");
		AddEnemy(0, lw/2-0.5f, 5, "Standspawner");
		AddEnemy(0, lw/2+0.5f, 5, "Shooter");
		AddEnemy(0, lw/2-0.5f, lh-5, "Shooter");
		AddEnemy(0, lw/2+0.5f, lh-5, "Standspawner");
		AddEnemy(0, lw/2-0.5f, lh-4, "Standspawner");
		AddEnemy(0, lw/2+0.5f, lh-4, "Shooter");
		EndLevel();
	}
}
