using UnityEngine;
using System.Collections;

public class Twohitter : qEnemy {
	public float acceleration;
	public GameObject shell;
	private Vector3 velocity;
	private bool killable = false;
	
	protected override void qAwake () {
		shell.renderer.enabled = false;
		base.qAwake();
		this.player = (Player) FindObjectOfType(typeof(Player));
		velocity = new Vector3(0,0,0);
	}
	
	protected override void qUpdate () {
		// hacky thing to do for shell
		if (shell && isActive) shell.renderer.enabled = true;

		// update position
		Vector3 direction = this.player.transform.position - this.transform.position;
		direction.y = 0;
		direction.Normalize();
		velocity += direction*acceleration*Time.deltaTime;
		
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		for (int ii = 0; ii < enemies.Length; ii++) {
			if (enemies[ii].GetComponent<Twohitter>() == null) continue;
			if (!enemies[ii].GetComponent<qEnemy>().isActive) continue;
			direction = enemies[ii].transform.position - this.transform.position;
			if (direction.magnitude > 1) continue;
			direction.y = 0;
			direction.Normalize();
			velocity -= direction*acceleration*Time.deltaTime;
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
		transform.rotation = Quaternion.Euler(0, 90+Mathf.Rad2Deg*Mathf.Atan2(velocity.x, velocity.z) ,0);
	}

	protected override void qDie() {
		if (killable == false) {
			killable = true;
			Destroy(shell);
			speed *= 1.4f;
			GameObject explosion = Instantiate(Resources.Load("Prefabs/ParticleEffects/Explosion")) as GameObject;
			explosion.transform.position = transform.position;
			AudioManager.instance.playDeath();
		}
		else {
			base.qDie();
		}
	}
}
