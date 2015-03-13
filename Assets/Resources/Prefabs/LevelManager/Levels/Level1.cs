using UnityEngine;
using System.Collections;

public class Level1 : Level {
	public override void Setup() {
		lw = 30;
		lh = 20;
		BeginLevel();
		// waveset 1
		NewWave(5);
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw*(ii+3)/9.0f, lh-2, "Stander");
		}
		NewWave(5);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, 5+4*Mathf.Cos(Mathf.PI/2 + 2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(Mathf.PI/2+2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(5);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw-5+4*Mathf.Cos(2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(15);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, 5+4*Mathf.Cos(Mathf.PI/2 + 2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(Mathf.PI/2+2*Mathf.PI/20*ii), "Chaser");
		}
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw-5+4*Mathf.Cos(2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(2*Mathf.PI/20*ii), "Chaser");
		}
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(2.0f, lw*(ii+1)/6, lh-3, "Bouncer");
		}
		NewWave(15);
		AddEnemy(0, 1.5f, 1.5f, "Shooter");
		AddEnemy(0, lw-1.5f, 1.5f, "Shooter");
		AddEnemy(0, 1.5f, lh-1.5f, "Shooter");
		AddEnemy(0, lw-1.5f, lh-1.5f, "Shooter");
		NewWave(1000);
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/10*(ii+1), lh/10*(4-ii), "Chaser");
		}
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/2+lw/10*(ii+1), lh/10*(ii+1), "Chaser");
		}
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(3.0f, lw*(ii+1)/6, 3, "Bouncer");
		}
		AddEnemy(3.0f, lw/2, lh-3, "Shooter");
		AddEnemy(3.0f, lw/2-3, lh-3, "Shooter");
		AddEnemy(3.0f, lw/2+3, lh-3, "Shooter");

		// waveset 2
		NewWave(30);
		for (int ii = 1; ii < 9; ii++) {
			AddEnemy(0.0f, ii*lw/9.0f, ii*lh/9.0f, "Shooter");
		}
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(1.0f, 6+3*Mathf.Cos(Mathf.PI*2*(ii+0.5f)/6), lh-6+3*Mathf.Sin(Mathf.PI*2*(ii+0.5f)/6), "Stander");
		}
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(2.0f, lw-6+3*Mathf.Cos(Mathf.PI*2*ii/6), 6+3*Mathf.Sin(Mathf.PI*2*ii/6), "Stander");
		}
		NewWave(30);
		for (int ii = 1; ii < 9; ii++) {
			AddEnemy(0.0f, ii*lw/9.0f, (9-ii)*lh/9.0f, "Shooter");
		}
		for (int ii = 2; ii < 8; ii++) {
			AddEnemy(1.0f, ii*lw/9.0f-2, (9-ii)*lh/9.0f-2, "Chaser");
		}
		for (int ii = 2; ii < 8; ii++) {
			AddEnemy(2.0f, ii*lw/9.0f+2, (9-ii)*lh/9.0f+2, "Chaser");
		}
		NewWave(10);
		AddEnemy(0, 4, 4, "Twohitter");
		AddEnemy(0, lw-4, lh-4, "Twohitter");
		NewWave(30);
		AddEnemy(0, lw-4, 4, "Twohitter");
		AddEnemy(0, 4, lh-4, "Twohitter");
		NewWave(30);
		AddEnemy(0, lw-4, 4, "Twohitter");
		AddEnemy(0, 4, lh-4, "Twohitter");
		AddEnemy(0, 4, 4, "Twohitter");
		AddEnemy(0, lw-4, lh-4, "Twohitter");
		NewWave(1000);
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(0, lw/2+(ii-2.5f)*3, lh/2-3, "Stander");
			AddEnemy(0, lw/2+(ii-2.5f)*3, lh/2+3, "Stander");
		}
		AddEnemy(0, lw-3, lh/2, "Twohitter");
		AddEnemy(0, 3, lh/2, "Twohitter");


		// waveset 3
		NewWave(20);
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/10*(ii+1), lh/10*(6+ii), "Bouncer");
		}
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/2+lw/10*(ii+1), lh/10*(9-ii), "Bouncer");
		}
		for (int ii = 0; ii <= 10; ii++) {
			AddEnemy(0, lw/2+10*Mathf.Cos(Mathf.PI/10*ii), 2+10*Mathf.Sin(Mathf.PI/10*ii), "Stander");
		}
		NewWave(20);
		AddEnemy(0, lw/2, lh/2-5, "Shooter");
		AddEnemy(0, lw/2, lh/2-3, "Shooter");
		AddEnemy(0, lw/2, lh/2-1, "Shooter");
		AddEnemy(0, lw/2, lh/2+1, "Shooter");
		AddEnemy(0, lw/2, lh/2+3, "Shooter");
		AddEnemy(0, lw/2, lh/2+5, "Shooter");
		for (int ii = 0; ii < 9; ii++) {
			AddEnemy(0, lw/2+8+4*Mathf.Cos(-Mathf.PI/2+ii*Mathf.PI/8), lh/2+4*Mathf.Sin(-Mathf.PI/2+ii*Mathf.PI/8), "Chaser");
			AddEnemy(0, lw/2-8+4*Mathf.Cos(Mathf.PI/2+ii*Mathf.PI/8), lh/2+4*Mathf.Sin(Mathf.PI/2+ii*Mathf.PI/8), "Chaser");
		}
		NewWave(5);
		for (int ii = -7; ii <= 7; ii += 2) {
			AddEnemy(0, lw/2+ii, lh/2, "Shooter");
		}
		AddEnemy(0, lw/2-4, lh-3, "Twohitter");
		AddEnemy(0, lw/2+4, lh-3, "Twohitter");
		NewWave(25);
		AddEnemy(0, lw/2-4, 3, "Twohitter");
		AddEnemy(0, lw/2+4, 3, "Twohitter");
		NewWave(25);
		for (int ii = 0; ii < 12; ii++) {
			AddEnemy(0, lw/2+9*Mathf.Cos(Mathf.PI/6*ii), lh/2+9*Mathf.Sin(Mathf.PI/6*ii), "Chaser");
		}
		NewWave(1000);
		for (int ii = 0; ii < 9; ii++) {
			AddEnemy(ii*1.0f, lw/2 + (ii%2 == 0 ? 5 : -5), 5, "Chaser");
			AddEnemy(ii*1.0f, lw/2 + (ii%2 == 0 ? 5 : -5), lh-5, "Chaser");
		}
		for (int ii = 0; ii < 3; ii++) {
			AddEnemy(ii*3.0f, lw/2 + (ii%2 == 0 ? -5 : 5), lh/2, "Twohitter");
		}
		EndLevel();
	}
}
