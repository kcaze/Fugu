using UnityEngine;
using System.Collections;

public enum TrailEnum {none, normal};

public class Player : qObject {
	public float maxSpeed;
	public float acceleration;
	public float friction;
	public float turnSpeed;
	public float shieldTime;
	
	public TrailEnum trail { get; private set; }
	[System.NonSerialized]
	public float velocityHorizontal, velocityVertical;
	private float minSpeed = 1e-3f;
	private LevelManager levelManager;
	private float rotationSpeedThreshold = 0.01f; // minimum speed necessary before rotations happens
	private bool isInvulnerable;
	private Shield shield;

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
	}

	protected override void qAwake() {
		shield = GetComponentInChildren<Shield>();
		qDie();
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
		if (other.gameObject.tag == "enemy" && 
		    other.gameObject.GetComponent<qObject>().isActive &&
		    !isInvulnerable) {
			qDie();
			LevelManager.instance.SendMessage("qDie");
		}
	}

	protected override void qDie() {
		velocityHorizontal = 0;
		velocityVertical = 0;
		isInvulnerable = true;
		StartCoroutine(becomeVulnerable());
		shield.Activate(shieldTime);
		transform.position = new Vector3(LevelManager.instance.levelWidth/2, 
		                                 transform.position.y, 
		                                 LevelManager.instance.levelHeight/2);
	}

	private IEnumerator becomeVulnerable() {
		yield return new WaitForSeconds(shieldTime);
		isInvulnerable = false;
	}
}
