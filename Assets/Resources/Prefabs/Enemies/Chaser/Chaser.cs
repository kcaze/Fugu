using UnityEngine;
using System.Collections;

public class Chaser : qEnemy {
	protected override void qAwake () {
		base.qAwake();
		this.player = (Player) FindObjectOfType(typeof(Player));
	}

	protected override void qUpdate () {
		// update position
		Vector3 direction = this.player.transform.position - this.transform.position;
		direction.y = 0;
		direction.Normalize();
		direction *= speed*slow;
		this.transform.Translate(direction*Time.deltaTime, Space.World);

		// update rotation
		transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg*Mathf.Atan2(direction.x, direction.z)+90 ,0);
	}
}
