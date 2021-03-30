using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSimple : MonoBehaviour
{
    public bool invisible = false;
    private Collider2D collider;
    private SpriteRenderer pic;

    private void Start()
    {
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

    public void SwitchRegister()
    {
        // Not doing anything here
    }

    public void SwitchPress()
    {
        Debug.Log("switch enter");
        collider.enabled = !collider.enabled;
        pic.color = new Color(pic.color.r, pic.color.g, pic.color.b, 1F);
    }

    public void SwitchRelease()
    {
        Debug.Log("switch exit");
    }
}
