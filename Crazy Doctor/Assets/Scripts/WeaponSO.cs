using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public GameObject bulletPrefab;
    public float minTimeBetweenAttack;
    public float maxTimeBetweenAttack;
    public float damage;
    public float bulletMoveSpeed;
}
