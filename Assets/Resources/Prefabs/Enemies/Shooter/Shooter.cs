using UnityEngine;
using System.Collections;

public class Shooter : qEnemy {
	public float shootingPeriod;
	public GameObject shell;

	protected override void qAwake () {
		shell.renderer.enabled = false;	
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

	protected override void qUpdate() {
		// hacky thing to do for animated shell
		if (shell && isActive) shell.renderer.enabled = true;

		Vector3 direction = (player.transform.position-transform.position).normalized;
		transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg*Mathf.Atan2(direction.x, direction.z) ,0);
	}
}
