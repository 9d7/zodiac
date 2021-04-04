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

    private bool showEvent;
    public float showEventSec = 2f;
    private float showEventTime;
    private GameObject triggeredEvent;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerControl>();
        showEvent = false;
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
        if (player.currCharacter)
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
}
