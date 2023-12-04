using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXObjectPool : MonoBehaviour
{
    public static VFXObjectPool Instance;
    [SerializeField] private ParticleSystem explodeVFX;

    private Queue<GameObject> explodeVFXQueue = new();

    private void Awake()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        explodeVFXQueue.Clear();
    }

    public GameObject GetPooledObject()
    {
        if (explodeVFXQueue.Count == 0)
        {
            AddWeaponToPool(1);
        }

        return explodeVFXQueue.Dequeue();
    }

    private void AddWeaponToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (!explodeVFX) return;

            GameObject bullet = Instantiate(explodeVFX.gameObject);
            bullet.SetActive(false);
            explodeVFXQueue.Enqueue(bullet);
        }
    }

    public void ReturnToPool(GameObject weapon)
    {
        weapon.SetActive(false);
        explodeVFXQueue.Enqueue(weapon);
    }
}