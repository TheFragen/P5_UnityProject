using UnityEngine;
using System.Collections;

public class rotatingLaser : MonoBehaviour
{
    private Transform player;
    private GameObject Laser;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player/Robart").transform;
        Laser = GameObject.Find("Lasers");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform)
        {
            if (Vector3.Distance(child.transform.position, player.transform.position) < 2.5f)
            {
                Debug.Log("shit");
                GameObject.Find("LevelEnd").GetComponent<LevelEnd>().setEndCondition("Det var endnu mere dumt.");
            }
        }
       
       
    }
}
