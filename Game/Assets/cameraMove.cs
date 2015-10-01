using UnityEngine;
using System.Collections;
using System;

public class cameraMove : MonoBehaviour {
    public float speed = 0f;
    long lastDebounceTime = 0;
    long debounceDelay = 150;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
       //     transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
       //     transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed, Space.World);
       //     transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            //     transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - lastDebounceTime) > debounceDelay)
            {
                lastDebounceTime = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PointAndClick>().setClickButton(true);
            }
        }
    }
}
