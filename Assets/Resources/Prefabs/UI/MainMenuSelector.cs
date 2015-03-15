using UnityEngine;
using System.Collections;

public class MainMenuSelector : MonoBehaviour {
    
	// Use this for initialization
	public void BackToMain () {
        transform.FindChild("Levels").gameObject.SetActive(false);
        Debug.Log("Main");
        transform.FindChild("Main").gameObject.SetActive(true);
	}
	// Update is called once per frame
	public void Levels () {
        transform.FindChild("Main").gameObject.SetActive(false);
        Debug.Log("Level");
        transform.FindChild("Levels").gameObject.SetActive(true);
	}
}
