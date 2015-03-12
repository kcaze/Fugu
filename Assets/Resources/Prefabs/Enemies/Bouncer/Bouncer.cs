using UnityEngine;
using System.Collections;

public class Bouncer : qEnemy {
	private Vector3 direction;
	private Vector3 previousPosition;

	protected override void qAwake () {
		base.qAwake();
		float angle = Mathf.Deg2Rad*(Mathf.FloorToInt(Random.Range(0, 4))*90 + 45);
		direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
		previousPosition = transform.position;
	}
	
	private void ReflectHorizontal() {
		direction.x *= -1;
	}

	private void ReflectVertical() {
		direction.z *= -1;
	}

	protected override void qUpdate () {
		//TODO: temporary code for animation
		transform.Rotate(new Vector3(0, 100f*Time.deltaTime, 0), Space.World);

		previousPosition = transform.position;
		// move
		transform.Translate(speed*direction*Time.deltaTime, Space.World);

		// bounce
		Vector3 position = transform.position;
		if (position.x < 0 || position.x > LevelManager.instance.levelWidth) {
			ReflectHorizontal();
		}
		if (position.z < 0 || position.z > LevelManager.instance.levelHeight) {
			ReflectVertical();
		}

		// clamp
		position.x = Mathf.Clamp(position.x, 0.001f, LevelManager.instance.levelWidth - 0.001f);
		position.z = Mathf.Clamp(position.z, 0.001f, LevelManager.instance.levelHeight - 0.001f);
		transform.position = position;	

	}

	private void OnTriggerEnter(Collider other) {
		if (!other.GetComponent<Bouncer>()) return;
		if (!other.GetComponent<qObject>().isActive) return;
		transform.position = previousPosition;
		Vector3 otherDirection = other.GetComponent<Bouncer>().direction;
		if (direction.x*otherDirection.x < 0) {
			Invoke("ReflectHorizontal", 0.01f); // need delay, otw the other bouncer won't reflect
		}
		if (direction.z*otherDirection.z < 0) {
			Invoke("ReflectVertical", 0.01f);
		}
	}
}
