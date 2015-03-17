using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UICanvas : MonoBehaviour {
    public float levelprogress;
	public GameObject resumeButton;
    public GameObject pausescreen;
	EventSystem eventSystem;
    public Texture txmeter, txpointer;
    int n_lives;
    
	void Awake() {
		eventSystem = (EventSystem) FindObjectOfType(typeof(EventSystem));
	}
      
    void Start() {
        pausescreen.SetActive(false);
        n_lives = 3;
    }
	
    void Update() {
        int lives = LevelManager.instance.lives;
        if (n_lives != lives && n_lives > 0) transform.FindChild(n_lives.ToString()).gameObject.SetActive(false);
        n_lives = lives;
    }

	public void Pause() {
		pausescreen.SetActive(true);
		eventSystem.SetSelectedGameObject(resumeButton);
	}

	public void Unpause() {
		pausescreen.SetActive(false);
	}

    public void ReturnToMainMenu() {
		LevelManager.instance.Unpause();
        Application.LoadLevel("Main Menu");
    }

    public void Retry() {
		LevelManager.instance.Unpause();
        Application.LoadLevel(Application.loadedLevel);
    }

	public void Resume() {
		LevelManager.instance.Unpause();
	}
}
