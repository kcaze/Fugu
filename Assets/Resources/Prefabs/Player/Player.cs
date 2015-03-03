using UnityEngine;
using System.Collections;

public enum TrailEnum {none, normal};

public class Player : qObject {
	public float maxSpeed;
	public float acceleration;
	public float friction;
	public float turnSpeed;
	public float lightIntensity;

	public TrailEnum trail { get; private set; }
	private float velocityHorizontal, velocityVertical;
	private float minSpeed = 1e-3f;
	private LevelManager levelManager;
	private Light light;

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
		else if (type == "QButtonDown" && val != 0) {
			if (trail == TrailEnum.normal) {
				trail = TrailEnum.none;
				light.intensity = 0;
			}
			else if (trail == TrailEnum.none) {
				trail = TrailEnum.normal;				
				light.intensity = lightIntensity;
			}
		}
	}

	private void Awake() {
		velocityHorizontal = 0;
		velocityVertical = 0;
		trail = TrailEnum.none;
		light = GetComponentInChildren<Light>();
		light.intensity = 0;
	}
	
	private void Start() {
		InputManager.instance.Subscribe(this);
	}
	
	private void Update() {
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
		if (Mathf.Abs(velocityHorizontal) != 0 || velocityVertical != 0) {
			float angle = 90+Mathf.Rad2Deg*Mathf.Atan2(velocityHorizontal, velocityVertical);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,angle,0), turnSpeed*Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "enemy") {
			qEnemy enemy = other.gameObject.GetComponent<qEnemy> ();
			if (enemy.isActive) {
				// restart level
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}	
}
