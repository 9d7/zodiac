using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnTrigger : MonoBehaviour
{
    public DialogManager.Dialog[] dialogs;


    public void OnTriggerEnter2D(Collider2D other)
    {
        DialogManager.Instance.RunDiag(dialogs);
    }
}
