using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	private Player player;

	void Start () {
		this.player = (Player) FindObjectOfType(typeof(Player));
		Vector3 position = this.player.transform.position;
		position.y = transform.position.y;
		this.transform.position = position;
	}
	
	void Update () {
		Vector3 position = this.player.transform.position;
		position.y = transform.position.y;
		this.transform.position = position;
	}
}
