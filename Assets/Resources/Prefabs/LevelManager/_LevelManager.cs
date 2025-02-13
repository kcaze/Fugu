﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _LevelManager : qObject {
	public string levelName;
	[System.NonSerialized]
	public int levelWidth;
	[System.NonSerialized]
	public int levelHeight;
	public int lives;
	[System.NonSerialized]
	public Level level;
	[System.NonSerialized]
	public int waveNumber;

	private Player player;
	private float time;
	private float waveBeginTime;
	private float nextWaveTime;
	private bool waveDoneSpawning;
	private UICanvas uiCanvas; 

	public void Pause() {
		Time.timeScale = 0;
		isActive = false;
		uiCanvas.Pause();
	}

	public void Unpause() {
		Time.timeScale = 1;
		isActive = true;
		uiCanvas.Unpause();
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
		uiCanvas = (UICanvas) FindObjectOfType(typeof(UICanvas));
		level = (Level) gameObject.AddComponent(levelName);
		level.Setup();
		levelWidth = level.lw;
		levelHeight = level.lh;
		nextWaveTime = level.waveTimes[0];
		waveNumber = 0;
		player = (Player) FindObjectOfType(typeof(Player));
		player.transform.position = new Vector3(levelWidth/2, player.transform.position.y, levelHeight/2);
	}

	protected override void qStart() {
		InputManager.instance.Subscribe(this);
	}

	protected override void qUpdate() {
		if (!isActive) return;
		time += Time.deltaTime;

		// spawn enemies
		waveDoneSpawning = true;
		for (int ii = 0; ii < level.waves[waveNumber].Count; ii++) {
			EnemyInfo enemyInfo = level.waves[waveNumber][ii];
			if (enemyInfo.spawned) continue;
			waveDoneSpawning = false;
			if (time-waveBeginTime > enemyInfo.time) {
				Object enemy = Resources.Load(enemyInfo.path, typeof(GameObject));
				Instantiate(enemy, enemyInfo.position, Quaternion.identity);
				enemyInfo.spawned = true;
			}
		}

		GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
		bool currentWaveCleared = true;
		for (int ii = 0; ii < enemyObjects.Length; ii++) {
			if (enemyObjects[ii].GetComponent<qEnemy>()) {
				currentWaveCleared = false;
				break;
			}
		}

		// move on to next wave
		if ((waveDoneSpawning && currentWaveCleared) || time >= nextWaveTime) {
			waveNumber++;
			if (waveNumber >= level.waves.Count) {
				StartCoroutine(Victory());
				isActive = false;
			}
			else {
				time = nextWaveTime;
				waveBeginTime = time;
				nextWaveTime += level.waveTimes[waveNumber];
				for (int ii = 0; ii < level.messages[waveNumber].Count; ii++) {
					MessageInfo msgInfo = level.messages[waveNumber][ii];
					Object msgObj = Resources.Load("Prefabs/UI/Message/Message", typeof(GameObject));
					Message msg = (Instantiate(msgObj) as GameObject).GetComponent<Message>();
					msg.Initialize(msgInfo.message, msgInfo.duration);
				}
			}
		}


	}

	private IEnumerator Victory() {
		yield return new WaitForSeconds(2); 
		Application.LoadLevel("Victory");
	}

	protected override void qDie() {
		lives--;
		if (lives < 0) {
			Application.LoadLevel(Application.loadedLevel);
		}
		player.SendMessage("qDie");
		GameObject[] coins = GameObject.FindGameObjectsWithTag("coin");
		for (int ii = 0; ii < coins.Length; ii++) {
			Destroy(coins[ii]);
		}
		GridManager.instance.SendMessage("ClearNormal");
		nextWaveTime = time+level.waveTimes[waveNumber];
	}
}

public class LevelManager : qSingleton<_LevelManager> {
}