using UnityEngine;
using System.Collections;

public class qEnemy : qObject {
	public int score;
	public bool isActive;

	protected virtual void Awake() {
		isActive = false;
		StartCoroutine(Activate());
	}

	private IEnumerator Activate() {
		yield return new WaitForSeconds(0.2f);
		isActive = true;
		yield return null;
	}

	public void Circled (TileEnum type) {
		if (type == TileEnum.normalCircled) {
			ScoreManager.instance.score += score;
			Destroy(gameObject);
		}
	}
}