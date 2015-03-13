using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	public Vector3 direction;
	private void Update() {
		transform.Translate(direction*10*Time.deltaTime, Space.World);
		if (transform.position.x < 0 || transform.position.x > GridManager.instance.grid.gridWidth || transform.position.z < 0 || transform.position.z > GridManager.instance.grid.gridHeight) Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "enemy") { 
			if (!other.gameObject.GetComponent<qObject>().isActive) return;
			if (!other.gameObject.GetComponent<qEnemy>()) return;
			other.gameObject.GetComponent<qEnemy>().SendMessage("qDie");
			Destroy(gameObject);
		} 
	}
}
