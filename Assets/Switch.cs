using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public bool isButton;
    public List<GameObject> objectsToTrigger;
    public bool requiresSpecificCharacter;
    public string specificCharacterName;

    private bool isTouching = false;

    private void Start()
    {
        foreach (GameObject gameObject in objectsToTrigger)
        {
            gameObject.SendMessage("SwitchRegister");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (requiresSpecificCharacter && !other.gameObject.CompareTag(specificCharacterName)) return;

        if (!isTouching)
        {
            foreach (GameObject gameObject in objectsToTrigger)
            {
                gameObject.SendMessage("SwitchPress");
            }

            isTouching = true;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (requiresSpecificCharacter && !other.gameObject.CompareTag(specificCharacterName)) return;
        if (!isButton) return;

        if (isTouching)
        {
            foreach (GameObject gameObject in objectsToTrigger)
            {
                gameObject.SendMessage("SwitchRelease");
            }

            isTouching = false;
        }
    }
}
