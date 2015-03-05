using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _LevelManager : MonoBehaviour {
	public string levelName;
	public float levelWidth;
	public float levelHeight;
	public int lives;
	[System.NonSerialized]
	public Level level;
	[System.NonSerialized]
	public int waveNumber;

	private Player player;
	private float time;
	private float previousWaveTime;
	private float nextWaveTime;

	private void Awake() {
		level = (Level) gameObject.AddComponent(levelName);
		level.Setup();
		time = 0;
		nextWaveTime = 0;
		waveNumber = -1;
		player = (Player) FindObjectOfType(typeof(Player));
		player.transform.position = new Vector3(levelWidth/2, player.transform.position.y, levelHeight/2);
	}

	private void Update() {
		time += Time.deltaTime;

		GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
		bool currentWaveCleared = enemyObjects.Length == 0 ? true : false;

		// spawn next wave
		if (currentWaveCleared || time >= nextWaveTime) {
			waveNumber++;
			time = nextWaveTime;
			nextWaveTime += level.waveTimes[waveNumber];
			for (int ii = 0; ii < level.waves[waveNumber].Count; ii++) {
				EnemyInfo enemyInfo = level.waves[waveNumber][ii];
				qEnemy enemy = (qEnemy) Resources.Load("Prefabs/Enemies/"+enemyInfo.name+"/"+enemyInfo.name, typeof(qEnemy));
				Instantiate(enemy, enemyInfo.position, Quaternion.identity);
			}
		}
	}

	public void Die() {
		lives--;
		player.Awake();
		nextWaveTime = time+level.waveTimes[waveNumber];
	}
}

public class LevelManager : qSingleton<_LevelManager> {
}