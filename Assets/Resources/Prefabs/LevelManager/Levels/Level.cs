using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInfo {
	public float time;
	public Vector3 position;
	public string name;
	public bool spawned;
}

public class Level : MonoBehaviour {
	public List<List<EnemyInfo>> waves;
	private List<EnemyInfo> currentWave;

	public virtual void Setup() {
	}

	public void BeginLevel() {
		waves = new List<List<EnemyInfo>> ();
	}
	
	public void NewWave() {
		currentWave = new List<EnemyInfo> ();
		waves.Add(currentWave);
	}

	public void AddEnemy(float t, float x, float y, string s) {
		if (currentWave == null) {
			Debug.LogError("Must call NewWave() before AddEnemy()");
			return;
		}
		EnemyInfo enemyInfo = new EnemyInfo ();
		enemyInfo.time = t;
		enemyInfo.position = new Vector3(x, 1.0f, y);
		enemyInfo.name = s;
		enemyInfo.spawned = false;
		currentWave.Add(enemyInfo);
	}

	public void EndLevel() {
	}
}
