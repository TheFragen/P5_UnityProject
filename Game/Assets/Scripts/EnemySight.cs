using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fovAngle = 110f;
    public bool playerInSight;
	public Vector3 personalLastSighting = new Vector3(1000.0f, 1000.0f, 1000.0f);

	public Vector3 resetSight = new Vector3(1000.0f, 1000.0f, 1000.0f);
    private SphereCollider col;
    private GameObject player;
    private Vector3 previousSighting;

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerStay (Collider other)
    {
        
        if (other.gameObject == player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius * 2))
            {
                if (angle < fovAngle * 0.5f)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    if (hit.collider.gameObject == player)
                    {
                        if (Physics.Linecast(this.transform.position, hit.point, out hit))
                        {

                        }

                        playerInSight = true;   
                        personalLastSighting = player.transform.position;
                        Debug.Log("player sighted");
                    }

                //Check if player is really close to enemy, and mark that as sight
                } else if (hit.distance < 1.5f && hit.collider.gameObject == player) {
                    playerInSight = true;
                    personalLastSighting = player.transform.position;
                    Debug.Log("player sighted");
                }

            } 
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }

}
