using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool startOpen;
    private GameObject openChild;
    private GameObject closedChild;
    public Color color;

    private void Start()
    {
        openChild = transform.Find("Open").gameObject;
        closedChild = transform.Find("Closed").gameObject;
        closedChild.GetComponent<SpriteRenderer>().color = color + new Color(0, 0, 0, -0.5f);
        /*Vector2 size = ((Transform) transform).rect.size;
        openChild.GetComponent<SpriteRenderer>().size = size;
        closedChild.GetComponent<SpriteRenderer>().size = size;
        closedChild.GetComponent<BoxCollider2D>().size = size;*/

        OnSwitchableExit(); // get initial position
    }

    void OnSwitchableEnter()
    {
        openChild.SetActive(!startOpen);
        closedChild.SetActive(startOpen);
    }

    void OnSwitchableExit()
    {
        closedChild.SetActive(!startOpen);
        openChild.SetActive(startOpen);
    }
}
