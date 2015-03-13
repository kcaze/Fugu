using UnityEngine;
using System.Collections;

public class ShooterBullet : qObject {
	public float speed;
	[System.NonSerialized]
	public Vector3 direction;

	protected override void qUpdate () {
		direction.y = 0;
		transform.position += direction*speed*Time.deltaTime;
		if (transform.position.x < 0 || transform.position.x > LevelManager.instance.levelWidth){
			Destroy(gameObject);
		}
		if (transform.position.z < 0 || transform.position.z > LevelManager.instance.levelHeight){
			Destroy(gameObject);
		}
	}
}
