using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCollisionManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Destroy(gameObject);
        }
    }
}
