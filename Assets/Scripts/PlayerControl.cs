using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private GameObject currCharacter;
    private int charcterNum;
    private int curCharacterIdx;
    public GameObject[] characters;

    bool activated = false;

    private MainMenu menuControl;

    private bool gameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        charcterNum = characters.Length;
        curCharacterIdx = 0;
        menuControl = GameObject.FindObjectOfType<MainMenu>();


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
                currCharacter = Instantiate(characters[curCharacterIdx], pos, Quaternion.identity);
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
        if (curCharacterIdx == 0)
            return;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = 0;
        currCharacter = Instantiate(characters[curCharacterIdx], pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }

    void OnTransformSecond(InputValue value)
    {
        if (curCharacterIdx == 1)
            return;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = 1;
        currCharacter = Instantiate(characters[curCharacterIdx], pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }


    void OnTransformThird(InputValue value)
    {
        if (curCharacterIdx == 2 || characters.Length < 3)
            return;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = 2;
        currCharacter = Instantiate(characters[curCharacterIdx], pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }


}
