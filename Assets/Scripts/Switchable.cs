using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchable : MonoBehaviour
{
    private int numberOfSwitches = 0;
    private int timesTriggered = 0;

    private bool _isActive;
    public bool isActive => _isActive;

    public void SwitchRegister()
    {
        numberOfSwitches++;
    }

    public void SwitchPress()
    {
        timesTriggered++;
        if (timesTriggered >= numberOfSwitches && numberOfSwitches > 0 && !_isActive)
        {
            _isActive = true;
            SendMessage("OnSwitchableEnter");
        }
    }

    public void SwitchRelease()
    {
        timesTriggered--;
        if (timesTriggered < numberOfSwitches && numberOfSwitches > 0 && _isActive)
        {
            _isActive = false;
            SendMessage("OnSwitchableExit");
        }
    }
}
