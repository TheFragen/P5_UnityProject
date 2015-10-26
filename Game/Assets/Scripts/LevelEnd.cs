using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	public bool end = false;
	float playedTime = 0.0f;
	int winTime = 0;
	public GUISkin windowStyle;
	public GUIStyle timerStyle;
	public float replayButtonWidth = 500;
	public float replayButtonHeight = 300;

	// Use this for initialization
	void Start () {

		windowStyle.button.fixedWidth = replayButtonWidth;
		windowStyle.button.fixedHeight = replayButtonHeight;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (end == false)
			playedTime += Time.deltaTime;
	
	}

	public void OnGUI(){
		GUI.Label(new Rect(Screen.width*0.82f, 10, 150, 100), "Time: " + Mathf.RoundToInt(playedTime).ToString(), timerStyle);

		if (end == true) {
			winTime = Mathf.RoundToInt(playedTime);
			GUI.skin = windowStyle;
			GUILayout.Window(1, new Rect(0,0, Screen.width, Screen.height), EndWindow, "VICTORY!");
		}

	}

	// Content of victory window
	void EndWindow(int windowID) {
		if (end == true) {
			GUILayout.Label ("Congratulations, you Win!");

			GUILayout.Label("Your time: "+winTime);

			GUILayout.BeginArea (new Rect((Screen.width/2)-replayButtonWidth/2, (Screen.height/2)-replayButtonHeight/2, replayButtonWidth, replayButtonHeight));

			if (GUILayout.Button ("Replay")){
				Application.LoadLevel (Application.loadedLevel);
			}
			GUILayout.EndArea();
			
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			end = true;
		}
	}
}
