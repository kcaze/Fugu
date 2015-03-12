using UnityEngine;
using System.Collections;

public class Level1 : Level {
	public override void Setup() {
		lw = 30;
		lh = 20;
		BeginLevel();
		NewWave(120);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, 5+4*Mathf.Cos(Mathf.PI/2 + 2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(Mathf.PI/2+2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(120);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw-5+4*Mathf.Cos(2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(5);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, 5+4*Mathf.Cos(Mathf.PI/2 + 2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(Mathf.PI/2+2*Mathf.PI/20*ii), "Chaser");
		}
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, lw-5+4*Mathf.Cos(2*Mathf.PI/20*ii), lh-5+4*Mathf.Sin(2*Mathf.PI/20*ii), "Chaser");
		}
		NewWave(5);
		AddEnemy(0, 1.5f, 1.5f, "Shooter");
		AddEnemy(0, lw-1.5f, 1.5f, "Shooter");
		NewWave(120);
		AddEnemy(0, 1.5f, lh-1.5f, "Shooter");
		AddEnemy(0, lw-1.5f, lh-1.5f, "Shooter");
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0, (ii+1)*lw/6, 2, "Chaser");
		}
		NewWave(10);
		for (int ii = 0; ii < 3; ii++) {
			AddEnemy(0.0f, lw/2+5*Mathf.Cos(Mathf.PI*2*ii/3), lh/2+5*Mathf.Sin(Mathf.PI*2*ii/3), "Bouncer");
		}
		NewWave(5);
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(0.0f, lw/2+5*Mathf.Cos(Mathf.PI*2*ii/6), lh/2+5*Mathf.Sin(Mathf.PI*2*ii/6), "Bouncer");
		}
		NewWave(8);
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(0.0f, lw/2+5*Mathf.Cos(Mathf.PI*2*(ii+0.5f)/6), lh/2+5*Mathf.Sin(Mathf.PI*2*(ii+0.5f)/6), "Bouncer");
		}
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(2.5f, lw/2+5*Mathf.Cos(Mathf.PI*2*ii/6), lh/2+5*Mathf.Sin(Mathf.PI*2*ii/6), "Bouncer");
		}
		NewWave(5);
		for (int ii = 1; ii < 9; ii++) {
			AddEnemy(0.0f, ii*lw/9.0f, ii*lh/9.0f, "Shooter");
		}
		NewWave(120);
		for (int ii = 1; ii < 9; ii++) {
			AddEnemy(0.0f, ii*lw/9.0f, (9-ii)*lh/9.0f, "Shooter");
		}
		NewWave(5);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0.0f, 5, lh*(ii+1)/6.0f, "Shooter");
		}
		NewWave(5);
		for (int ii = 0; ii < 5; ii++) {
			AddEnemy(0.0f, lw-5, lh*(ii+1)/6.0f, "Shooter");
		}
		NewWave(120);
		for (int ii = 0; ii < 5; ii++) {
			for (int jj = 0; jj < 5; jj++) {
				AddEnemy(3.0f*ii, lw/2+3*Mathf.Cos((ii%2)*Mathf.PI+Mathf.PI*jj/4), lh/2+3*Mathf.Sin((ii%2)*Mathf.PI+Mathf.PI*jj/4), (ii%2 == 0) ? "Bouncer" : "Chaser");
			}
		}

		NewWave(120);
		for (int ii = 0; ii < 6; ii++) {
			AddEnemy(0, lw/2+(ii-2.5f)*3, lh/2-3, "Stander");
			AddEnemy(0, lw/2+(ii-2.5f)*3, lh/2+3, "Stander");
			AddEnemy(0, lw/2-3, lh/2+(ii-2.5f)*3, "Stander");
			AddEnemy(0, lw/2+3, lh/2+(ii-2.5f)*3, "Stander");
		}

		/*for (int ii = 0; ii < 10; ii++) {
			AddEnemy(1.5f*(ii+1), lw/2-3f, lh/2-3f, "Chaser");
			AddEnemy(1.5f*(ii+1), lw/2+3f, lh/2-3f, "Chaser");
			AddEnemy(1.5f*(ii+1), lw/2-3f, lh/2+3f, "Chaser");
			AddEnemy(1.5f*(ii+1), lw/2+3f, lh/2+3f, "Chaser");
		}*/


		EndLevel();
	}
}
