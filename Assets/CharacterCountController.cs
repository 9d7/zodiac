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
        for(int i = 0; i < pc.characters.Length; i ++)
        {
            charText[i].text = pc.characters[i].count.ToString();
            charImg[i].sprite = pc.characters[i].image;
        }

        for (int i = pc.characters.Length; i < charText.Length; i++)
        {
            charImg[i].gameObject.SetActive(false);
        }
    }


    public void UpdateCount(int charIdx, int count)
    {
        charText[charIdx].text = count.ToString();
    }
}
