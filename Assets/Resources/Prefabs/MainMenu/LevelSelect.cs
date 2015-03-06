using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	public void LoadTutorial() {
		Application.LoadLevel("Tutorial");
	}
	public void LoadLevel1() {
		Application.LoadLevel("Level1");
	}
	public void LoadLevel2() {
		Application.LoadLevel("Level2");
	}
}
