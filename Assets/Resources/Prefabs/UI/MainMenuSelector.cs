using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenuSelector : MonoBehaviour {
	public GameObject mainMenuSelectedObject;
	public GameObject levelSelectedObject;
	EventSystem eventSystem;

	public void Awake() {
		eventSystem = (EventSystem) FindObjectOfType(typeof(EventSystem));
		eventSystem.SetSelectedGameObject(mainMenuSelectedObject);
	}

	public void BackToMain () {
        transform.FindChild("LevelsView").gameObject.SetActive(false);
        transform.FindChild("Main").gameObject.SetActive(true);
		eventSystem.SetSelectedGameObject(mainMenuSelectedObject);
	}

	public void Levels () {
        transform.FindChild("Main").gameObject.SetActive(false);
        transform.FindChild("LevelsView").gameObject.SetActive(true);
		mainMenuSelectedObject = eventSystem.currentSelectedGameObject;
		eventSystem.SetSelectedGameObject(levelSelectedObject);
	}

	public void Quit() {
		Application.Quit();
	}
}
