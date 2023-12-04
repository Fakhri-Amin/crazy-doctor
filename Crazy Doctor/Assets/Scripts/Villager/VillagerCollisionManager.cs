using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponController>(out WeaponController weapon))
        {
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<ZombieController>(out ZombieController zombie))
        {
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
