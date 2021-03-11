using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelPortal : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    private float negativeDistance = 0.1f;

    private Collider2D _collider;
    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // check if correctly-sized character is in portal

        Bounds bounds = _collider.bounds;
        Vector2[] positivePoints =
        {
            new Vector2(bounds.extents.x, 0f),
            new Vector2(-bounds.extents.x, 0f),
            new Vector2(0f, bounds.extents.y),
            new Vector2(0f, -bounds.extents.y)
        };
        Vector2 center = new Vector2(bounds.center.x, bounds.center.y);

        foreach (Vector2 vec in positivePoints)
        {
            if (Physics2D.OverlapCircleAll(vec + center, negativeDistance / 2f, whatIsPlayer).Length == 0)
            {
                return;
            }

            if (Physics2D.OverlapCircleAll(vec + vec.normalized * negativeDistance + center, negativeDistance / 2f,
                whatIsPlayer).Length > 0)
            {
                return;
            }
        }

        NextLevel();
    }

    void NextLevel()
    {
        // TODO async this maybe
        string sceneName = SceneManager.GetActiveScene().name;
        int levelNumber = Int32.Parse(sceneName.Substring(sceneName.Length - 1)) + 1;

        if (PlayerPrefs.GetInt("maxUnlocked", 1) < levelNumber)
        {
            PlayerPrefs.SetInt("maxUnlocked", levelNumber);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("Level" + levelNumber.ToString(), LoadSceneMode.Single);
    }

}
