using UnityEngine;
using System.Collections;

public class Enemy_movement : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    float t = 0.0f;
    public float velocity = 0.0f;
    public GameObject P0;
    public GameObject P1;
    public GameObject P2;
    // Update is called once per frame
    void Update()
    {
        transform.position = (1 - t) * (1 - t) * P0.transform.position + 2 * (1 - t) * t * P1.transform.position + (t * t) * P2.transform.position;
        t += velocity * Time.deltaTime;
        if (t > 1.0)
        {
            velocity = -velocity;

        }
        if (t < 0.0) { velocity = -velocity; }
    }
}
