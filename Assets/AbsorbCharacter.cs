using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbsorbCharacter : MonoBehaviour
{
    private bool activated = false;
    public GameObject character;

    public Sprite spriteChar;
    public DialogManager.Dialog[] dialog;
    public ParticleSystem absorbtionEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<CharacterMovement_simple>() && !activated)
        {
            StartCoroutine(AbsorbtionAnimation(other.GetComponent<CharacterMovement_simple>()));
        }
    }

    private IEnumerator AbsorbtionAnimation(CharacterMovement_simple cm)
    {
        DialogManager.Instance.RunDiag(dialog);
        activated = true;
        absorbtionEffect.transform.position = cm.transform.position;
        absorbtionEffect.Play();
        cm.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(1);
        GameManager.Instance.pc.AddCharacter(character, spriteChar);
        cm.GetComponent<Rigidbody2D>().simulated = true;
        
        
        absorbtionEffect.Stop();
        
        Destroy(gameObject);
    }
}
