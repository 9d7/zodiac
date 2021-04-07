using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "You Collected " + CounterManager.Instance.count + " out of " + CounterManager.Instance.max + " dragon tokens.";
        Destroy(CounterManager.Instance.gameObject);
    }

   
}
