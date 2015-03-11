using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	public float followWidth;
	public float followHeight;
	private Player player;

	void Start () {
		this.player = (Player) FindObjectOfType(typeof(Player));
		Vector3 position = new Vector3(0,0,0);
		position.y = transform.position.y;
		position.x = LevelManager.instance.levelWidth/2;
		position.z = LevelManager.instance.levelHeight/2;
		this.transform.position = position;
	}
	
	void LateUpdate () {
		float playerX = player.transform.position.x;
		float playerY = player.transform.position.z;
		float ratioX = 2*playerX/LevelManager.instance.levelWidth - 1;
		float ratioY = 2*playerY/LevelManager.instance.levelHeight - 1;

		Vector3 position = new Vector3(0,0,0);
		position.y = transform.position.y;
		position.x = LevelManager.instance.levelWidth/2 + ratioX*followWidth;
		position.z = LevelManager.instance.levelHeight/2 + ratioY*followHeight;
		this.transform.position = position;
		this.transform.rotation = Quaternion.identity;
		this.transform.Rotate(new Vector3(90-ratioY*10, 0, 0), Space.World);
		this.transform.Rotate(new Vector3(0, 0, -ratioX*10), Space.World);
	}
}
