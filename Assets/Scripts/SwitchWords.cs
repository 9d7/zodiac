using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWords : MonoBehaviour
{
    public Text textObject;
    private string words;
    private bool isOn = false;
    public float lastSec = 5f;
    public float fadeSec = 5f;
    private float lastTime;
    private float fadedTime;


    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 0F);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn == true)
        {
            lastTime -= Time.deltaTime;
            if(lastTime < 0)
            {
                fadedTime -= Time.deltaTime;
                textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, fadedTime/ fadeSec);
                if(fadedTime < 0)
                {
                    isOn = false;
                }
            }
        }
    }

    public void SwitchPress()
    {
        Debug.Log("switch enter");
        isOn = true;
        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 1);
        lastTime = lastSec;
        fadedTime = fadeSec;
    }

    public void SwitchRelease()
    {
        Debug.Log("switch exit");
    }

    public void SwitchRegister()
    {
        // Not doing anything here
    }
}
