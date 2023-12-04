using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieCommandUI : MonoBehaviour
{
    [SerializeField] private TMP_Text commandText;
    void Start()
    {
        InputManager.Instance.OnSpaceHit += InputManager_OnSpaceHit;
    }

    private void InputManager_OnSpaceHit(bool status)
    {
        if (status)
        {
            commandText.text = "Zombie Control Mode : Attacking";
        }
        else
        {
            commandText.text = "Zombie Control Mode : Following";
        }
    }
}
