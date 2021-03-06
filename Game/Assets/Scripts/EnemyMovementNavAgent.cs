﻿using UnityEngine;
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
    public bool soundAlerted = false;

    private NavMeshAgent agent;
    private EnemySight enemySight;

    private Transform player;

    private float chaseTimer;                               // A timer for the chaseWaitTime.
    private float patrolTimer;                              // A timer for the patrolWaitTime.
    public int pathPointIndex;

    private bool recheck = false;
    private Quaternion target;
    public bool obstaclePoint;
    public float distance = 1.1f;
    private float initialDistance;
    private float initialPatrolSpeed;
    private Vector3 soundAlertPosition;
    private bool isChasing;

    // Use this for initialization
    void Start() {
        initialPatrolSpeed = patrolSpeed;
        initialDistance = distance;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.0f;

        enemySight = GetComponent<EnemySight>();
        player = GameObject.Find("Player/Robart").transform;

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

        if(waypoint.Length > 0) {
            agent.destination = waypoint[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update() {

        if (enemySight.lastSighting != enemySight.resetSight) {
            chase();
        } else if(soundAlerted){
            sound();
        } else if(waypoint.Length > 0){
            patrol();
        }

        if (recheck) {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1.5f);
        }

        //Enemy has found the enemy
        if (Vector3.Distance(this.transform.position, player.transform.position) < distance)
        {
            GameObject.Find("LevelEnd").GetComponent<LevelEnd>().setEndCondition("The vicious enemies has cought you.");
        }


    }

    public void setSoundAlerted(Vector3 sourcePosition)
    {
        soundAlerted = true;
        agent.destination = sourcePosition;
        agent.speed = chaseSpeed;
        this.soundAlertPosition = sourcePosition;
    }

    public void resetSoundAlerted()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(soundAlertPosition, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            soundAlerted = false;
            agent.speed = patrolSpeed;
            agent.ResetPath();
            soundAlertPosition = new Vector3();
        }
    }

    void sound() {
        
        if (Vector3.Distance(this.transform.position, agent.destination) < distance) {
            // Incerement timer
            chaseTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (chaseTimer >= chaseWaitTime) {
                recheck = true;

                //Wait a bit before resetting
                if (chaseTimer >= chaseWaitTime + 2f) {
                    Debug.Log("Resetting");
                    soundAlerted = false;
                    chaseTimer = 0f;
                    recheck = false;
                    if(waypoint.Length > 0) {
                        agent.destination = waypoint[pathPointIndex].transform.position;
                    }
                    patrolSpeed = chaseSpeed;
                }
            }
        } else {
            // If not near the last sighting personal sighting of the player, reset the timer.
            chaseTimer = 0f;
        }
    }

    void chase() {
        isChasing = true;
        if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
        agent.speed = chaseSpeed;
        agent.destination = enemySight.lastSighting;

        // If near the last personal sighting...
        if (Vector3.Distance(this.transform.position, agent.destination) < distance) {
            // Incerement timer
            chaseTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (chaseTimer >= chaseWaitTime) {
                recheck = true;

                //Wait a bit before resetting
                if (chaseTimer >= chaseWaitTime + 1f) {
                    Debug.Log("Resetting");
                    isChasing = false;
                    enemySight.lastSighting = enemySight.resetSight;
                    agent.destination = waypoint[pathPointIndex].transform.position;
                    chaseTimer = 0f;
                    recheck = false;
                    if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
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

        //Detect if NavMeshAgent is hitting an obstacle
        for (int i = 1; i < agent.path.corners.Length; i++)
        {
         //   Vector3 pathPoint = agent.path.corners[i - 1];
            Vector3 nextPoint = agent.path.corners[i];
            RaycastHit hit;

            if (Physics.Linecast(this.transform.position, nextPoint, out hit))
            {
                if (hit.transform.gameObject.tag == "Gate")
                {
                    Debug.DrawLine(this.transform.position, hit.point, Color.blue);
                    agent.destination = hit.point;
                    obstaclePoint = true;
                    distance = 2 + this.transform.localScale.x;
                }

            }
        }

        // If near the next waypoint or there is no destination...
        if (Vector3.Distance(this.transform.position, agent.destination) <= distance) {
            patrolSpeed = initialPatrolSpeed;
            // ... increment the timer.
            patrolTimer += Time.deltaTime;
            obstaclePoint = false;
            distance = initialDistance;

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

    public bool getIsChasing()
    {
        return isChasing;
    }
}
