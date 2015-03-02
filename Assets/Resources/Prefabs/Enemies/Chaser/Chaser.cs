using UnityEngine;
using System.Collections;

public class Chaser : qEnemy {
	public float speed;

	private Player player;

	protected override void Awake () {
		base.Awake();
		this.player = (Player) FindObjectOfType(typeof(Player));
	}

	private void Update () {
		if (!isActive) return;
		Vector3 direction = this.player.transform.position - this.transform.position;
		direction.y = 0;
		direction.Normalize();
		direction *= speed;
		this.transform.Translate(direction*Time.deltaTime, Space.World);
	}
}
