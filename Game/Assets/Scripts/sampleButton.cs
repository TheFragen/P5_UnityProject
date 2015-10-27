using UnityEngine;
using System.Collections;

public class sampleButton : MonoBehaviour
{
    GameObject player;
    public GameObject objectToAffect;
    private bool open = true;
    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
      
    }

    void OnTriggerEnter(Collider other) {
      
        if(other.gameObject.tag == "Player") {
        //   open = !open;
           objectToAffect.GetComponent<gate>().setIsActivated(open);
        }
    }
}
