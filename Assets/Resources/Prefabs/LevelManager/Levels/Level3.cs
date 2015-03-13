using UnityEngine;
using System.Collections;

public class Level3 : Level {
	public override void Setup() {
		lw = 30;
		lh = 20;
		BeginLevel();
		// waveset 1
		NewWave(100);
		for (int ii = 0; ii < 2; ii++) {
			AddEnemy(0, lw/2+(ii-0.5f)*10, 2, "Stander");
		}
		NewWave(100);
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/2+(ii-1.5f)*5, lh-2, "Stander");
		}
		NewWave(100);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, 5+4*Mathf.Cos(Mathf.PI/2 + 2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(Mathf.PI/2+2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(100);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw-5+4*Mathf.Cos(2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(100);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, 5+4*Mathf.Cos(Mathf.PI/2 + 2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(Mathf.PI/2+2*Mathf.PI/20*ii), "Chaser");
		}
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw-5+4*Mathf.Cos(2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(100);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0.0f, lw*(ii+1)/6, lh-3, "Bouncer");
		}
		NewWave(100);
		AddEnemy(0, 1.5f, 1.5f, "Shooter");
		AddEnemy(0, lw-1.5f, 1.5f, "Shooter");
		AddEnemy(0, 1.5f, lh-1.5f, "Shooter");
		AddEnemy(0, lw-1.5f, lh-1.5f, "Shooter");
		NewWave(100);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0.0f, lw*(ii+1)/6, lh-3, "Bouncer");
		}
		AddEnemy(0.0f, lw/2-3, 3, "Shooter");
		AddEnemy(0.0f, lw/2+3, 3, "Shooter");
		NewWave(100);
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/10*(ii+1), lh/10*(1+ii), "Chaser");
		}
		for (int ii = 0; ii < 4; ii++) {
			AddEnemy(0, lw/2+lw/10*(ii+1), lh/10*(4-ii), "Chaser");
		}

		// waveset 2
		NewWave(100);
		for (int ii = 1; ii < 10; ii += 2) {
			AddEnemy(0.0f, ii*lw/10.0f, ii*lh/10.0f, "Shooter");
		}
		NewWave(100);
		for (int ii = 2; ii < 8; ii++) {
			AddEnemy(0.0f, ii*lw/9.0f-2, (9-ii)*lh/9.0f-2, "Chaser");
		}
		for (int ii = 2; ii < 8; ii++) {
			AddEnemy(0.0f, ii*lw/9.0f+2, (9-ii)*lh/9.0f+2, "Chaser");
		}
		NewWave(100);
		AddEnemy(0, 4, 4, "Twohitter");
		AddEnemy(0, lw-4, lh-4, "Twohitter");
		NewWave(100);
		AddEnemy(0, lw-4, 4, "Twohitter");
		AddEnemy(0, 4, lh-4, "Twohitter");
		NewWave(100);
		AddEnemy(0, lw-4, 4, "Twohitter");
		AddEnemy(0, 4, lh-4, "Twohitter");
		AddEnemy(0, 4, 4, "Twohitter");
		AddEnemy(0, lw-4, lh-4, "Twohitter");
		EndLevel();
	}
}
