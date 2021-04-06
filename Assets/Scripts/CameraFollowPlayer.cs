using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private float MaxDistance;

    [SerializeField] private float FollowSpeed;

    [SerializeField] private PlayerControl player;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor = 5;

    private Animator anim;

    public Camera cam;
    private bool ShouldFollow;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ShouldFollow = true;
        transform.position = player.transform.position + offset;
        FadeIn();

    }

    private bool showEvent;
    public float showEventSec = 2f;
    private float showEventTime;
    private GameObject triggeredEvent;

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
    
    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!showEvent)
        {
            FollowPlayer();
        } else
        {
            FollowEvent(triggeredEvent);
            showEventTime -= Time.fixedDeltaTime;
            if(showEventTime < 0)
            {
                showEvent = false;
            }
        }
    }

    void FollowPlayer()
    {
        if (player.currCharacter && ShouldFollow)
        {
            Vector3 targetPosition = player.currCharacter.transform.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }

    void FollowEvent(GameObject obj)
    {
        if (obj)
        {
            Vector3 targetPosition = obj.transform.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }

    public void showEventFunc(GameObject obj)
    {
        triggeredEvent = obj;
        showEvent = true;
        showEventTime = showEventSec;
    }

    public void WatchSomething(float goTime, float stayTime, GameObject[] objects)
    {
        StopAllCoroutines();
        StartCoroutine(DoWatchRoutine(goTime, stayTime, objects));
    }

    IEnumerator DoWatchRoutine(float goTime, float stayTime, GameObject[] objects)
    {
        player.currCharacter.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(1);
        ShouldFollow = false;
        Vector3 initPos = transform.position;
        foreach (GameObject obj in objects)
        {
            Vector3 startPos = transform.position;
            Vector3 goalPos = obj.transform.position + offset;
            for (float _t = 0; _t < goTime; _t += Time.deltaTime)
            {
                transform.position = Vector3.Slerp(startPos, goalPos, _t / goTime);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(stayTime);
        }

        Vector3 lastPos = transform.position;
        for (float _t = 0; _t < goTime; _t += Time.deltaTime)
        {
            transform.position = Vector3.Slerp(lastPos, player.currCharacter.transform.position + offset, _t / goTime);
            yield return new WaitForEndOfFrame();
        }
        player.currCharacter.GetComponent<Rigidbody2D>().simulated = true;
        ShouldFollow = true;
    }
    
    
}
