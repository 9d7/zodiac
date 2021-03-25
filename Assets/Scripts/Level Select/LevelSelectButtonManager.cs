using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonManager : MonoBehaviour
{
    // Whether a level is locked or not is driven by "maxUnlocked" in PlayerPrefs.
    // We can set that to a very high number in MainMenu to get debug access to all levels.

    [Header("Utility")] [SerializeField] private GameObject button;
    [SerializeField] private LevelList levels;

    [Header("Layout")] [SerializeField] private Vector2 marginBetweenButtons;
    [SerializeField] private int buttonsPerRow;
    [SerializeField] private int rowsPerPage;
    [SerializeField] private bool centerNonFullRow;
    [SerializeField] private bool centerNonFullCol;

    private int numberOfLevels;
    private int currentPage = 0;
    private int maxPage;
    private int buttonsPerPage;

    private List<GameObject> buttons = new List<GameObject>();

    public bool atMaxPage => currentPage >= maxPage;
    public bool atMinPage => currentPage <= 0;

    void Awake()
    {
        buttonsPerPage = buttonsPerRow * rowsPerPage;
        maxPage = (numberOfLevels - 1) / buttonsPerPage;
        numberOfLevels = levels.scenes.Count;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Spawn and place buttons
        Vector2 buttonSize = button.GetComponent<RectTransform>().rect.size;
        
        int minNonFullRow = (numberOfLevels / buttonsPerRow) * buttonsPerRow;
        int minNonFullCol = (numberOfLevels / buttonsPerPage) * buttonsPerPage;
        
        for (int i = 0; i < numberOfLevels; i++)
        {
            GameObject newButton = Instantiate(button, transform, false);
            newButton.GetComponent<LevelSelectButton>().levelNumber = i + 1;
            newButton.GetComponent<LevelSelectButton>().scene = levels.scenes[i];

            int page = i / buttonsPerPage;
            int indexInPage = i % buttonsPerPage;
            
            int row = indexInPage / buttonsPerRow;
            int col = indexInPage % buttonsPerRow;

            int buttonsInThisRow = buttonsPerRow;
            if (centerNonFullRow && i >= minNonFullRow)
            {
                buttonsInThisRow = numberOfLevels - minNonFullRow;
            }

            int buttonsInThisCol = rowsPerPage;
            if (centerNonFullCol && i >= minNonFullCol)
            {
                buttonsInThisCol = (numberOfLevels - minNonFullCol + (buttonsPerRow - 1)) / buttonsPerRow;
            }

            float xPos = (col - (buttonsInThisRow - 1) / 2.0f) * (buttonSize.x + marginBetweenButtons.x);
            float yPos = - (row - (buttonsInThisCol - 1) / 2.0f) * (buttonSize.y + marginBetweenButtons.y);

            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f);
            if (page != currentPage)
            {
                newButton.SetActive(false);
            }
            
            buttons.Add(newButton);

        }
        
    }

    void ReloadButtons()
    {
        for (int i = 0; i < numberOfLevels; i++)
        {
            buttons[i].SetActive( (i / buttonsPerPage) == currentPage );
        }
    }

    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            ReloadButtons();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ReloadButtons();
        }
    }
}
