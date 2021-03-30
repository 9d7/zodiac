using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public GameObject currCharacter;
    private int charcterNum;
    private int curCharacterIdx;
    public CharacterCount[] characters;
    [SerializeField] private CharacterCountController ccc;

    bool activated = false;

    public MainMenu menuControl;
    
    [Serializable]
    public struct CharacterCount

    {
        public GameObject characterPrefab;
        public int count;
        public Sprite image;
    }

    private bool gameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        charcterNum = characters.Length;
        curCharacterIdx = 0;
        menuControl = GameObject.FindObjectOfType<MainMenu>();
        ccc = GameObject.FindObjectOfType<CharacterCountController>();


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

    void OnTransformFirst(InputValue value)
    {
        if (curCharacterIdx == 0 || characters[0].count == 0)
            return;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = 0;
        characters[0].count--;
        
        ccc.UpdateCount(0, characters[0].count);
        currCharacter = Instantiate(characters[curCharacterIdx].characterPrefab, pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }

    void OnTransformSecond(InputValue value)
    {
        if (curCharacterIdx == 1 || characters[1].count == 0)
            return;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = 1;
        characters[1].count--;
        ccc.UpdateCount(1, characters[1].count);
        currCharacter = Instantiate(characters[curCharacterIdx].characterPrefab, pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }


    void OnTransformThird(InputValue value)
    {
        if (curCharacterIdx == 2 || characters.Length < 3 ||  characters[2].count == 0)
            return;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = 2;
        characters[2].count--;
        ccc.UpdateCount(2, characters[2].count);
        currCharacter = Instantiate(characters[curCharacterIdx].characterPrefab, pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }


}
