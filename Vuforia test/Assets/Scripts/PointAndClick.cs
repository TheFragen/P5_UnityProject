using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(NavMeshAgent))]

public class PointAndClick : MonoBehaviour {
    public Vector3 hitPosition;
    public List<GameObject> wayPoints = new List<GameObject>();
    public GameObject wayPointObject;
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

                    GameObject newWaypoint = Instantiate(wayPointObject) as GameObject;
                 //   newWaypoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    newWaypoint.transform.position = hitPosition + transform.up/4;
                    newWaypoint.transform.parent = waypointParent.transform;

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

            //Draw path to next waypoint
            this.transform.GetComponent<LineRenderer>().SetVertexCount(navMeshAgent.path.corners.Length);
            this.transform.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
            for (int i = 1; i < navMeshAgent.path.corners.Length; i++)
            {
                this.transform.GetComponent<LineRenderer>().SetPosition(i, navMeshAgent.path.corners[i] + transform.up/6);
            }

            if (Vector3.Distance(this.transform.position, wayPoints[0].transform.position) < 0.5f)
            {
                Destroy(wayPoints[0]);
                wayPoints.RemoveAt(0);

                if(wayPoints.Count == 0)
                {
                    this.transform.GetComponent<LineRenderer>().SetVertexCount(0);
                }
            }
        }
    }

    public void setClickButton(bool clickButton)
    {
        this.clickButton = clickButton;
    }
}
