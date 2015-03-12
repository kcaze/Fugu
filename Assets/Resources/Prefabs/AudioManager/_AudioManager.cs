using UnityEngine;
using System.Collections;

public class _AudioManager : MonoBehaviour {
	public AudioSource circled;
	public AudioSource enemyDeath;
	public AudioSource coin;

	public void playCircled () {
		circled.Play();
	}

	public void playDeath () {
		enemyDeath.Play();
	}

	public void playCoin() {
		coin.Play();
	}
}

public class AudioManager : qSingleton<_AudioManager> {
}