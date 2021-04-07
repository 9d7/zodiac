using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnAwake : MonoBehaviour
{
    public DialogManager.Dialog[] dialog;
    public float waitTime;

    private void Start()
    {
        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(waitTime);
        DialogManager.Instance.RunDiag(dialog);   
    }
}
