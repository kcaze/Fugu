using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Message : MonoBehaviour {
	private Text text;
	private Image image;

	public void Initialize(string message, float duration) {
		Canvas canvas = (Canvas) FindObjectOfType(typeof(Canvas));
		text = gameObject.GetComponentInChildren<Text>();
		image = gameObject.GetComponent<Image>();

		// TODO: this is stupid, should add a way to modify color components directly
		Color c;
		c = text.color;
		c.a = 0;
		text.color = c;
		c = image.color;
		c.a = 0;
		image.color = c;

		text.text = message;
		transform.SetParent(canvas.transform, false);
		StartCoroutine(Behave(duration));
	}

	private IEnumerator Behave(float duration) {
		StartCoroutine(FadeIn());
		yield return new WaitForSeconds(duration-0.5f);
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut() {
		for (int ii = 0; ii < 100; ii++) {
			Color c;
			c = text.color;
			c.a -= 0.01f;
			text.color = c;
			c = image.color;
			c.a -= 0.01f;
			image.color = c;
			yield return new WaitForSeconds(0.005f);
		}
		Destroy(gameObject);
	}

	private IEnumerator FadeIn() {
		for (int ii = 0; ii < 100; ii++) {
			Color c;
			c = text.color;
			c.a += 0.01f;
			text.color = c;
			c = image.color;
			c.a += 0.01f;
			image.color = c;
			yield return new WaitForSeconds(0.005f);
		}
	}
}
