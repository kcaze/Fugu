using UnityEngine;
using System.Collections;

public class qObject : MonoBehaviour {
	[System.NonSerialized]
	public bool isActive;

	public virtual void HandleInput(string type, float val) {
	}

	protected virtual void qAwake() {
	}

	protected virtual void qStart() {
	}

	protected virtual void qUpdate() {
	}

	private void Awake() {
		isActive = true;
		qAwake();
	}

	private void Start() {
		qStart();
	}

	private void Update() {
		if (isActive) {
			qUpdate();
		}
	}
}
