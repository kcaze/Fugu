using UnityEngine;
using System.Collections;

public class UICanvas : MonoBehaviour {
    public int n_bombs;
    public int n_lives;
    public float levelprogress;
    Rect rmeter, rpointer,rscore;
    Rect[] rlife, rbomb;
    public Texture txbomb, txlife, txmeter, txpointer;
    public string score;
    public GUISkin scoreskin;
    private GUIStyle scorestyle;
    int width, height;
    float wcenter;
    float scalefactor;
    float unit, offset;
	// Use this for initialization
    void Start(){
        width = Screen.width;
        height = Screen.height;
        wcenter = width / 2.0f;
        scalefactor = height / 464.0f;
        offset = scalefactor * 20;
        unit = scalefactor*25;
        scorestyle = scoreskin.GetStyle("text");
        scorestyle.fontSize = Mathf.CeilToInt(scalefactor * 15);
        rmeter = new Rect( wcenter-unit*10f, height-offset-unit, unit*20,unit);
        rpointer = new Rect(wcenter-unit*5.5f, height-offset-unit, unit*0.5f, unit*0.5f);
        rscore = new Rect( wcenter-unit*3, offset, unit*6,unit); 
        rlife = new Rect[3];
        rbomb = new Rect[3];
        for (int i = 0; i < 3; i++) {
            rlife[i] = new Rect(wcenter - 100 * scalefactor - i * unit, offset, unit, unit);
            rbomb[i] = new Rect(wcenter + (100 * scalefactor -unit)+ i * unit, offset, unit, unit);
        }
        n_lives = 3; n_bombs = 3;

    }
	void OnGUI() {
        GUI.Label(rscore, score, scorestyle);
        GUI.DrawTexture(rmeter, txmeter,ScaleMode.ScaleToFit,true,0);
        for (int i = 0; i < n_lives; i++) {
            GUI.DrawTexture(rlife[i], txlife,ScaleMode.ScaleToFit,true,0);
        }
        for(int i = 0; i <n_bombs;i++){
            GUI.DrawTexture(rbomb[i], txbomb,ScaleMode.ScaleToFit,true,0);
        }
        rpointer.Set(wcenter - unit * 5.5f + 10*unit*levelprogress, height - offset - unit, 0.5f*unit, 0.5f*unit);
        Debug.Log(""+levelprogress);
        GUI.DrawTexture(rpointer, txpointer,ScaleMode.ScaleToFit,true,0);
	}
  
}
