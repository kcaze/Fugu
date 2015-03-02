using UnityEngine;
using System.Collections;

public class qPersistent : MonoBehaviour {
	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}
}
