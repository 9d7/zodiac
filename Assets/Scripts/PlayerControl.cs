using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public GameObject currCharacter;
    private int charcterNum;
    private int curCharacterIdx;
    public List<CharacterCount> characters;
    [SerializeField] private CharacterCountController ccc;

    bool activated = false;

    public MainMenu menuControl;
    
    [Serializable]
    public struct CharacterCount

    {
        CharacterCount(GameObject cp, int c, Sprite i)
        {
            characterPrefab = cp;
            count = c;
            image = i;
        }
        public GameObject characterPrefab;
        public int count;
        public Sprite image;
    }

    private bool gameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        charcterNum = characters.Count;
        curCharacterIdx = 0;
        menuControl = GameObject.FindObjectOfType<MainMenu>();
        ccc = GameObject.FindObjectOfType<CharacterCountController>();


    }

    public void AddCharacter(GameObject character, Sprite img)
    {
        CharacterCount cc = new CharacterCount();
        cc.characterPrefab = character;
        cc.count = 0;
        cc.image = img;
        characters.Add(cc);
        ccc.RenderCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd)
        {
            if (!activated)
            {
                activated = true;
                Vector3 pos = gameObject.transform.position;
                currCharacter = Instantiate(characters[curCharacterIdx].characterPrefab, pos, Quaternion.identity);
            }
            else
            {
                gameObject.transform.position = currCharacter.transform.position;
            }
            if (gameObject.transform.position.y < -15)
            {
                gameEnd = true;
                menuControl.GameEnd(false);
            }
        }
    }

    public void OnTransformFirst(InputValue value)
    {
        if (curCharacterIdx == 0)
            return;
        DoTransformationAnimation(0);
    }

    public void OnTransformSecond(InputValue value)
    {
        if (curCharacterIdx == 1 || characters.Count < 2)
            return;
        DoTransformationAnimation(1);
    }


    public void OnTransformThird(InputValue value)
    {
        if (curCharacterIdx == 2 || characters.Count < 3)
            return;
        
        
        DoTransformationAnimation(2);
        
    }
    
    

    public void DoTransformationAnimation(int idx)
    {
        StartCoroutine(TransformationEffect(idx));
    }

    public IEnumerator TransformationEffect(int idx)
    {
        currCharacter.GetComponent<Rigidbody2D>().simulated = false;
        float t = 0;
        while (t < 10)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            GameManager.Instance.dnc.SetSpeedMultipler(Mathf.Pow(t + 1, 2));
        }
        
        GameManager.Instance.dnc.SetSpeedMultipler(1);
        GameManager.Instance.dnc.ResetDay();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = 2;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        currCharacter.GetComponent<Rigidbody2D>().simulated = true;
        currCharacter = Instantiate(characters[idx].characterPrefab, pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }
}
