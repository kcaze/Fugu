using UnityEngine;
using System.Collections;

public class ComboTextTest : MonoBehaviour {
	public float textDuration;

	private IEnumerator Destroy() {
		yield return new WaitForSeconds(1.3f);
		Destroy(gameObject);
	}

	void Start () {
		StartCoroutine(Destroy());
	}
}
