using UnityEngine;
using System.Collections;

// only at most 1 attracter should ever be on screen

public class Attracter : qEnemy {
	public float maxAttraction;
	public float acceleration;
	public GameObject lightObject;
	private Vector3 velocity;

	protected override void qAwake () {
		lightObject.SetActive(false);
		base.qAwake();
	}
	
	protected override void qUpdate () {
		// TODO:inefficient and hacky, but works. should make qEnemy base class more flexible to 
		// allow for this though.
		if (isActive) lightObject.SetActive(true);

		// attract player
		Vector3 attractDirection = transform.position - player.transform.position;
		attractDirection.Normalize();
		attractDirection *= maxAttraction;
		player.attractDirection = attractDirection;

		// update position
		Vector3 direction = this.player.transform.position - this.transform.position;
		// this check prevents attracter from juking too much around player when he dies
		if (direction.magnitude < 0.2f) {
			direction = new Vector3(0,0,0);
		} else {
			direction.y = 0;
			direction.Normalize();
			velocity += direction*acceleration*Time.deltaTime;
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
		// -90 is because the model has a 90 degrees rotational offset
		transform.rotation = Quaternion.Euler(0, -90+Mathf.Rad2Deg*Mathf.Atan2(velocity.x, velocity.z) ,0);

	}

	protected override void qDie() {
		player.attractDirection = new Vector3(0,0,0);
		base.qDie();
	}
}