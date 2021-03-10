using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectNextAndBack : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectManager;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject prevButton;
    
    private LevelSelectButtonManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = levelSelectManager.GetComponent<LevelSelectButtonManager>();
        
        nextButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            manager.NextPage();
            UpdateButtons();
        });
        
        prevButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            manager.PrevPage();
            UpdateButtons();
        });
        
        UpdateButtons();
    }

    void UpdateButtons()
    {
        nextButton.SetActive(!manager.atMaxPage);
        prevButton.SetActive(!manager.atMinPage);
    }

}
