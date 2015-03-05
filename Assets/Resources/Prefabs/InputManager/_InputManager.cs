using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _InputManager : MonoBehaviour {
	private List<qObject> listeners;

	/* Add obj to the list of subscribed input listeners.
	 * TODO: Does NOT check for duplicates.
	 */
	public void Subscribe(qObject obj) {
		listeners.Add(obj);
	}

	private void Awake() {
		listeners = new List<qObject>();
	}

	private void Update() {
		for (int ii = 0; ii < listeners.Count; ii++) {
			listeners[ii].HandleInput("AxisHorizontal", Input.GetAxisRaw("Horizontal"));
			listeners[ii].HandleInput("AxisVertical", Input.GetAxisRaw("Vertical"));
			listeners[ii].HandleInput("Pause", System.Convert.ToSingle(Input.GetButtonDown("Pause")));
		}
	}
}

public class InputManager : qSingleton<_InputManager> {
}