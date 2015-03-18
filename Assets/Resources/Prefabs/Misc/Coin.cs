using UnityEngine;
using System.Collections;

public class Coin : qObject {
	public float maxSpeed;
	public float attractConstant;
	public float attractRadius;
	public float friction;
	private Vector3 velocity;
	private Player player;

	protected override void qAwake () {
		StartCoroutine(Animate(6.0f));
		player = (Player) FindObjectOfType(typeof(Player));
		Vector2 vel2d = (Random.insideUnitCircle).normalized;
		velocity = new Vector3(vel2d.x, 0, vel2d.y);
	}

	protected override void qUpdate() {
		Vector3 direction = player.transform.position - transform.position;
		if (direction.magnitude <= attractRadius) { 
			float speed; 
			speed = Mathf.Min(attractConstant/direction.magnitude, maxSpeed);
			direction.Normalize();
			velocity = speed*direction;
			StartCoroutine(CollidePlayer());
		}
		transform.position += velocity*Time.deltaTime;
		velocity *= friction;
	}

	IEnumerator CollidePlayer() {
		yield return new WaitForSeconds(0.05f);
		transform.position = player.transform.position;
	}

	private IEnumerator Animate(float time) {
		GetComponent<MeshRenderer>().enabled = true;
		yield return new WaitForSeconds(time-2.2f); // 2.2 is approximately the flicker time
		float t = 0.3f;
		for (int ii = 0; ii < 10; ii++) {
			GetComponent<MeshRenderer>().enabled = false;
			yield return new WaitForSeconds(0.05f);
			GetComponent<MeshRenderer>().enabled = true;
			yield return new WaitForSeconds(t);
			t *= 0.8f;
		}
		GetComponent<MeshRenderer>().enabled = false;
		Destroy(gameObject);
	}
}
