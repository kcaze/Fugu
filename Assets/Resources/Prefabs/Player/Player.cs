using UnityEngine;
using System.Collections;

public enum TrailEnum {none, normal};

public class Player : qObject {
	public float maxTrailSpeed;
	public float maxNoTrailSpeed;
	public float acceleration;
	public float friction;
	public float turnSpeed;
	public float shieldTime;
	public int bombs;

	private float maxSpeed;
	public TrailEnum trail { get; private set; }
	[System.NonSerialized]
	public float velocityHorizontal, velocityVertical;
	[System.NonSerialized]
	public bool isInvulnerable;
	private float minSpeed = 1e-3f;
	private LevelManager levelManager;
	private float rotationSpeedThreshold = 0.01f; // minimum speed necessary before rotations happens
	private Shield shield;
	private new Light light;

	public override void HandleInput(string type, float val) {
		if (type == "AxisHorizontal") {
			if (val != 0) {
				velocityHorizontal += val*acceleration*Time.deltaTime;
			}
			else {
				velocityHorizontal *= friction;
			}
		}
		else if (type == "AxisVertical") {
			if (val != 0) {
				velocityVertical += val*acceleration*Time.deltaTime;
			}
			else {
				velocityVertical *= friction;
			}
		}
		else if (type == "TrailDown") {
			if (val == 0) return;
			trail = TrailEnum.normal;
			light.enabled = true;
			maxSpeed = maxTrailSpeed;
		}
		else if (type == "TrailUp") {
			if (val == 0) return;
			trail = TrailEnum.none;
			light.enabled = false;
			maxSpeed = maxNoTrailSpeed;
		}
		else if (type == "TrailClear") {
			if (val == 0) return;
			GridManager.instance.SendMessage("ClearNormal");
		}
		else if (type == "Bomb") {
			if (val == 0 || bombs <= 0) return;
			bombs--;
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
			for (int ii = 0; ii < enemies.Length; ii++) {
				if (enemies[ii].GetComponent<qObject>().isActive) {
					enemies[ii].SendMessage("qDie");
				}
			}
		}
	}

	protected override void qAwake() {
		shield = GetComponentInChildren<Shield>();
		shield.renderer.enabled = false;
		light = GetComponentInChildren<Light>();
		trail = TrailEnum.none;
		maxSpeed = maxNoTrailSpeed;
		light.enabled = false;
		Reset();
	}
	
	protected override void qStart() {
		InputManager.instance.Subscribe(this);
	}
	
	protected override void qUpdate() {
		// movement code
		float magnitude = Mathf.Sqrt(Mathf.Pow(velocityHorizontal,2) + Mathf.Pow(velocityVertical,2));
		if (magnitude > maxSpeed) {
			velocityHorizontal *= maxSpeed/magnitude;
			velocityVertical *= maxSpeed/magnitude;
		}
		if (Mathf.Abs(velocityHorizontal) < minSpeed) {
			velocityHorizontal = 0;
		}
		if (Mathf.Abs(velocityVertical) < minSpeed) {
			velocityVertical = 0;
		}
		Vector3 ds = new Vector3(velocityHorizontal*Time.deltaTime, 0, velocityVertical*Time.deltaTime);
		Vector3 position = transform.position + ds;
		position.x = Mathf.Clamp(position.x, 0, LevelManager.instance.levelWidth-0.001f);
		position.z = Mathf.Clamp(position.z, 0, LevelManager.instance.levelHeight-0.001f);
		transform.position = position;

		// rotation code
		if (Mathf.Abs(velocityHorizontal) > rotationSpeedThreshold || 
		    Mathf.Abs(velocityVertical) > rotationSpeedThreshold) {
			float angle = 90+Mathf.Rad2Deg*Mathf.Atan2(velocityHorizontal, velocityVertical);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,angle,0), turnSpeed*Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (!isActive) return;
		if (other.gameObject.tag == "enemy") { 
			if (!other.gameObject.GetComponent<qObject>().isActive || isInvulnerable) return;
			LevelManager.instance.SendMessage("qDie");
		} else if (other.gameObject.tag == "coin") {
			ScoreManager.instance.combo += 1;
			other.gameObject.SendMessage("qDie");
			AudioManager.instance.playCoin();
		}
	}

	private void Reset() {
		velocityHorizontal = 0;
		velocityVertical = 0;
		transform.position = new Vector3(LevelManager.instance.levelWidth/2, 
		                                 transform.position.y, 
		                                 LevelManager.instance.levelHeight/2);
	}

	private IEnumerator Death() {
		AudioManager.instance.playPlayerDeath();
		GameObject explosion = Instantiate(Resources.Load("Prefabs/ParticleEffects/PlayerExplosion")) as GameObject;
		explosion.transform.position = transform.position;
		isActive = false;
		renderer.enabled = false;
		yield return new WaitForSeconds(1.5f);
		renderer.enabled = true;
		isActive = true;
		Reset();
		isInvulnerable = true;
		StartCoroutine(becomeVulnerable());
		shield.Activate(shieldTime);

	}

	protected override void qDie() {
		StartCoroutine(Death());
	}

	private IEnumerator becomeVulnerable() {
		yield return new WaitForSeconds(shieldTime);
		isInvulnerable = false;
	}
}
