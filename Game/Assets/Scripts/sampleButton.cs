using UnityEngine;
using System.Collections;

public class sampleButton : MonoBehaviour
{
    GameObject player;
    public GameObject objectToAffect;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            objectToAffect.GetComponent<gate>().setIsActivated(true);
        }
    }
}
