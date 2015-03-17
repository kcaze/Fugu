using UnityEngine;
using System.Collections;

public class Boss1 : Level {
	public override void Setup() {
		lw = 30;
		lh = 20;
		BeginLevel();
		NewWave(1000);
		AddEnemy(0, 3*lw/4, 3*lh/4, "BOSSAnglerFish");
		EndLevel();
	}
}
