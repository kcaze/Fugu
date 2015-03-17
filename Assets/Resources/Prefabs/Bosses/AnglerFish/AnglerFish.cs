using UnityEngine;
using System.Collections;

public class AnglerFish : qEnemy {
	enum State { charging, aiming };
	public float acceleration;
	public float aimDuration;
	public float shootDuration;
	private float scale;
	private Vector3 velocity;
	private float aimCounter;
	private float shootCounter;
	private State state;
	private new Light light;

	protected override void qAwake () {
		this.player = (Player) FindObjectOfType(typeof(Player));
		state = State.aiming;
		scale = 8;
		light = GetComponentInChildren<Light>();
		transform.localScale = new Vector3(scale, scale, scale);
		velocity = new Vector3(0,0,0);
	}
	
	protected override void qUpdate () {

		if (state == State.charging) {
			//renderer.material.color = Color.Lerp(renderer.material.color, Color.white, 0.1f);
			light.intensity = Mathf.Lerp(light.intensity, 8, 0.4f);

			this.transform.Translate(velocity*Time.deltaTime, Space.World);

			if (transform.position.x < 0 || transform.position.x > LevelManager.instance.levelWidth ||
			    transform.position.z < 0 || transform.position.z > LevelManager.instance.levelHeight) {
				state = State.aiming;
			}

			// clamp
			Vector3 position = transform.position;
			position.x = Mathf.Clamp(position.x, 0.001f, LevelManager.instance.levelWidth - 0.001f);
			position.z = Mathf.Clamp(position.z, 0.001f, LevelManager.instance.levelHeight - 0.001f);
			transform.position = position;

			// update rotation
			transform.rotation = Quaternion.Euler(0, -90+Mathf.Rad2Deg*Mathf.Atan2(velocity.x, velocity.z) ,0);
		} else if (state == State.aiming) {
			Vector3 direction = player.transform.position - transform.position;
			//renderer.material.color = Color.Lerp(renderer.material.color, Color.black, 0.1f);
			light.intensity = Mathf.Lerp(light.intensity, 0, 0.1f);
			float angle = -90+Mathf.Rad2Deg*Mathf.Atan2(direction.x, direction.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), 0.1f);

			aimCounter += Time.deltaTime;
			shootCounter += Time.deltaTime;
			if (aimCounter >= aimDuration) {
				if (direction.magnitude < 15) {
					aimCounter = 0;
					state = State.charging;
					velocity = speed*direction.normalized;
					AudioManager.instance.playGrowl2();
				}
			}
			if (shootCounter >= shootDuration) {
				shootCounter = 0;
				Shoot();
			}
		} 
		// keep y-coordinate fixed
		Vector3 clampPosition = transform.position;
		clampPosition.y = 0.05f;
		transform.position = clampPosition;
	}

	protected override void qDie() {
		if (scale <= 2) {
			base.qDie();
		}
		else {
			GameObject explosion = Instantiate(Resources.Load("Prefabs/ParticleEffects/Explosion")) as GameObject;
			explosion.transform.position = transform.position;
			AudioManager.instance.playDeath();
			scale -= 1;
			light.range = scale*1.5f;
			transform.localScale = new Vector3(scale, scale, scale);
		}
	}

	void Shoot() {
		Vector3 playerDirection = player.transform.position - transform.position;
		float angle = -Mathf.PI/2 + Mathf.Atan2(playerDirection.x, playerDirection.z);
		Vector3[] directions = new Vector3[3];
		directions[0] = new Vector3(Mathf.Cos(angle), 0, -Mathf.Sin(angle));
		directions[1] = new Vector3(Mathf.Cos(angle+0.15f), 0, -Mathf.Sin(angle+0.15f));
		directions[2] = new Vector3(Mathf.Cos(angle-0.15f), 0, -Mathf.Sin(angle-0.15f));
		for (int ii = 0; ii < 3; ii++) {
			GameObject expellerObject = (GameObject) Instantiate(Resources.Load("Prefabs/Bosses/AnglerFish/Expeller", typeof(GameObject)), transform.position, Quaternion.identity);
			expellerObject.GetComponent<Expeller>().direction = directions[ii];
		}
	}
}