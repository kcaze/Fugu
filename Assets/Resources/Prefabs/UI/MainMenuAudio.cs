using UnityEngine;
using System.Collections;

public class MainMenuAudio : MonoBehaviour {
	public AudioSource click;

	public void playClick() {
		click.Play();
	}
}
