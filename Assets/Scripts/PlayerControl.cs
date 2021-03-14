using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        gameObject.transform.position = new Vector3(-15,-4,0);
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
    }

    void OnTransform(InputValue value)
    {
        Destroy(currCharacter);
        Vector3 pos = gameObject.transform.position;
        curCharacterIdx = (curCharacterIdx + 1) % charcterNum;
        currCharacter = Instantiate(characters[curCharacterIdx], pos, Quaternion.identity);
    }
}
