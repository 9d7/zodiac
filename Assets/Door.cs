using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool startOpen;
    private GameObject openChild;
    private GameObject closedChild;

    private void Start()
    {
        openChild = transform.Find("Open").gameObject;
        closedChild = transform.Find("Closed").gameObject;

        Vector2 size = ((RectTransform) transform).rect.size;
        openChild.GetComponent<SpriteRenderer>().size = size;
        closedChild.GetComponent<SpriteRenderer>().size = size;
        closedChild.GetComponent<BoxCollider2D>().size = size;

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
