using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(NavMeshAgent))]

public class PointAndClick : MonoBehaviour {
    public Vector3 hitPosition;
    public List<GameObject> wayPoints = new List<GameObject>();
    public Material waypointMaterial;
    bool clickButton = false;
    NavMeshAgent navMeshAgent;
    GameObject waypointParent;

    // Use this for initialization
    void Start () {
        waypointParent = new GameObject();
        waypointParent.name = "Waypoints";
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Create waypoints
        if (clickButton)
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Ground")
                {
                    hitPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                    GameObject newWaypoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    newWaypoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    newWaypoint.transform.position = hitPosition + transform.up/4;
                    newWaypoint.transform.parent = waypointParent.transform;
                    newWaypoint.GetComponent<Renderer>().material = waypointMaterial;

                    int wayPointNumber = wayPoints.Count + 1;
                    newWaypoint.name = "Waypoint " +wayPointNumber;
                    wayPoints.Add(newWaypoint);
                }
            }

            clickButton = false;
        }

        //Move player to waypoints
        if(wayPoints.Count > 0)
        {
            navMeshAgent.destination = wayPoints[0].transform.position;

            if (Vector3.Distance(this.transform.position, wayPoints[0].transform.position) < 0.5f)
            {
                Destroy(wayPoints[0]);
                wayPoints.RemoveAt(0);
            }
        }
    }

    public void setClickButton(bool clickButton)
    {
        this.clickButton = clickButton;
    }
}
