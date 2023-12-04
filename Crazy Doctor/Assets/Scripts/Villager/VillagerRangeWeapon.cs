using System.Collections;
using System.Collections.Generic;
// using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;
// using MoreMountains.Feedbacks;

public class VillagerRangeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float viewDistance = 20f;
    [SerializeField] private LayerMask targetLayerMask;
    // [SerializeField] private MMFeedbacks shootingSFX;

    private Vector3 targetPosition = new(0, 0);
    private Transform closestTarget;
    private Vector3 shootDirection;
    private bool isFlip;
    private Coroutine firingCoroutine;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        HandleFlip();
    }

    void FixedUpdate()
    {
        IsPlayerOrZombieVisible();
    }

    private void RotateToTarget(Vector2 targetPosition)
    {
        targetPosition.x -= transform.position.x;
        targetPosition.y -= transform.position.y;

        float gunAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

        if (isFlip)
        {
            weaponTransform.transform.localScale = new Vector3(-1, 1, 1);
            weaponTransform.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
        }
        else
        {
            weaponTransform.transform.localScale = new Vector3(1, 1, 1);
            weaponTransform.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
        }
    }

    private void HandleFlip()
    {
        if (targetPosition.x < transform.position.x)
        {
            bodyTransform.transform.localScale = new Vector3(-1, 1, 1);
            isFlip = true;
        }
        else
        {
            bodyTransform.transform.localScale = new Vector3(1, 1, 1);
            isFlip = false;
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            StartShooting();

            float minNumber = weaponSO.minTimeBetweenAttack;
            float maxNumber = weaponSO.maxTimeBetweenAttack;
            float timeBetweenAttack = Random.Range(minNumber, maxNumber);

            yield return new WaitForSeconds(timeBetweenAttack);
        }
    }

    private void StartShooting()
    {
        if (!IsPlayerOrZombieVisible()) return;

        GameObject bulletTransform = Instantiate(weaponSO.bulletPrefab, shootPoint.position, shootPoint.rotation);
        _ = bulletTransform.TryGetComponent(out Bullet bullet);
        bullet.Setup(shootPoint.transform.right, weaponSO.damage, weaponSO.bulletMoveSpeed, Bullet.Source.Villager);

        // shootingSFX.PlayFeedbacks();
    }

    public bool IsPlayerOrZombieVisible()
    {
        Collider2D[] overlapZombies = Physics2D.OverlapCircleAll(transform.position, viewDistance, targetLayerMask);

        if (overlapZombies.Length == 0) return false;

        foreach (Collider2D obj in overlapZombies)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, obj.transform.position - transform.position, viewDistance, targetLayerMask);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<ZombieController>(out ZombieController zombie))
                {
                    targetPosition = zombie.transform.position;
                    RotateToTarget(targetPosition);
                    return true;
                }
                else if (hit.collider.TryGetComponent<PlayerMovement>(out PlayerMovement player))
                {
                    targetPosition = player.transform.position;
                    RotateToTarget(targetPosition);
                    return true;
                }
            }
        }
        targetPosition = Vector2.zero;
        return false;
    }
}