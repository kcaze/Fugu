using UnityEngine;
using System.Collections;

public class qObject : MonoBehaviour {
	[System.NonSerialized]
	public bool isActive;

	public virtual void HandleInput(string type, float val) {
	}

	private void Start() {
	}
	
	private void Update() {	
	}

}
