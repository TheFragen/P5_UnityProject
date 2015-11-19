using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class UnityAnalytics : MonoBehaviour {
    public string userID;
    private cycleControls control;
    public List<Vector3> playerPositions;
    Transform player;

	// Use this for initialization
	void Start () {
        Analytics.SetUserId(userID);
        control = GameObject.Find("Control Cycler").GetComponent<cycleControls>();
        player = GameObject.Find("Player").transform;
        playerPositions.Add(player.localPosition);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if(player.localPosition != playerPositions[playerPositions.Count - 1])
        {
            playerPositions.Add(player.localPosition);
        }
    }

    public void createAnalyticsEntry(int endTime, string sourceOfEnd)
    {
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            {"userID", userID },
            {"endtime", endTime },
            {"source", sourceOfEnd },
            {"controlScheme", control.getCurrentControlScheme()},
            {"playerPositions", playerPositions.ToArray() }
        });
    }

    public void setUserID(string userID)
    {
        this.userID = userID;
        GameObject.Find("LevelEnd").GetComponent<LevelEnd>().startTimer();
        Debug.Log("Timer started");
    }

    void newLocation()
    {
        
    }
}
