using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSimple : MonoBehaviour
{
    public bool startOpen;
    private Collider2D collider;
    private SpriteRenderer pic;

    private void Start()
    {
        collider = this.GetComponent<Collider2D>();
        pic = this.GetComponent<SpriteRenderer>();
        collider.enabled = false;
        pic.color = new Color(1, 1, 1, 0.1F);
    }

    public void SwitchRegister()
    {
        // Not doing anything here
    }

    public void SwitchPress()
    {
        Debug.Log("switch enter");
        collider.enabled = !collider.enabled;
        pic.color = new Color(1, 1, 1, 1F);
    }

    public void SwitchRelease()
    {
        Debug.Log("switch exit");
    }
}
