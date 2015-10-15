using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Enemy_Patrol : MonoBehaviour {

    public GameObject[] waypoint;
    private List<GameObject> initialSearch = new List<GameObject>();
    private List<GameObject> sortedSearch = new List<GameObject>();
    public float speed = 3f;
    public float turnSpeed = 0f;

    public int currentWaypoint = 0;
    bool doOnce = true;

    // Use this for initialization
    void Start()
    {
        //Find all the waypoints and sort them by their index
        initialSearch = GameObject.FindGameObjectsWithTag("EnemyWaypoint").ToList();
        foreach(GameObject elem in initialSearch.ToList())
        {
            if(elem.transform.parent == this.transform)
            {
                sortedSearch.Add(elem);
            }
        }
        waypoint = sortedSearch.OrderBy(go => int.Parse(go.name.Substring(2))).ToArray();
        initialSearch = null;
        sortedSearch = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<EnemyAlert>().inSight)
        {
            if (currentWaypoint < waypoint.Length)
            {
                patrol();
            }
            else
            {
                currentWaypoint = 0;
            }
        }
    }

    void patrol()
    {
        this.GetComponent<NavMeshAgent>().destination = waypoint[currentWaypoint].transform.position;

    //    Debug.Log(Vector3.Distance(transform.position, waypoint[currentWaypoint].transform.position));
        if (Vector3.Distance(transform.position, waypoint[currentWaypoint].transform.position) < 0.75f)
        {
            currentWaypoint++;
        }
    }
}
