using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    [SerializeField] private float CycleTime = 30;
    [SerializeField] private float AnimTime = 3;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.speed = AnimTime / CycleTime;
    }

    public void SetSpeedMultipler(float mult)
    {
        anim.speed = mult * (AnimTime / CycleTime);
    }

    public void ResetSpeed()
    {
        anim.speed = (AnimTime / CycleTime);
    }

    public void ResetDay()
    {
        anim.SetTrigger("Reset");
    }
}
