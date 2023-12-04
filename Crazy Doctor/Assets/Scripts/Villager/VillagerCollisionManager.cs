using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class VillagerCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zombiePrefabs;
    [SerializeField] private MMFeedbacks getHitFeedbacks;
    [SerializeField] private bool isBoss = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponController>(out WeaponController weapon))
        {
            if (isBoss)
            {
                GameManager.Instance.Win();
            }
            weapon.SetBehaviourTo(false);
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<ZombieController>(out ZombieController zombie))
        {
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
