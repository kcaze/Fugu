using UnityEngine;
using System.Collections;

public class Wanderer : qEnemy {
	public float speed;

	private Vector3 direction;

	protected override void Awake () {
		base.Awake();
		float angle = Random.Range(0, 2*Mathf.PI);
		direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
	}
	
	private void ReflectHorizontal() {
		direction.x *= -1;
	}
	private void ReflectVertical() {
		direction.z *= -1;
	}

	private void Update () {
		if (!isActive) return;

		//TODO: temporary code for animation
		transform.Rotate(new Vector3(0, 100f*Time.deltaTime, 0), Space.World);

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
}
