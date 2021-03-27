using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textContent;
    [SerializeField] private Image actorImage;
    [SerializeField] private CanvasGroup dialogCanvas;
    [SerializeField] private float timeBetweenChars;
    [SerializeField] private float timeBetweenLines;
    [SerializeField] private bool isRunningDialog;

    public static DialogManager Instance;

    private bool shouldSkip;

    private void Awake()
    {
        Instance = this;
        dialogCanvas.alpha = 0;
    }

    [Serializable]
    public class Dialog
    {
        public ActorScriptableObject actor;
        public string content;
    }

    public void RunDiag(Dialog[] dgs)
    {
        if (isRunningDialog)
        {
            Debug.LogError("ALREADY RUNNING DIALOG");
        }
        StartCoroutine(DiagRoutine(dgs));
    }

    public void OnSkip()
    {
        shouldSkip = true;
    }

    private IEnumerator DiagRoutine(Dialog[] dgs)
    {
        dialogCanvas.alpha = 1;
        isRunningDialog = true;
        foreach(Dialog dg in dgs)
        {
            textContent.text = "";
            actorImage.sprite = dg.actor.sprite;
            for (int i = 0; i < dg.content.Length; i++)
            {
                textContent.text += dg.content[i];
                float _t = 0;
                while (_t < timeBetweenChars)
                {
                    yield return new WaitForEndOfFrame();
                    _t += Time.deltaTime;
                    if (shouldSkip)
                    {
                        break;
                    }
                }

                if (shouldSkip)
                {
                    textContent.text = dg.content;
                    shouldSkip = false;
                    break;
                }
            }

            float _tl = 0;
            while (_tl < timeBetweenLines)
            {
                yield return new WaitForEndOfFrame();
                _tl += Time.deltaTime;
                if (shouldSkip)
                {
                    shouldSkip = false;
                    break;
                }
            }
        }

        dialogCanvas.alpha = 0;
        isRunningDialog = false;
    } 
}
