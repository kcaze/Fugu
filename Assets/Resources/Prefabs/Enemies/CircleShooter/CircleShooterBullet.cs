using UnityEngine;
using System.Collections;

public class CircleShooterBullet : qObject {
	public float speed;
	[System.NonSerialized]
	public Vector3 direction;

	protected override void qUpdate () {
		transform.position += direction*speed*Time.deltaTime;
		if (transform.position.x < 0 || transform.position.x > LevelManager.instance.levelWidth){
			Destroy(gameObject);
		}
		if (transform.position.z < 0 || transform.position.z > LevelManager.instance.levelHeight){
			Destroy(gameObject);
		}
	}

	protected override void qDie() {
		GetComponent<Renderer>().material.color = Color.white;
		StartCoroutine(Reverse());
	}

	private IEnumerator Reverse() {
		float originalSpeed = speed;
		for (int ii = 0; ii < 20; ii++) {
			speed -= originalSpeed/4;
			yield return new WaitForSeconds(0.05f);
		}
	}
}
