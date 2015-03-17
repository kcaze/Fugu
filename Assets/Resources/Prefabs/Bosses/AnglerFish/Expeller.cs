using UnityEngine;
using System.Collections;

public class Expeller : qObject {
	public float speed;
	[System.NonSerialized]
	public Vector3 direction;
	public float acceleration;
	[System.NonSerialized]
	public Vector3 velocity;
	private Player player;

	protected override void qStart() {
		velocity = direction*speed;
		player = FindObjectOfType<Player>();
	}

	protected override void qUpdate () {
		direction = player.transform.position - transform.position;
		velocity += direction*acceleration*Time.deltaTime;
		velocity.y = 0;
		transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg*Mathf.Atan2(velocity.x, velocity.z), 0);

		transform.position += velocity*Time.deltaTime;
		if (transform.position.x < 0 || transform.position.x > LevelManager.instance.levelWidth){
			Destroy(gameObject);
		}
		if (transform.position.z < 0 || transform.position.z > LevelManager.instance.levelHeight){
			Destroy(gameObject);
		}
	}


	/*public float acceleration;
	[System.NonSerialized]
	public Vector3 velocity;
	private AnglerFish anglerFish;

	protected override void qAwake () {
		anglerFish = (AnglerFish) FindObjectOfType(typeof(AnglerFish));
		player = (Player) FindObjectOfType(typeof(Player));
	}

	protected override void qUpdate () {
		//Vector3 direction = anglerFish.transform.position - transform.position;
		Vector3 direction = player.transform.position - transform.position;
		velocity += acceleration*direction.normalized*Time.deltaTime;
		if (velocity.magnitude > speed) {
			velocity *= speed/velocity.magnitude;
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		for (int ii = 0; ii < enemies.Length; ii++) {
			if (enemies[ii].GetComponent<Expeller>() == null) continue;
			if (!enemies[ii].GetComponent<qEnemy>().isActive) continue;
			direction = enemies[ii].transform.position - this.transform.position;
			if (direction.magnitude > 1) continue;
			direction.y = 0;
			direction.Normalize();
			velocity -= direction*acceleration*Time.deltaTime;
		}

		transform.Translate(velocity*Time.deltaTime, Space.World);
		
		// clamp
		Vector3 position = transform.position;
		position.x = Mathf.Clamp(position.x, 0.001f, LevelManager.instance.levelWidth - 0.001f);
		position.z = Mathf.Clamp(position.z, 0.001f, LevelManager.instance.levelHeight - 0.001f);
		transform.position = position;

		// fix y coordinate
		Vector3 clampPosition = transform.position;
		clampPosition.y = 0.05f;
		transform.position = clampPosition;
	}*/
}
