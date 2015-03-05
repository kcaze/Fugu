using UnityEngine;
using System.Collections;

public class qEnemy : qObject {
	public int score;
	public Color bubbleColor;
	public float speed;
	protected Player player;

	protected override void qAwake() {
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
}