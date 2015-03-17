using UnityEngine;
using System.Collections;

public class BOSSAnglerFish : qEnemy {
	protected override void qAwake() {
		GameObject anglerFish = (GameObject) Resources.Load("Prefabs/Bosses/AnglerFish/AnglerFish", typeof(GameObject));
		Instantiate(anglerFish, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}