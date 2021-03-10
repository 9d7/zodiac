using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public int levelNumber;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        GetComponentInChildren<Text>().text = levelNumber.ToString();
        
        button.onClick.AddListener(() =>
        {
            // TODO Async this maybe
            SceneManager.LoadScene("Level" + levelNumber.ToString(), LoadSceneMode.Single);
        });
        
        if (PlayerPrefs.GetInt("maxUnlocked", 1) < levelNumber)
        {
            button.interactable = false;
        }

    }

}
