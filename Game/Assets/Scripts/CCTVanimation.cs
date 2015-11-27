using UnityEngine;
using System.Collections;
using System.Linq;

public class CCTVanimation : MonoBehaviour {

    public float speed = 0.0f;
    public bool reverse;
    public int minRotation;
    public int maxRotation;
    int rotation;

    void Update()
    {
        /*   if(Enumerable.Range((int)this.transform.eulerAngles.y - 5, (int)this.transform.eulerAngles.y + 5).Contains(minRotation-2))
           {
               rotation = maxRotation;
               reverse = true;
           }else if (Enumerable.Range((int) this.transform.eulerAngles.y - 5, (int) this.transform.eulerAngles.y + 5).Contains(maxRotation))
           {
               reverse = false;
               rotation = minRotation;
           }*/

        if (this.transform.eulerAngles.y > 20)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        } else if (this.transform.eulerAngles.y < 220)
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * speed);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rotation, transform.eulerAngles.z), Time.deltaTime);

    }
}
