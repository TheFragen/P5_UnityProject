using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System.Text;
using System;
using System.IO;

public class UnityAnalytics : MonoBehaviour {
    public string userID;
    private cycleControls control;
    public List<Vector3> playerPositions;
    Transform player;
    private IDbConnection databaseConnection;
    private IDbCommand dbcmd;
    private IDataReader reader;
    private StringBuilder builder;
    public bool isDebug;

    // Use this for initialization
    void Start () {
      //  Analytics.SetUserId(userID);
        control = GameObject.Find("Control Cycler").GetComponent<cycleControls>();
        player = GameObject.Find("Player").transform;
        playerPositions.Add(player.localPosition);

        if(!File.Exists(Application.dataPath + "/testingData.s3db"))
        {
            OpenDB();

            string[] col = { "id", "userID", "endTime", "source", "controlScheme" };
            string[] colType = { "integer primary key autoincrement", "text", "integer", "text", "text" };

            if (!CreateTable("testing", col, colType))
                Debug.Log("Error creating table");
            CloseDB();
        }
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
    /*    Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            {"userID", userID },
            {"endtime", endTime },
            {"source", sourceOfEnd },
            {"controlScheme", control.getCurrentControlScheme()},
            {"playerPositions", playerPositions.ToArray() }
        });*/

        string query = string.Format("INSERT INTO testing (userID,endTime,source,controlScheme) VALUES('{0}',{1},'{2}','{3}')", userID, endTime, sourceOfEnd, control.getCurrentControlScheme());
        save(query);
    }

    public void setUserID(string userID)
    {
        this.userID = userID;
        GameObject.Find("LevelEnd").GetComponent<LevelEnd>().startTimer();
        Debug.Log("Timer started");
    }

    void save(string query)
    {
        OpenDB();
        try
        {
            dbcmd = databaseConnection.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader(); 
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        CloseDB();
    }

    public void OpenDB()
    {
        string connectionURI;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            connectionURI = "URI=file:" + Application.dataPath + "/testingData.s3db";
        }
        else
        {
            connectionURI = "URI=file:" + Application.persistentDataPath + "/testingData.s3db";
        }
        databaseConnection = (IDbConnection) new SqliteConnection(connectionURI);
        databaseConnection.Open();
    }

    public void CloseDB()
    {
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        databaseConnection.Close();
        databaseConnection = null;
    }

    IDataReader BasicQuery(string query)
    { 
        dbcmd = databaseConnection.CreateCommand(); // create empty command
        dbcmd.CommandText = query; // fill the command
        reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        return reader; // return the reader

    }


    bool CreateTable(string name, string[] col, string[] colType)
    { // Create a table, name, column array, column type array
        string query;
        query = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0];
        for (var i = 1; i < col.Length; i++)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";
        try
        {
            dbcmd = databaseConnection.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
            return false;
        }
        return true;
    }
}
