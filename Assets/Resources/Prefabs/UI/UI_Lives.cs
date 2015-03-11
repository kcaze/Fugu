using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Lives : MonoBehaviour {
    private UICanvas canvas;
    private int prevlives;

	private void Awake() {
		//lives = GetComponent<Text>();
        prevlives = 3;
        canvas = GetComponent<UICanvas>();
	}

	private void Update() {
        int lives = LevelManager.instance.lives;
        if (prevlives == lives) return;
		canvas.n_lives = lives;
	}
}
