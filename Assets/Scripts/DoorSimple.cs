using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSimple : MonoBehaviour
{
    public bool invisible = false;
    private Collider2D collider;
    private SpriteRenderer pic;
    private Color picColor;

    private bool triggered;
    private float flashTime = 2f;
    private int flashFreq = 0;

    private void Start()
    {
        triggered = false;
        collider = this.GetComponent<Collider2D>();
        pic = this.GetComponent<SpriteRenderer>();
        collider.enabled = false;
        if(invisible)
        {
            pic.color = new Color(pic.color.r, pic.color.g, pic.color.b, 0F);
        }
        else
        {
            pic.color = new Color(pic.color.r, pic.color.g, pic.color.b, 0.1F);
        }
        
    }

    private void FixedUpdate()
    {
        if(triggered)
        {
            flashTime -= Time.fixedDeltaTime;
            flashFreq++;
            if(flashFreq % 10 == 0)
            {
                flashColor();
            }
            if(flashTime < 0)
            {
                triggered = false;
                pic.color = picColor;
            }
        }
    }
    public void SwitchRegister()
    {
        // Not doing anything here
    }

    public void SwitchPress()
    {
        Debug.Log("switch enter");
        collider.enabled = !collider.enabled;
        pic.color = new Color(pic.color.r, pic.color.g, pic.color.b, 1F);
        picColor = pic.color;
        triggered = true;
        flashTime = 2f;
    }

    public void SwitchRelease()
    {
        Debug.Log("switch exit");
    }

    void flashColor()
    {
        pic.color = new Color(-pic.color.r, -pic.color.g, -pic.color.b);
    }
}
