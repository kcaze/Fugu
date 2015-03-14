using UnityEngine;
using System.Collections;

public class qEnemy : qObject {
	public int score;
	public Color bubbleColor;
	public float speed;
	[System.NonSerialized]
	protected Player player;

	protected override void qAwake() {
		player = (Player) FindObjectOfType(typeof(Player));
		isActive = false;
		StartCoroutine(Activate());
	}

	protected override void qDie() {
		ScoreManager.instance.IncrementCombo();


		GameObject comboText = (GameObject)Instantiate(Resources.Load("Prefabs/Misc/ComboTextTest", typeof(GameObject)));
		comboText.GetComponent<TextMesh>().text = ""+ScoreManager.instance.combo;
		comboText.transform.position = transform.position+new Vector3(0, 0.5f, 0);


		ScoreManager.instance.AddScore(score);
		Instantiate(Resources.Load("Prefabs/Misc/Coin", typeof(GameObject)), transform.position, Quaternion.identity);
		GameObject explosion = Instantiate(Resources.Load("Prefabs/ParticleEffects/Explosion")) as GameObject;
		explosion.transform.position = transform.position;
		AudioManager.instance.playDeath();
		Destroy(gameObject);
	}

	private IEnumerator Activate() {
		GameObject bubbles = Instantiate(Resources.Load("Prefabs/ParticleEffects/Bubbler")) as GameObject;
		ParticleSystem bubbleParticles = bubbles.GetComponent<ParticleSystem>();
		bubbles.transform.position = transform.position;
		bubbleParticles.startColor = bubbleColor;
		bubbles.GetComponent<Light>().color = bubbleColor;

		GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(1.3f);

		isActive = true;
		Destroy(bubbles);
		GetComponent<MeshRenderer>().enabled = true;
		yield return null;
	}
}