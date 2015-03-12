using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Score : MonoBehaviour {
	public int maxDigits;
    private UICanvas canvas;
    private int prevscore;
	private void Awake() {
        canvas = GetComponent<UICanvas>();
        prevscore = -1;
	}

	private void Update () {
        int score = ScoreManager.instance.score;
        if(prevscore == score)return;
        int nzeros;
        nzeros = (score == 0) ? maxDigits : maxDigits-Mathf.CeilToInt(Mathf.Log10(score));
        string s = "";
        for (int i = 0; i < nzeros; i++) s += "0";
        s += score.ToString();
        canvas.score = s;
        prevscore = score;
	}
}
