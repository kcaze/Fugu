using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	public float followWidth;
	public float followHeight;
	private Player player;

	void Start () {
		this.player = (Player) FindObjectOfType(typeof(Player));
		Vector3 position = this.player.transform.position;
		position.y = transform.position.y;
		this.transform.position = position;
	}
	
	void LateUpdate () {
		Vector3 playerPosition = this.player.transform.position;
		Vector3 newPosition = new Vector3(0,transform.position.y,0);
		if (playerPosition.x > transform.position.x + followWidth) {
			newPosition.x = playerPosition.x - followWidth;
		} 
		else if (playerPosition.x < transform.position.x - followWidth) {
			newPosition.x = playerPosition.x + followWidth;
		}
		else {
			newPosition.x = transform.position.x;
		}

		if (playerPosition.z > transform.position.z + followHeight) {
			newPosition.z = playerPosition.z - followHeight;
		} 
		else if (playerPosition.z < transform.position.z - followHeight) {
			newPosition.z = playerPosition.z + followHeight;
		}
		else {
			newPosition.z = transform.position.z;
		}

		transform.position = newPosition;
	}
}
