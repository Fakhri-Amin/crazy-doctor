using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollisionManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponController>(out WeaponController controller))
        {
            Destroy(other.gameObject);
        }
    }
}