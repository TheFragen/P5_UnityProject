using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Enemy_Patrol : MonoBehaviour {

    public GameObject[] waypoint;
    public List<GameObject> initialSearch = new List<GameObject>();
    public float speed = 3f;
    public float turnSpeed = 0f;

    int currentWaypoint = 0;
    bool doOnce = true;

    // Use this for initialization
    void Start()
    {
        //Find all the waypoints and sort them by their index
        initialSearch = GameObject.FindGameObjectsWithTag("EnemyWaypoint").ToList();
        foreach(GameObject elem in initialSearch)
        {
            if(elem.transform.parent == null)
            {
                initialSearch.Remove(elem);
            }

            if(elem.transform.parent != this.transform)
            {
                initialSearch.Remove(elem);
            }
        }

        waypoint = initialSearch.OrderBy(go => int.Parse(go.name.Substring(2))).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypoint < waypoint.Length - 1)
        {
       //     patrol();
        }
    }

    void patrol()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed / 2);

        if (Vector3.Distance(transform.position, waypoint[currentWaypoint].transform.position) < 0.10)
        {
            currentWaypoint++;
            var newRotation = Quaternion.LookRotation(transform.position - waypoint[currentWaypoint].transform.position, Vector3.back);
            newRotation.x = 0;
            newRotation.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
            foreach (Transform child in transform)
            {
                child.transform.Rotate(0, 0, -90);
            }
        }
    }
}
