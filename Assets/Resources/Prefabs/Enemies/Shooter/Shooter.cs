using UnityEngine;
using System.Collections;

public class Shooter : qEnemy {
	public float shootingPeriod;

	protected override void Awake () {
		base.Awake();
		InvokeRepeating("Shoot", 0.5f, shootingPeriod);
	}

	void Shoot() {
		if (!isActive) return;
		Vector3 direction = (player.transform.position-transform.position).normalized;
		GameObject bulletObject = Instantiate(Resources.Load("Prefabs/Enemies/Shooter/Bullet", typeof(GameObject))) as GameObject;
		Bullet bullet = bulletObject.GetComponent<Bullet> ();
		bulletObject.transform.position = transform.position;
		bullet.direction = direction;
	}
}
