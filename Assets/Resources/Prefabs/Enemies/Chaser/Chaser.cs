using UnityEngine;
using System.Collections;

public class Chaser : qEnemy {
	public float acceleration;
	private Vector3 velocity;

	protected override void qAwake () {
		base.qAwake();
		this.player = (Player) FindObjectOfType(typeof(Player));
		velocity = new Vector3(0,0,0);
	}

	protected override void qUpdate () {
		// update position
		Vector3 direction = this.player.transform.position - this.transform.position;
		direction.y = 0;
		direction.Normalize();
		velocity += direction*acceleration*Time.deltaTime;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		for (int ii = 0; ii < enemies.Length; ii++) {
			if (enemies[ii].GetComponent<Chaser>() == null) continue;
			if (!enemies[ii].GetComponent<qEnemy>().isActive) continue;
			direction = enemies[ii].transform.position - this.transform.position;
			if (direction.magnitude > 1) continue;
			direction.y = 0;
			direction.Normalize();
			velocity -= direction*acceleration*Time.deltaTime;
		}

		if (velocity.magnitude > speed) {
			velocity *= speed/velocity.magnitude;
		}

		this.transform.Translate(velocity*Time.deltaTime, Space.World);

		// clamp
		Vector3 position = transform.position;
		position.x = Mathf.Clamp(position.x, 0.001f, LevelManager.instance.levelWidth - 0.001f);
		position.z = Mathf.Clamp(position.z, 0.001f, LevelManager.instance.levelHeight - 0.001f);
		transform.position = position;

		// update rotation
		transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg*Mathf.Atan2(velocity.x, velocity.z) ,0);
	}
}