using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {
	public GameObject pauseBackdrop;
	public GameObject resumeButton;
	public GameObject exitButton;
	public GameObject retryButton;

	private void Awake () {
		Unpause();
	}

	public void ReturnToMainMenu() {
		Application.LoadLevel("Main Menu");
	}

	public void Retry() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Pause () {
		pauseBackdrop.SetActive(true);
		resumeButton.SetActive(true);
		retryButton.SetActive(true);
		exitButton.SetActive(true);
	}
	
	public void Unpause () {
		pauseBackdrop.SetActive(false);
		resumeButton.SetActive(false);
		retryButton.SetActive(false);
		exitButton.SetActive(false);
	}
}
