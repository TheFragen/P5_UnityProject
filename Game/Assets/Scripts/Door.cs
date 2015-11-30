using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public TextMesh textObject;
    public bool locked = true;
    private string[] colors = new string[] { "Red", "Blue", "Green", "Yellow" };
    // Use this for initialization
    void Start()
    {
        textObject = GameObject.Find("ColerCode").GetComponent<TextMesh>();

        for (int i = 0; i < colors.Length; i++)
        {
            string tmp = colors[i];
            int r = Random.Range(i, colors.Length);
            colors[i] = colors[r];
            colors[r] = tmp;
        }
        
        for(int i = 0; i < colors.Length; i++)
        {
            Debug.Log(colors[i]);
            textObject.text += colors[i] + " ";
        }
    }
    void Update ()
    {


    }
    void MixOrder()
    {
        if (textObject.text != null) textObject.text = null;

        for (int i = 0; i < colors.Length; i++)
        {
            string tmp = colors[i];
            int r = Random.Range(i, colors.Length);
            colors[i] = colors[r];
            colors[r] = tmp;
        }
        for (int i = 0; i < colors.Length; i++)
        {
            Debug.Log(colors[i]);
            textObject.text += colors[i] + " ";
        }
    }
}