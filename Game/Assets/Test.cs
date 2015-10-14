using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    // Use this for initialization

    public float speed = 0.0f;
   
    //float x = 0.0f;
    //float y = 0.0f;
    //float z = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (this.transform.rotation.x < 10) { 
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        //t = speed * Time.deltaTime;
    }
}
