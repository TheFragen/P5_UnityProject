using UnityEngine;
using System.Collections;

public class laserCollide : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

    void OnTriggerEnter(Collider other)
    {
      

        if (other.tag == "laserChild")
        {
                    Debug.Log("shit");
                    GameObject.Find("LevelEnd").GetComponent<LevelEnd>().setEndCondition("Det var fandme dumt.");
            }
        if (other.tag == "rotatingLaser")
        {
            Debug.Log("shit");
            GameObject.Find("LevelEnd").GetComponent<LevelEnd>().setEndCondition("Det var endnu mere dumt.");
        }
       if (other.tag == "lavaCollider")
        {
            Debug.Log("shit");
            GameObject.Find("LevelEnd").GetComponent<LevelEnd>().setEndCondition("jesus");
        }

}
        
        

    // Update is called once per frame
    void Update () {
	
	}
}
