using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class customKeepAliveBehaviour : MonoBehaviour {
    public string userID;
    public Transform imageTarget;

    public static customKeepAliveBehaviour Instance;
    void Awake()
    {
       Screen.SetResolution(640, 480, true);
   /*     Transform[] allTrans = Resources.FindObjectsOfTypeAll<Transform>();
        foreach(Transform elem in allTrans)
        {
            if (elem.name == "ImageTarget")
            {
                elem.gameObject.SetActive(true);
                break;
            }
       
        }
        */
    }
    
	// Use this for initialization
	void Start () {
        if (Application.loadedLevel > 0)
        {
            GameObject.Find("InputField").SetActive(false);
            GameObject.FindGameObjectWithTag("Analytics").GetComponent<UnityAnalytics>().setUserID(userID);
        }
        else if (!string.IsNullOrEmpty(userID))
        {

            GameObject.Find("InputField").SetActive(false);
            GameObject.FindGameObjectWithTag("Analytics").GetComponent<UnityAnalytics>().setUserID(userID);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Application.loadedLevel == 0 && !string.IsNullOrEmpty(GameObject.Find("UserID").GetComponent<UnityEngine.UI.Text>().text) && string.IsNullOrEmpty(userID))
        {
            userID = GameObject.Find("UserID").GetComponent<UnityEngine.UI.Text>().text;
            Application.LoadLevel(1);
        }
        if(GameObject.FindGameObjectWithTag("Analytics") != null 
            && string.IsNullOrEmpty(GameObject.Find("UserID").GetComponent<UnityEngine.UI.Text>().text))
        {
            GameObject.Find("InputField").SetActive(false);
            GameObject.FindGameObjectWithTag("Analytics").GetComponent<UnityAnalytics>().setUserID(userID);
        }

    }
}
