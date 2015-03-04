using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1 : Level {
	private int lw = 20;
	private int lh = 18;

	public override void Setup() {
		BeginLevel();
		NewWave(1000);
		AddEnemy(5, 5, "Avoider");
		// beginning
		NewWave(7);
		AddEnemy(1,1, "Bouncer");
		AddEnemy(lw-1,1, "Bouncer");
		AddEnemy(1,lh-1, "Bouncer");
		AddEnemy(lw-1,lh-1, "Bouncer");
		NewWave(7);
		AddEnemy(1, lh/2, "Bouncer");
		AddEnemy(lw-1, lh/2, "Bouncer");
		AddEnemy(lw/2, 1, "Bouncer");
		AddEnemy(lw/2, lh-1, "Bouncer");
		NewWave(12);
		AddEnemy(1,1, "Bouncer");
		AddEnemy(lw-1,1, "Bouncer");
		AddEnemy(1,lh-1, "Bouncer");
		AddEnemy(lw-1,lh-1, "Bouncer");
		AddEnemy(1, lh/2, "Bouncer");
		AddEnemy(lw-1, lh/2, "Bouncer");
		AddEnemy(lw/2, 1, "Bouncer");
		AddEnemy(lw/2, lh-1, "Bouncer");
		NewWave(7);
		AddEnemy(1,1, "Bouncer");
		AddEnemy(1,2, "Bouncer");
		AddEnemy(2,1, "Bouncer");
		AddEnemy(2,2, "Bouncer");
		AddEnemy(lw-1, lh-1, "Chaser");
		NewWave(6);
		AddEnemy(1,1, "Chaser");
		AddEnemy(1,2, "Chaser");
		AddEnemy(2,1, "Chaser");
		AddEnemy(2,2, "Chaser");
		AddEnemy(lw-1,lh-1, "Chaser");
		AddEnemy(lw-1,lh-2, "Chaser");
		AddEnemy(lw-2,lh-1, "Chaser");
		AddEnemy(lw-2,lh-2, "Chaser");

		// middle
		NewWave(8);
		for (int ii = 1; ii < lh; ii += 2) {
			AddEnemy(lw/2, ii, "Bouncer");
		}
		NewWave(8);
		for (int ii = 1; ii < lw; ii += 2) {
			AddEnemy(ii, lh/2, "Bouncer");
		}
		NewWave(20);
		for (int ii = 1; ii < lw; ii += 2) {
			AddEnemy(ii, lh/2, "Bouncer");
		}
		for (int ii = 1; ii < lh; ii += 2) {
			AddEnemy(lw/2, ii, "Bouncer");
		}
		NewWave(2);
		for (int ii = 1; ii <= 3; ii++) {
			for (int jj = 1; jj <= 3; jj++) {
				AddEnemy(lw-ii, 1+jj, "Chaser");
			}
		}
		NewWave(8);
		for (int ii = 1; ii <= 3; ii++) {
			for (int jj = 1; jj <= 3; jj++) {
				AddEnemy(1+ii, lh-jj, "Bouncer");
			}
		}
		NewWave(5);
		AddEnemy(lw/2, lh/2, "Shooter");
		AddEnemy(1, lh/2, "Bouncer");
		AddEnemy(lw-1, lh/2, "Bouncer");
		AddEnemy(lw/2, 1, "Bouncer");
		AddEnemy(lw/2, lh-1, "Bouncer");
		NewWave(6);
		AddEnemy(lw/4, lh/4, "Shooter");
		AddEnemy(3*lw/4, 3*lh/4, "Shooter");
		AddEnemy(lw/4, 3*lh/4, "Chaser");
		AddEnemy(3*lw/4, lh/4, "Chaser");
		NewWave(8);
		for (int ii = 1; ii < lw; ii += 2) {
			AddEnemy(ii, 1, "Chaser");
			AddEnemy(ii, lh-1, "Chaser");
		}
		NewWave(3);
		AddEnemy(1,1, "Bouncer");
		AddEnemy(lw-1,1, "Bouncer");
		AddEnemy(1,lh-1, "Bouncer");
		AddEnemy(lw-1,lh-1, "Bouncer");		
		NewWave(3);
		AddEnemy(lw/2,3, "Shooter");
		AddEnemy(lw/2,lh-3, "Shooter");
		NewWave(3);
		AddEnemy(1, lh/2, "Bouncer");
		AddEnemy(lw-1, lh/2, "Bouncer");
		AddEnemy(lw/2, 1, "Bouncer");
		AddEnemy(lw/2, lh-1, "Bouncer");
		NewWave(10);
		AddEnemy(3, lh/2, "Shooter");
		AddEnemy(lw-3, lh/2, "Shooter");

		// ending
		NewWave(10);
		for (int ii = 1; ii < 6; ii++) {
			AddEnemy(lw/2+7*Mathf.Cos(2*Mathf.PI/6*ii), lh/2+7*Mathf.Sin(2*Mathf.PI/6*ii), "Shooter");
		}
		NewWave(20);
		for (int ii = 3; ii < lw - 2; ii += 2) {
			AddEnemy(ii, 1, "Bouncer");
			AddEnemy(ii, lh-1, "Bouncer");
		}
		for (int ii = 3; ii < lh - 2; ii += 2) {
			AddEnemy(1, ii, "Bouncer");
			AddEnemy(lw-1, ii, "Bouncer");
		}

		NewWave(Mathf.Infinity);
		AddEnemy(1,1, "Shooter");
		AddEnemy(lw-1,1, "Shooter");
		AddEnemy(1,lh-1, "Shooter");
		AddEnemy(lw-1,lh-1, "Shooter");
		EndLevel();
	}
}