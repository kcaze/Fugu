using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _LevelManager : qObject {
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
	private UI ui;
	
	public void Pause() {
		Time.timeScale = 0;
		isActive = false;
		ui.Pause();
	}

	public void Unpause() {
		Time.timeScale = 1;
		isActive = true;
		ui.Unpause();
	}

	public override void HandleInput(string type, float val) {
		if (type == "Pause") {
			if (val != 0) {
				if (isActive) {
					Pause();
				} else {
					Unpause();
				}
			}
		}
	}

	protected override void qAwake() {
		ui = (UI) FindObjectOfType(typeof(UI));
		level = (Level) gameObject.AddComponent(levelName);
		level.Setup();
		time = 0;
		nextWaveTime = 0;
		waveNumber = -1;
		player = (Player) FindObjectOfType(typeof(Player));
		player.transform.position = new Vector3(levelWidth/2, player.transform.position.y, levelHeight/2);
	}

	protected override void qStart() {
		InputManager.instance.Subscribe(this);
	}

	protected override void qUpdate() {
		time += Time.deltaTime;

		GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
		bool currentWaveCleared = enemyObjects.Length == 0 ? true : false;

		// spawn next wave
		if (currentWaveCleared || time >= nextWaveTime) {
			waveNumber++;
			if (waveNumber >= level.waves.Count) {
				Victory();
			}
			else {
				time = nextWaveTime;
				nextWaveTime += level.waveTimes[waveNumber];
				for (int ii = 0; ii < level.waves[waveNumber].Count; ii++) {
					EnemyInfo enemyInfo = level.waves[waveNumber][ii];
					qEnemy enemy = (qEnemy) Resources.Load("Prefabs/Enemies/"+enemyInfo.name+"/"+enemyInfo.name, typeof(qEnemy));
					Instantiate(enemy, enemyInfo.position, Quaternion.identity);
				}
			}
		}
	}

	private void Victory() {
		Application.LoadLevel("Victory");
	}

	protected override void qDie() {
		lives--;
		nextWaveTime = time+level.waveTimes[waveNumber];
	}
}

public class LevelManager : qSingleton<_LevelManager> {
}