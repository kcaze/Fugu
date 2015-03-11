using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInfo {
	public Vector3 position;
	public string path;
}

public class MessageInfo {
	public string message;
	public float duration;
}

public class Level : MonoBehaviour {
	public List<List<EnemyInfo>> waves;
	public List<List<MessageInfo>> messages;
	public List<float> waveTimes;
	private List<EnemyInfo> currentWave;
	private List<MessageInfo> currentWaveMessages;

	public virtual void Setup() {
	}

	public void BeginLevel() {
		waves = new List<List<EnemyInfo>> ();
		messages = new List<List<MessageInfo>> ();
		waveTimes = new List<float> ();
	}

	// time is duration for the new wave
	public void NewWave(float time) {
		currentWave = new List<EnemyInfo> ();
		currentWaveMessages = new List<MessageInfo> ();
		waves.Add(currentWave);
		messages.Add(currentWaveMessages);
		waveTimes.Add(time);
	}

	public void AddEnemy (float x, float y, string s) {
		if (currentWave == null) {
			Debug.LogError("Must call NewWave() before AddEnemy()");
			return;
		}
		EnemyInfo enemyInfo = new EnemyInfo ();
		enemyInfo.position = new Vector3(x, 0.05f, y);
		enemyInfo.path = "Prefabs/Enemies/"+s+"/"+s;
		currentWave.Add(enemyInfo);
	}

	public void AddMessage (string s, float t) {
		if (currentWave == null) {
			Debug.LogError("Must call NewWave() before AddMessage()");
			return;
		}
		MessageInfo messageInfo = new MessageInfo ();
		messageInfo.message = s;
		messageInfo.duration = t;
		currentWaveMessages.Add(messageInfo);
	}

	public void EndLevel() {
	}
}
