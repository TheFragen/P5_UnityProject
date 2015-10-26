using UnityEngine;
using System.Collections;

public class sampleButton : MonoBehaviour
{
    GameObject player;
    public GameObject objectToAffect;
    private bool open = false;
    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
      
    }

    void OnTriggerStay(Collider other) {
      
        if(other.gameObject.tag == "Player" && Input.GetKeyDown("e")) {
           open = !open;
           objectToAffect.GetComponent<gate>().setIsActivated(open);
        }
    }
}
