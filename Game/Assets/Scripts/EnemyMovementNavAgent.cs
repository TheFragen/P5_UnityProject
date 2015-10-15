using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyMovementNavAgent : MonoBehaviour
{
    public GameObject[] waypoint;
    private List<GameObject> initialSearch = new List<GameObject>();
    private List<GameObject> sortedSearch = new List<GameObject>();

    public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
    public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
    public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
    public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.

    private NavMeshAgent agent;
    private EnemySight enemySight;

    private Transform player;

    private float chaseTimer;                               // A timer for the chaseWaitTime.
    private float patrolTimer;                              // A timer for the patrolWaitTime.
    private int pathPointIndex;

    private bool recheck = false;
    private Quaternion target;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.0f;

        enemySight = GetComponent<EnemySight>();
        //	player = GameObject.FindWithTag("Player").transform;

        //Find all the waypoints and sort them by their index
        initialSearch = GameObject.FindGameObjectsWithTag("EnemyWaypoint").ToList();
        foreach (GameObject elem in initialSearch.ToList()) {
            if (elem.transform.parent == this.transform) {
                sortedSearch.Add(elem);
            }
        }
        waypoint = sortedSearch.OrderBy(go => int.Parse(go.name.Substring(2))).ToArray();
        initialSearch = null;
        sortedSearch = null;
        target = Quaternion.Euler(0, 180, 0);

        agent.destination = waypoint[0].transform.position;
    }

    // Update is called once per frame
    void Update() {

        if (enemySight.personalLastSighting != enemySight.resetSight) {
            chase();
        } else {
            patrol();
        }

        if (recheck) {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1.5f);
        }

    }

    void chase() {
        agent.speed = chaseSpeed;
        agent.destination = enemySight.personalLastSighting;

        // If near the last personal sighting...
        if (Vector3.Distance(this.transform.position, agent.destination) < 1.1f) {
            // Incerement timer
            chaseTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (chaseTimer >= chaseWaitTime) {
                recheck = true;

                //Wait a bit before resetting
                if (chaseTimer >= chaseWaitTime + 3f) {
                    Debug.Log("Resetting");
                    enemySight.personalLastSighting = enemySight.resetSight;
                    agent.destination = waypoint[pathPointIndex].transform.position;
                    chaseTimer = 0f;
                    recheck = false;
                }
            }
        } else {
            // If not near the last sighting personal sighting of the player, reset the timer.
            chaseTimer = 0f;
        }
    }

    void patrol() {
        // Set an appropriate speed for the NavMeshAgent.
        agent.speed = patrolSpeed;

        //Draw path to next waypoint
        this.transform.GetComponent<LineRenderer>().SetVertexCount(agent.path.corners.Length);
        this.transform.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        for (int i = 1; i < agent.path.corners.Length; i++) {
            this.transform.GetComponent<LineRenderer>().SetPosition(i, agent.path.corners[i] + transform.up / 6);
        }

        // If near the next waypoint or there is no destination...
        if (Vector3.Distance(this.transform.position, waypoint[pathPointIndex].transform.position) < 1.1f) {
            // ... increment the timer.
            patrolTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (patrolTimer >= patrolWaitTime) {

                // Increment the wayPointIndex.
                if (pathPointIndex == waypoint.Length - 1) {
                    pathPointIndex = 0;
                } else {
                    pathPointIndex++;
                }

                // Reset the timer.
                patrolTimer = 0;
            }

            // Set the destination to the patrolWayPoint.
            agent.destination = waypoint[pathPointIndex].transform.position;

        } else {
            // If not near a destination, reset the timer.
            patrolTimer = 0;
        }
    }
}
