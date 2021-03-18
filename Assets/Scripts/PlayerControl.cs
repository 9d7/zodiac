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
    // Start is called before the first frame update
    void Start()
    {
        charcterNum = characters.Length;
        curCharacterIdx = 0;
          
    }

    // Update is called once per frame
    void Update()
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTransform(InputValue value)
    {
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = (curCharacterIdx + 1) % charcterNum;
        currCharacter = Instantiate(characters[curCharacterIdx], pos, Quaternion.identity);
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }

}
