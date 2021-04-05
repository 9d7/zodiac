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
    
    private void Awake()
    {
        Vector3 pos = gameObject.transform.position;
        currCharacter = Instantiate(characters[curCharacterIdx].characterPrefab, pos, Quaternion.identity);
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
            if (currCharacter.transform.position.y < -15)
            {
                Debug.Log("Resetting");
                currCharacter.GetComponent<CharacterMovement_simple>().Reset();
                //menuControl.GameEnd(false);
            }
        }
    }


    public void OnTransformFirst(InputValue value)
    {
        if (curCharacterIdx == 0 || !currCharacter.GetComponent<CharacterMovement_simple>().CanTransform())
            return;
        DoTransformationAnimation(0);
    }

    public void OnTransformSecond(InputValue value)
    {
        if (curCharacterIdx == 1 || characters.Count < 2 || !currCharacter.GetComponent<CharacterMovement_simple>().CanTransform())
            return;
        DoTransformationAnimation(1);
    }


    public void OnTransformThird(InputValue value)
    {
        if (curCharacterIdx == 2 || characters.Count < 3 || !currCharacter.GetComponent<CharacterMovement_simple>().CanTransform())
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
        float originalCamSize = GameManager.Instance.cfp.cam.orthographicSize;
        while (t < 3)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            GameManager.Instance.dnc.SetSpeedMultipler(Mathf.Pow(t + 1, 4));
            GameManager.Instance.cfp.cam.orthographicSize = Mathf.Lerp(originalCamSize, originalCamSize / 1.5f, t / 3);
        }

        t = 0;
        
        
        GameManager.Instance.dnc.SetSpeedMultipler(1);
        GameManager.Instance.dnc.ResetDay();
        Destroy(currCharacter);
        Vector3 pos = currCharacter.transform.position;
        curCharacterIdx = idx;
        Vector3 oldVel = currCharacter.GetComponent<Rigidbody2D>().velocity;
        float oldInput = currCharacter.GetComponent<CharacterMovement>().GetHorizontalInput();
        currCharacter.GetComponent<Rigidbody2D>().simulated = true;
        currCharacter = Instantiate(characters[idx].characterPrefab, pos, Quaternion.identity);
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            GameManager.Instance.cfp.cam.orthographicSize = Mathf.Lerp(originalCamSize / 1.5f, originalCamSize, t  / 1);
        }
        currCharacter.GetComponent<Rigidbody2D>().velocity = oldVel;
        currCharacter.GetComponent<CharacterMovement>().SetHorizontalInput(oldInput);
    }
}
