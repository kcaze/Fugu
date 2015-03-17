using UnityEngine;
using System.Collections;

public class Setter : MonoBehaviour {
	public void SetRectTransform (RectTransform rectTransform) {
		Vector2 anchoredPosition = rectTransform.anchoredPosition;
		anchoredPosition.y *= -1;
		anchoredPosition.y += 45;
		gameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
	}
}
