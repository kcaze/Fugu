using UnityEngine;
using System.Collections;

public class Level1 : Level {
	private int lw = 30;
	private int lh = 26;
	
	public override void Setup() {
		BeginLevel();
		NewWave(3);
		AddMessage("Press z to toggle your trail on and off.", 5.0f);
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(lw/2+3*Mathf.Cos(ii*Mathf.PI/2), lh/2+3*Mathf.Sin(ii*Mathf.PI/2), "Stander");
		}
		NewWave(3);
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(lw/2+6*Mathf.Cos(ii*Mathf.PI/3), lh/2+6*Mathf.Sin(ii*Mathf.PI/3), "Stander");
		}
		NewWave(10);
		for (int ii = 0; ii < 12; ii++) {
			AddEnemy(lw/2+12*Mathf.Cos(ii*Mathf.PI/6), lh/2+12*Mathf.Sin(ii*Mathf.PI/6), "Stander");
		}
		NewWave(1);
		AddEnemy(lw-2, lh-2, "Chaser");
		AddEnemy(2, lh-2, "Chaser");
		AddEnemy(lw-2, 2, "Chaser");
		AddEnemy(2, 2, "Chaser");
		NewWave(1);
		AddEnemy(lw-2, lh-2, "Chaser");
		AddEnemy(2, lh-2, "Chaser");
		AddEnemy(lw-2, 2, "Chaser");
		AddEnemy(2, 2, "Chaser");
		NewWave(1);
		AddEnemy(lw-2, lh-2, "Chaser");
		AddEnemy(2, lh-2, "Chaser");
		AddEnemy(lw-2, 2, "Chaser");
		AddEnemy(2, 2, "Chaser");
		NewWave(1);
		AddEnemy(lw-2, lh-2, "Chaser");
		AddEnemy(2, lh-2, "Chaser");
		AddEnemy(lw-2, 2, "Chaser");
		AddEnemy(2, 2, "Chaser");
		NewWave(120);
		AddEnemy(lw-2, lh-2, "Chaser");
		AddEnemy(2, lh-2, "Chaser");
		AddEnemy(lw-2, 2, "Chaser");
		AddEnemy(2, 2, "Chaser");
		NewWave(120);
		for (int ii = 1; ii < 5; ii++) {
			AddEnemy(ii*lw/5, 2*lh/3, "Bouncer");
		}
		NewWave(120);
		for (int ii = 1; ii < 5; ii++) {
			AddEnemy(ii*lw/5, lh/3, "Bouncer");
		}
		NewWave(120);
		for (int ii = 1; ii < 8; ii++) {
			AddEnemy(lw/4, ii*lh/8, "Bouncer");
		}
		for (int ii = 1; ii < 8; ii++) {
			AddEnemy(lw/3, ii*lh/8, "Stander");
		}
		NewWave(120);
		for (int ii = 1; ii < 8; ii++) {
			AddEnemy(3*lw/4, ii*lh/8, "Bouncer");
		}
		for (int ii = 1; ii < 8; ii++) {
			AddEnemy(2*lw/3, ii*lh/8, "Chaser");
		}
		NewWave(1);
		AddEnemy(lw/4, lh/4, "Bouncer");
		AddEnemy(3*lw/4, 3*lh/4, "Bouncer");
		AddEnemy(lw/4, 3*lh/4, "Chaser");
		AddEnemy(3*lw/4, lh/4, "Chaser");
		NewWave(1);
		AddEnemy(lw/4, lh/4, "Chaser");
		AddEnemy(3*lw/4, 3*lh/4, "Chaser");
		AddEnemy(lw/4, 3*lh/4, "Bouncer");
		AddEnemy(3*lw/4, lh/4, "Bouncer");
		NewWave(1);
		AddEnemy(lw/4, lh/4, "Bouncer");
		AddEnemy(3*lw/4, 3*lh/4, "Bouncer");
		AddEnemy(lw/4, 3*lh/4, "Chaser");
		AddEnemy(3*lw/4, lh/4, "Chaser");
		NewWave(120);
		AddEnemy(lw/4, lh/4, "Chaser");
		AddEnemy(3*lw/4, 3*lh/4, "Chaser");
		AddEnemy(lw/4, 3*lh/4, "Bouncer");
		AddEnemy(3*lw/4, lh/4, "Bouncer");
		NewWave(120);
		AddEnemy(3*lw/5, 3*lh/5, "Shooter");
		AddEnemy(2*lw/5, 3*lh/5, "Shooter");
		AddEnemy(3*lw/5, 2*lh/5, "Shooter");
		AddEnemy(2*lw/5, 2*lh/5, "Shooter");
		NewWave(120);
		AddEnemy(lw-3, lh-3, "Shooter");
		AddEnemy(lw-3, 3, "Shooter");
		AddEnemy(3, lh-3, "Shooter");
		AddEnemy(3, 3, "Shooter");


		EndLevel();
	}
}
