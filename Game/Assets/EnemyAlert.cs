using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyAlert : MonoBehaviour {
    public float fovAngle = 0f;
    public bool inSight = false;
    public bool noSightOrRecheck = false;
    public Vector3 lastSighting;
    public float angle = 0f;
    float angleFix = -18f;
    long lastsightingTime = 0;
    public bool soundHeard = false;

    private Animator anim;
    private GameObject player;
    private Vector3 previousSighting;
    private NavMeshAgent navmeshAgent;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        navmeshAgent = this.GetComponent<NavMeshAgent>();

        this.GetComponent<LineRenderer>().SetColors(Color.red, Color.red);
        this.GetComponent<LineRenderer>().SetVertexCount(4);
        this.GetComponent<LineRenderer>().SetWidth(0.1f, 0.1f);

        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<SphereCollider>().isTrigger = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {

            Vector3 direction = other.transform.position - transform.position;
            angle = Vector3.Angle(direction, -transform.right);
            Debug.DrawRay(this.transform.position, direction);

            if (angle > ((180 - fovAngle) / 2) - angleFix && angle < fovAngle - angleFix)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, this.GetComponent<SphereCollider>().radius*2))
                {
                    if (hit.collider.gameObject == player)
                    {
                        inSight = true;
                        noSightOrRecheck = false;
                        lastSighting = player.transform.position;
                        navmeshAgent.SetDestination(lastSighting);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            inSight = false;
        angle = 0f;
        Debug.Log("Cleared");
        lastsightingTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    // Update is called once per frame
    void Update()
    {
        long currTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
   //     Debug.Log(currTime - lastsightingTime);
        long compare = currTime - lastsightingTime;
        if (!inSight && compare < 1000)
        {
            Quaternion target = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2f);
            noSightOrRecheck = true;
        }

        Quaternion left = Quaternion.AngleAxis(((180 - fovAngle) / 2) - angleFix, transform.up);
        Quaternion right = Quaternion.AngleAxis(fovAngle - angleFix, transform.up);

        this.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        this.GetComponent<LineRenderer>().SetPosition(1, (left * -this.transform.right * 4) + transform.position);
        this.GetComponent<LineRenderer>().SetPosition(2, transform.position);
        this.GetComponent<LineRenderer>().SetPosition(3, (right * -this.transform.right * 4) + transform.position);

        if (soundHeard)
        {
           
        }
    }
}
