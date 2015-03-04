using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInfo {
	public Vector3 position;
	public string name;
}

public class Level : MonoBehaviour {
	public List<List<EnemyInfo>> waves;
	public List<float> waveTimes;
	private List<EnemyInfo> currentWave;

	public virtual void Setup() {
	}

	public void BeginLevel() {
		waves = new List<List<EnemyInfo>> ();
		waveTimes = new List<float> ();
	}

	// time is duration for the new wave
	public void NewWave(float time) {
		currentWave = new List<EnemyInfo> ();
		waves.Add(currentWave);
		waveTimes.Add(time);
	}

	public void AddEnemy (float x, float y, string s) {
		if (currentWave == null) {
			Debug.LogError("Must call NewWave() before AddEnemy()");
			return;
		}
		EnemyInfo enemyInfo = new EnemyInfo ();
		enemyInfo.position = new Vector3(x, 1.0f, y);
		enemyInfo.name = s;
		currentWave.Add(enemyInfo);
	}

	public void EndLevel() {
	}
}
