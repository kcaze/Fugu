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

		if (velocity.magnitude > speed) {
			velocity *= speed/velocity.magnitude;
		}

		this.transform.Translate(velocity*Time.deltaTime, Space.World);

		// update rotation
		transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg*Mathf.Atan2(velocity.x, velocity.z)+90 ,0);
	}
}
