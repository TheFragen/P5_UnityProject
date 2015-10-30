using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]

public class soundSystem : MonoBehaviour {
    SphereCollider soundWave;
    AudioSource audioSource;
    public bool createSound = false;
    private bool fireOnce = true;
    private bool soundHasPlayed = false;
    private long timeSincePlay;
    private long currentTime;
    private float loudnessScalar = 0.1f;
    private float sphereRadiusScalar;
    public Vector3 positionToSend;

    [Tooltip("Defines how far away the enemy can be to be alerted by sound. Distance = loundness * 10.")]
    public float loudness = 1;

	// Use this for initialization
	void Awake () {
        soundWave = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        //soundWave.radius = 0.01f;
        sphereRadiusScalar = soundWave.radius;
        this.gameObject.tag = "SoundEmitter";
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        if (createSound) {
            if(!soundHasPlayed) {
                if(audioSource.clip != null) audioSource.Play();
                soundHasPlayed = true;
                timeSincePlay = currentTime;
            }
            if(soundWave.radius < loudnessScalar * loudness) {
                soundWave.radius += sphereRadiusScalar;
            }
        } else {
            soundWave.radius = sphereRadiusScalar;
            if (audioSource.clip != null) audioSource.Stop();
            fireOnce = true;
        }

        if(timeSincePlay + (1000 * loudness) < currentTime) {
            createSound = false;
            soundHasPlayed = false;
        }
    }

    void OnTriggerExit(Collider other) {
  /*      if (other.gameObject.tag == "Enemy") {
            fireOnce = true;
        }*/
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            Vector3 direction = other.transform.position - transform.position;
      //      Debug.Log("Hit enemy");
      //      Debug.Log("Distance to Enemy: " + Vector3.Distance(this.transform.position, other.transform.position));
      //      Debug.Log("Soundwave radius: " + soundWave.radius * 100);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, soundWave.radius * 100 * 2)) {

                //      if(Vector3.Distance(this.transform.position, hit.transform.position) < loudnessScalar * loudness && createSound) {
                    Debug.DrawLine(this.transform.position, hit.point, Color.red);
                    if (fireOnce) {
                        fireOnce = false;
                        other.GetComponent<EnemyMovementNavAgent>().setSoundAlerted(positionToSend);
                    }
          //      }
            }
        }
    }

    public void setSound()
    {
        if(!createSound)
        {
            createSound = true;
        }
    }
}
