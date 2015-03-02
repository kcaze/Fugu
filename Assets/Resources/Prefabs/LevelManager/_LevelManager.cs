using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _LevelManager : MonoBehaviour {
	public string levelName;
	public float levelWidth;
	public float levelHeight;
	[System.NonSerialized]
	public List<qEnemy> enemies;
	[System.NonSerialized]
	public Level level;
	public int waveNumber;

	private Player player;
	private float waveStartTime;
	private float waveEndTime;
	private float time;

	private void Awake() {
		waveStartTime = Time.time;
		level = (Level) gameObject.AddComponent(levelName);
		level.Setup();
		enemies = new List<qEnemy> ();
		player = (Player) FindObjectOfType(typeof(Player));
		player.transform.position = new Vector3(levelWidth/2, player.transform.position.y, levelHeight/2);
		waveNumber = 0;
	}

	private void Update() {
		time = Time.time - waveStartTime;
		// spawn enemies
		bool waveDoneSpawning = true;
		for (int ii = 0; ii < level.waves[waveNumber].Count; ii++) {
			EnemyInfo enemyInfo = level.waves[waveNumber][ii];
			if (!enemyInfo.spawned) waveDoneSpawning = false;
			if (!enemyInfo.spawned && time >= enemyInfo.time) {
				qEnemy enemy = (qEnemy) Resources.Load("Prefabs/Enemies/"+enemyInfo.name+"/"+enemyInfo.name, typeof(qEnemy));
				enemies.Add((qEnemy) Instantiate(enemy, enemyInfo.position, Quaternion.identity));
				enemyInfo.spawned = true;
			}
		}

		// check if ready to move on to next wave;
		if (waveDoneSpawning) {
			bool currentWaveCleared = true;
			for (int ii = 0; ii < enemies.Count; ii++) {
				if (enemies[ii] != null) currentWaveCleared = false;
			}
			if (currentWaveCleared) {
				waveStartTime = waveEndTime;
				waveNumber++;
				enemies = new List<qEnemy> ();
			}
		}
		waveEndTime = Time.time;
	}
}

public class LevelManager : qSingleton<_LevelManager> {
}