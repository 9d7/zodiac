using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCountController : MonoBehaviour
{
    [SerializeField] private PlayerControl pc;
    [SerializeField] private TextMeshProUGUI[] charText;
    [SerializeField] private Image[] charImg;

    private void Awake()
    {
        RenderCharacters();
    }

    public void RenderCharacters()
    {
        for(int i = 0; i < pc.characters.Count; i ++)
        {
            charImg[i].sprite = pc.characters[i].image;
            charImg[i].gameObject.SetActive(true);
            charText[i].gameObject.SetActive(true);
        }

        for (int i = pc.characters.Count; i < charText.Length; i++)
        {
            charImg[i].gameObject.SetActive(false);
            charText[i].gameObject.SetActive(false);
        }
    }


    public void UpdateCount(int charIdx, int count)
    {
        //charText[charIdx].text = count.ToString();
    }
}
