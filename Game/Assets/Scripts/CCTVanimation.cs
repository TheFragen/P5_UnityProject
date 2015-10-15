using UnityEngine;
using System.Collections;

public class CCTVanimation : MonoBehaviour {

    // Use this for initialization

    public float speed = 0.0f;
    bool reverse = true;
    //float x = 0.0f;
    //float y = 0.0f;
    //float z = 0.0f;
    // Update is called once per frame
    void Update()
    {
       // Debug.Log("reverse" + reverse);
        //if(this.transform.rotation.x < 10 & this.transform.eulerAngles.y < 10 & this.transform.rotation.z < 319) { 
        if (reverse==true)
        {
            if (this.transform.eulerAngles.x > 328)
            {
                transform.Rotate(Vector3.up, speed * Time.deltaTime);
            }
            else if (this.transform.eulerAngles.x >= 0 & this.transform.eulerAngles.x < 31)
            {
                transform.Rotate(Vector3.up, speed * Time.deltaTime);
            }
            else
            {
                reverse = false;
            }
            }
        else
        {
            if (this.transform.eulerAngles.x >= 0 & this.transform.eulerAngles.x < 32)
            {
                transform.Rotate(-Vector3.up, speed * Time.deltaTime);
            }
            else if (this.transform.eulerAngles.x > 330)
            {
                transform.Rotate(-Vector3.up, speed * Time.deltaTime);
            }
            else
            {
                reverse = true;
            }
        }

    }
       // else
        //    transform.Rotate(-Vector3.up, speed * Time.deltaTime);
   // }
}
