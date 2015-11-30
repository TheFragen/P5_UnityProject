using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelEnd : MonoBehaviour {

	public bool end = false;
    System.Diagnostics.Stopwatch playedTime;

	public bool restart = false;
	private bool vicoryScreen = false;

	private GameObject canvas;

	private GameObject restartButton;
	private GameObject victoryScreenText;
	private GameObject yourTimeText;
	private GameObject victoryWindow;
    private UnityAnalytics analytics;

    void Awake()
    {
        analytics = GameObject.FindGameObjectWithTag("Analytics").transform.GetComponent<UnityAnalytics>();
    }

	// Use this for initialization
	void Start () 
	{
        playedTime = new System.Diagnostics.Stopwatch();
        canvas = GameObject.Find ("Canvas");
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (restart) 
		{
            Debug.Log ("We must go back");
			Application.LoadLevel(Application.loadedLevel);
			restart = false;

		}
	
	}



	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			end = true;
			if (vicoryScreen == false)
			{
                playedTime.Stop();
                System.TimeSpan ts = playedTime.Elapsed;
                int endTime = (int) ts.TotalSeconds;

                victoryWindow = Instantiate(Resources.Load("VictoryScreenPanel")) as GameObject;
				victoryWindow.transform.SetParent(canvas.transform, false);
				
				victoryScreenText = Instantiate(Resources.Load("VictoryText")) as GameObject;
				victoryScreenText.transform.SetParent(canvas.transform, false);
				
				yourTimeText = Instantiate(Resources.Load("YourTimeText")) as GameObject;
				yourTimeText.transform.SetParent(canvas.transform, false);

                
				yourTimeText.GetComponent<Text>().text = "Your Time: " + endTime;
				
				restartButton = Instantiate(Resources.Load("RestartButton")) as GameObject;
				restartButton.transform.SetParent(canvas.transform, false);
				restartButton.GetComponent<Button>().onClick.AddListener(() => { setRestart (true);});

				vicoryScreen = true;
                analytics.createAnalyticsEntry(endTime, "Victory");
			}

		}
	}

	public void setRestart (bool restart)
	{
		this.restart = restart;
	}

    public void setEndCondition(string endText)
    {
        end = true;
        if (vicoryScreen == false)
        {
            playedTime.Stop();
            System.TimeSpan ts = playedTime.Elapsed;
            int endTime = (int)ts.TotalSeconds;

            victoryWindow = Instantiate(Resources.Load("VictoryScreenPanel")) as GameObject;
            victoryWindow.transform.SetParent(canvas.transform, false);

            victoryScreenText = Instantiate(Resources.Load("VictoryText")) as GameObject;
            victoryScreenText.GetComponent<Text>().text = endText;
            victoryScreenText.transform.SetParent(canvas.transform, false);

            yourTimeText = Instantiate(Resources.Load("YourTimeText")) as GameObject;
            yourTimeText.transform.SetParent(canvas.transform, false);
            yourTimeText.GetComponent<Text>().text = "Your Time: " + endTime;

            restartButton = Instantiate(Resources.Load("RestartButton")) as GameObject;
            restartButton.transform.SetParent(canvas.transform, false);
            restartButton.GetComponent<Button>().onClick.AddListener(() => { setRestart(true); });

            vicoryScreen = true;
            analytics.createAnalyticsEntry(endTime, "Enemy");
        }
    }

    public void startTimer()
    {
        if(!playedTime.IsRunning) playedTime.Start();
    }


}




	/*public bool end = false;
	float playedTime = 0.0f;
	int winTime = 0;
	public GUISkin windowStyle;
	public GUIStyle timerStyle;
	public float replayButtonWidth = 500;
	public float replayButtonHeight = 300;



	// Use this for initialization
	void Start () 
	{

		windowStyle.button.fixedWidth = replayButtonWidth;
		windowStyle.button.fixedHeight = replayButtonHeight;
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (end == false)
			playedTime += Time.deltaTime;
	
	}

	public void OnGUI()
	{
		GUI.Label(new Rect(Screen.width*0.82f, 10, 150, 100), "Time: " + Mathf.RoundToInt(playedTime).ToString(), timerStyle);

		if (end == true) {
			winTime = Mathf.RoundToInt(playedTime);
			GUI.skin = windowStyle;
			GUILayout.Window(1, new Rect(0,0, Screen.width, Screen.height), EndWindow, "VICTORY!");
		}

	}

	// Content of victory window
	void EndWindow(int windowID) 
	{
		if (end == true) 
		{
			GUILayout.Label ("Congratulations, you Win!");

			GUILayout.Label("Your time: "+winTime);

			GUILayout.BeginArea (new Rect((Screen.width/2)-replayButtonWidth/2, (Screen.height/2)-replayButtonHeight/2, replayButtonWidth, replayButtonHeight));

			if (GUILayout.Button ("Replay"))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
			GUILayout.EndArea();
			
		}
	}


	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			end = true;
		}
	}*/

