using UnityEngine;
using System.Collections;

public class Offset : MonoBehaviour {

    public float scrollSpeed = 0.5F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        
        foreach (Material matt in rend.materials) 
        {
            if (matt.name == "UVMapTanktracks (Instance)")
            {
                matt.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                Debug.Log("Inside");
            }
        }
      
    }
}