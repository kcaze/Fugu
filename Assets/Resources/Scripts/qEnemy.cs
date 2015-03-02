using UnityEngine;
using System.Collections;

public class qEnemy : qObject {
	public int score;
	[System.NonSerialized]
	public bool isActive;

	protected virtual void Awake() {
		isActive = false;
		StartCoroutine(Activate());
	}

	private IEnumerator Activate() {
		Instantiate(Resources.Load("Prefabs/ParticleEffects/Bubbler"), transform.position, Quaternion.identity);
		GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(2.0f);
		isActive = true;
		GetComponent<MeshRenderer>().enabled = true;
		yield return null;
	}

	public void Circled (TileEnum type) {
		if (type == TileEnum.normalCircled) {
			ScoreManager.instance.score += score;
			Destroy(gameObject);
		}
	}
}