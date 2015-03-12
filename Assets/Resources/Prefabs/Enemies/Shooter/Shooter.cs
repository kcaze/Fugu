using UnityEngine;
using System.Collections;

public class Shooter : qEnemy {
	public float shootingPeriod;

	protected override void qAwake () {
		base.qAwake();
		InvokeRepeating("Shoot", 0.0f, shootingPeriod);
	}

	private void Shoot() {
		if (!isActive) return;
		Vector3 direction = (player.transform.position-transform.position).normalized;
		GameObject bulletObject = Instantiate(Resources.Load("Prefabs/Enemies/Shooter/ShooterBullet", typeof(GameObject))) as GameObject;
		ShooterBullet bullet = bulletObject.GetComponent<ShooterBullet> ();
		bulletObject.transform.position = transform.position;
		bullet.direction = direction;
	}
}
