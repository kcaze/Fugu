using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {
	private ParticleSystem ps;

	private void Awake () {
		ps = GetComponent<ParticleSystem>();
	}
	
	private void Update () {
		if(!ps.IsAlive()) {
			Destroy(gameObject);
		}
	}

}
