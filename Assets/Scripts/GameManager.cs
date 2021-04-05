using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl pc;
    public DayNightController dnc;
    public CameraFollowPlayer cfp;
    public static GameManager Instance;
    public bool DoInitialAnim = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        if (DoInitialAnim)
        {
            cfp.WatchSomething(2, 2, TargetsToShow);
        }
        
    }

    public GameObject[] TargetsToShow;

}
