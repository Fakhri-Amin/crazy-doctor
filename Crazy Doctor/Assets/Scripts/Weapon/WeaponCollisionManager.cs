using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class WeaponCollisionManager : MonoBehaviour
{
    private WeaponController weaponController;

    private void Awake()
    {
        weaponController = GetComponent<WeaponController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.TryGetComponent<TreeCollisionManager>(out TreeCollisionManager env) || other.TryGetComponent<VillagerController>(out VillagerController villager))
        // {
        //     weaponController.SetBehaviourTo(false);
        // }
    }
}
