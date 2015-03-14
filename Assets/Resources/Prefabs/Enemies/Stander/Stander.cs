using UnityEngine;
using System.Collections;

public class Stander : qEnemy {
	protected override void qAwake() {
		base.qAwake();
		float angle = Random.Range(0, 360);
		transform.rotation = Quaternion.Euler(0, angle, 0);
	}
}