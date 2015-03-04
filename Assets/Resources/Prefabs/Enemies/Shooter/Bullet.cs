using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;
	[System.NonSerialized]
	public Vector3 direction;

	void Update () {
		transform.position += direction*speed*Time.deltaTime;
		if (transform.position.x < 0 || transform.position.x > LevelManager.instance.levelWidth){
			Destroy(gameObject);
		}
		if (transform.position.z < 0 || transform.position.z > LevelManager.instance.levelHeight){
			Destroy(gameObject);
		}
	}
}
