using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectPool : MonoBehaviour
{
    public static PlayerObjectPool Instance;
    [SerializeField] private WeaponController weaponController;

    private Queue<GameObject> weaponPoolQueue = new();

    private void Awake()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        weaponPoolQueue.Clear();
    }

    public GameObject GetPooledObject()
    {
        if (weaponPoolQueue.Count == 0)
        {
            AddWeaponToPool(1);
        }

        return weaponPoolQueue.Dequeue();
    }

    private void AddWeaponToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (!weaponController) return;

            GameObject bullet = Instantiate(weaponController.gameObject);
            bullet.SetActive(false);
            weaponPoolQueue.Enqueue(bullet);
        }
    }

    public void ReturnToPool(GameObject weapon)
    {
        weapon.SetActive(false);
        weaponPoolQueue.Enqueue(weapon);
    }
}