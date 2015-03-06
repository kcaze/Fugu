using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	Quaternion rotation;
	void Awake () {
		rotation = transform.rotation;
	}

	void LateUpdate () {
		transform.rotation = rotation;
	}

	public void Activate(float time) {
		StartCoroutine(ShieldAnimate(time));
	}

	private IEnumerator ShieldAnimate(float time) {
		GetComponent<MeshRenderer>().enabled = true;
		yield return new WaitForSeconds(time);
		float t = 0.3f;
		for (int ii = 0; ii < 10; ii++) {
			GetComponent<MeshRenderer>().enabled = false;
			yield return new WaitForSeconds(0.05f);
			GetComponent<MeshRenderer>().enabled = true;
			yield return new WaitForSeconds(t);
			t *= 0.8f;
		}
		GetComponent<MeshRenderer>().enabled = false;
	}
}
