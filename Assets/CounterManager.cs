using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public static CounterManager Instance;

    public TextMeshProUGUI counterText;

    public int count;

    [SerializeField] public int max;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            counterText.text = "0/" + max;
        }
            

        
    }

    public void Increment()
    {
        counterText.text = (++count) + "/" + max;
    }

    private void Update()
    {
      
    }
}
