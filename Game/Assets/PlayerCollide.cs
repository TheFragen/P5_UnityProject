using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        GameObject colorCodeManager = GameObject.Find("ColerCodeManager");
 
        if (other.gameObject.CompareTag("RedPlate"))
        {
            GameObject.Find("ColerCodeManager").GetComponent<Door>().inputString("Red");
        }
        if (other.gameObject.CompareTag("YellowPlate"))
        {
            GameObject.Find("ColerCodeManager").GetComponent<Door>().inputString("Yellow");
        }
        if (other.gameObject.CompareTag("BluePlate"))
        {
            GameObject.Find("ColerCodeManager").GetComponent<Door>().inputString("Blue");
        }
        if (other.gameObject.CompareTag("GreenPlate"))
        {
            GameObject.Find("ColerCodeManager").GetComponent<Door>().inputString("Green");
        }
    }
}
