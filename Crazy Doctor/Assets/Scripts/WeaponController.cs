using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using MoreMountains.Feedbacks;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 25f;
    [SerializeField] private float rotateSpeed = 300f;
    [SerializeField] private float maxLifeTime = 2f;

    // [SerializeField] private MMFeedbacks hitWallFeedbacks;

    private Vector3 direction;
    private float currentLifeTime;

    private void OnEnable()
    {
        currentLifeTime = maxLifeTime;
    }

    private void Update()
    {
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime > 0)
        {
            transform.position += Time.deltaTime * movementSpeed * direction;
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }
        else
        {
            PlayerObjectPool.Instance.ReturnToPool(gameObject);
        }

    }

    public void MoveToDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}