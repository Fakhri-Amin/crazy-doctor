using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCollisionManager : MonoBehaviour
{
    private VillagerController villagerController;

    private void Awake()
    {
        villagerController = GetComponent<VillagerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponController>(out WeaponController controller))
        {
            villagerController.SetStateToZombie();
        }
    }
}
