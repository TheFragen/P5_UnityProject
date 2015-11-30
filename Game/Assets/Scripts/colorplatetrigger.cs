using UnityEngine;
using System.Collections;

public class colorplatetrigger : MonoBehaviour {

    Collider hit = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hit != other)
            {
                hit = other;
            }

        }
    }
}
