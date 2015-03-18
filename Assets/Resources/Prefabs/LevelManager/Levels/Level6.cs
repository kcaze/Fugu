using UnityEngine;
using System.Collections;

public class Level6 : Level {
	public override void Setup() {
		lw = 30;
		lh = 20;
		BeginLevel();
		NewWave(1000);
		AddEnemy(0, lw/2, lh/2, "Stander");
		NewWave(1000);
		AddEnemy(0, lw/2, lh/2, "Stander");
		NewWave(1000);
		AddEnemy(0, lw/2, lh/2, "Chaser");
		NewWave(1000);
		AddEnemy(0, lw/2, lh/2, "Standspawner");
		NewWave(1000);
		AddEnemy(0, lw/2, lh/2, "Twohitter");
		EndLevel();
	}
}
