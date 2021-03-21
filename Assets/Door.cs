using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject openChild;
    public GameObject closedChild;

    public int pressesToTrigger;
    public bool inverted;

    private int numPresses = 0;

    public void RegisterPress(bool pressed)
    {
        if (pressed)
        {
            numPresses++;
        }
        else
        {
            numPresses--;
        }

        if (numPresses == pressesToTrigger)
        {
            if (inverted)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    private void Start()
    {
        if (inverted)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    void Open()
    {
        openChild.SetActive(true);
        closedChild.SetActive(false);
    }

    void Close()
    {
        closedChild.SetActive(true);
        openChild.SetActive(false);
    }
}
