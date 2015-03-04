using UnityEngine;
using System.Collections;

public class Avoider : qEnemy {
	public float avoidRadius;
	private float velocityHorizontal, velocityVertical;

	protected override void Awake () {
		base.Awake();
		velocityHorizontal = 0;
		velocityVertical = 0;
	}
	
	void Update () {
		if (!isActive) return;

		Vector3 playerDir = player.transform.position-transform.position;
		Vector3 direction = new Vector3(0,0,0);

		if (playerDir.magnitude < avoidRadius) {
			direction = -playerDir.normalized;
		}
		else if (playerDir.magnitude > avoidRadius+0.1){
			direction = playerDir.normalized;
		}

		velocityHorizontal = speed*direction.x;
		velocityVertical = speed*direction.z;


		Vector3 ds = new Vector3(velocityHorizontal, 0, velocityVertical);
		transform.Translate(ds*Time.deltaTime, Space.World);

		Vector3 position = transform.position;
		position.x = Mathf.Clamp(position.x, 0, LevelManager.instance.levelWidth);
		position.z = Mathf.Clamp(position.z, 0, LevelManager.instance.levelHeight);

		transform.position = position;
	}
}
