using UnityEngine;
using System.Collections;

public class qEnemy : qObject {
	public int score;
	public Color bubbleColor;
	public float speed;
	protected Player player;
	protected float slow;

	protected virtual void Awake() {
		slow = 1.0f;
		player = (Player) FindObjectOfType(typeof(Player));
		isActive = false;
		StartCoroutine(Activate());
	}

	private IEnumerator Activate() {
		GameObject bubbles = Instantiate(Resources.Load("Prefabs/ParticleEffects/Bubbler")) as GameObject;
		ParticleSystem bubbleParticles = bubbles.GetComponent<ParticleSystem>();
		bubbles.transform.position = transform.position;
		bubbleParticles.startColor = bubbleColor;
		GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(1.0f);

		isActive = true;
		Destroy(bubbles);
		GetComponent<MeshRenderer>().enabled = true;
		yield return null;
	}

	public void Trailed (TileEnum type) {
		if (type == TileEnum.empty) {
			// reset other effects
			slow = 1.0f;
		}
		else if (type == TileEnum.slow) {
			slow = 0.5f;
		}
		else if (type == TileEnum.normalCircled) {
			ScoreManager.instance.score += score;
			Destroy(gameObject);
		}
	}
}