using UnityEngine;
using System.Collections;

public class Standspawner : qEnemy {
	protected override void qAwake() {
		base.qAwake();
		float angle = Random.Range(0, 360);
		transform.rotation = Quaternion.Euler(0, angle, 0);
	}

	protected override void qDie() {
		for (int ii = 0; ii < 3; ii++) {
			GameObject swarmer = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/Swarmer/Swarmer", typeof(GameObject)));
			swarmer.transform.position = transform.position; 
			swarmer.transform.position += 0.5f*(new Vector3(Mathf.Cos(ii*2*Mathf.PI/3), 0, Mathf.Sin(ii*2*Mathf.PI/3)));
		}
		base.qDie();
	}
}
