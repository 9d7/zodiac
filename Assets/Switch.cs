using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PressedEvent : UnityEvent<bool>
{
}

public class Switch : MonoBehaviour
{
    public PressedEvent pressedEvent;
    private bool wasPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!wasPressed)
        {
            pressedEvent.Invoke(true);
        }

        wasPressed = true;
    }
}
