using UnityEngine;
using System.Collections;

public class UICanvas : MonoBehaviour {
    public float levelprogress;
    public GameObject pausescreen;
    Rect rmeter, rpointer;
    public Texture txmeter, txpointer;
    int n_bombs;
    int n_lives;
    int score;
    int width, height;
    float wcenter;
    float scalefactor;
    float unit, offset;
	// Use this for initialization
    
      
    void Start(){
        pausescreen.SetActive(false);
        width = Screen.width;
        height = Screen.height;
        wcenter = width / 2.0f;
        scalefactor = height / 464.0f;
        offset = scalefactor * 20;
        unit = scalefactor*25;
        rmeter = new Rect( wcenter-unit*10f, height-offset-unit, unit*20,unit);
        rpointer = new Rect(wcenter-unit*5.5f, height-offset-unit, unit*0.5f, unit*0.5f);
        n_lives = 3; n_bombs = 3;

    }
	/*void OnGUI() {
        GUI.DrawTexture(rmeter, txmeter,ScaleMode.ScaleToFit,true,0);
        rpointer.Set(wcenter - unit * 5.5f + 10*unit*levelprogress, height - offset - unit, 0.5f*unit, 0.5f*unit);
        GUI.DrawTexture(rpointer, txpointer,ScaleMode.ScaleToFit,true,0);
	}*/
    void Update() {
        int lives = LevelManager.instance.lives;
        if (n_lives != lives && lives >0) transform.FindChild(n_lives.ToString()).gameObject.SetActive(false);
        n_lives = lives;
        int curscore = ScoreManager.instance.score;
        //if (curscore != score) (transform.FindChild("score").gameObject as GameObject).GetComponent<GUI>()=curscore.ToString();
        score = curscore;
    }

     public void ReturnToMainMenu()
    {
        Application.LoadLevel("Main Menu");
    }

    public void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

  
}
