using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnAwake : MonoBehaviour
{
    public DialogManager.Dialog[] dialog;

    private void Start()
    {
      DialogManager.Instance.RunDiag(dialog);   
    }
}
