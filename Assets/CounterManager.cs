using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public static CounterManager Instance;

    public TextMeshProUGUI counterText;

    private int count;

    [SerializeField] private int max;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Instance = this;
        counterText.text = "0/" + max;
    }

    public void Increment()
    {
        counterText.text = (++count) + "/" + max;
    }
}
