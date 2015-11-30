using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public TextMesh textObject;
    public bool locked = true;
    private string[] colors = new string[] { "Red", "Blue", "Green", "Yellow" };
    public string[] ColorsPressed;
    private int same;
    bool openDoor = false;

    // Use this for initialization
    void Start()
    {
        ColorsPressed = new string[4];
        textObject = GameObject.Find("ColerCodeManager").GetComponent<TextMesh>();
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
        same = 0;
        if (ColorsPressed[3] != null && openDoor == false)
        {
            for (int j = 0; j <= ColorsPressed.Length - 1; j++)
            {
                if (ColorsPressed[j].Equals(colors[j]))
                {
                    same += 1;
                    //print(same);
                }
                if (same == 4)
                {
                    Debug.Log("You did it");
                    openDoor = true;
                    GameObject Door = GameObject.Find("Door");
                    Door.transform.position = new Vector3(-2, 0, 0);
                    Door.GetComponent<BoxCollider>().enabled = false;
                    same = 0;
                }
            }
        }

    }
    public void inputString(string input)
    {
        
        for (int i = 0; i <= ColorsPressed.Length-1; i++)
            {
                   
                   if (ColorsPressed[i] == null)
                {
                    ColorsPressed[i] = input;
                    Debug.Log("put inside" + ColorsPressed[i] + (i+1));
                    if(colors[i] != ColorsPressed[i])
                {
                    System.Array.Clear(ColorsPressed, 0, ColorsPressed.Length);
                }
                    break;
                }
                   if(i == 3 && ColorsPressed[i] != null)
                {
                    System.Array.Clear(ColorsPressed, 0, ColorsPressed.Length);
                    ColorsPressed[0] = input;
                    Debug.Log(ColorsPressed[0] + 1);
                    break;
                }            
        }
    }
}