using UnityEngine;
using System.Collections;

public class EnemyMovementNavAgent : MonoBehaviour {

	public Transform[] pathPoints;

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



	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.stoppingDistance = 2.0f;

		enemySight = GetComponent<EnemySight> ();
		player = GameObject.FindWithTag("Player").transform;

		// Set pos to point1 and destination to point2
		/*Vector3 temp = transform.position;
		temp.x = pathPoint01.position.x;
		temp.z = pathPoint01.position.z;
		transform.position = temp;
		agent.SetDestination (pathPoint02.position);*/
	
	}
	
	// Update is called once per frame
	void Update () {
			
		if (enemySight.personalLastSighting != enemySight.resestSight) {
			chase ();
		} else {
			patrol ();

		}
		// When enemy at a point change destination to other point (kinda shit)
		/*if (agent.destination == pathPoint02.position && agent.remainingDistance <= agent.stoppingDistance){
			//agent.SetDestination (pathPoint01.position);
		}
		if (agent.destination == pathPoint01.position && agent.remainingDistance <= agent.stoppingDistance){
			agent.SetDestination (pathPoint02.position);
		}*/

	}

	void chase(){
		agent.speed = chaseSpeed;

		agent.destination = enemySight.personalLastSighting;

		// If near the last personal sighting...
		if(agent.remainingDistance < agent.stoppingDistance)
		{
			// ... increment the timer.
			chaseTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(chaseTimer >= chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				//lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = enemySight.resestSight;
				chaseTimer = 0f;
			}
		}
		else
			// If not near the last sighting personal sighting of the player, reset the timer.
			chaseTimer = 0f;
	}

	void patrol (){
		// Set an appropriate speed for the NavMeshAgent.
		agent.speed = patrolSpeed;
		
		// If near the next waypoint or there is no destination...
		if(/*agent.destination == lastPlayerSighting.resetPosition ||*/ agent.remainingDistance <= agent.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(pathPointIndex == pathPoints.Length - 1)
					pathPointIndex = 0;
				else
					pathPointIndex++;
				
				// Reset the timer.
				patrolTimer = 0;
			}
		}
		else
			// If not near a destination, reset the timer.
			patrolTimer = 0;
		
		// Set the destination to the patrolWayPoint.
		agent.destination = pathPoints[pathPointIndex].position;

	}
}
