using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 25f;
    [SerializeField] private float rotateSpeed = 300f;
    [SerializeField] private float maxLifeTime = 2f;
    [SerializeField] private MMFeedbacks getHitFeedbacks;
    [SerializeField] private MMFeedbacks returnFeedbacks;
    [SerializeField] private GameObject explodeVFX;

    private enum State
    {
        Active,
        InActive
    }

    private State state;

    private Vector3 direction;
    private float currentLifeTime;
    private bool hasDied;

    private void OnEnable()
    {
        state = State.Active;
        currentLifeTime = maxLifeTime;
        returnFeedbacks.PlayFeedbacks();
        hasDied = false;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Active:
                currentLifeTime -= Time.deltaTime;
                if (currentLifeTime > 0)
                {
                    transform.position += Time.deltaTime * movementSpeed * direction;
                    transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
                }
                else
                {
                    if (!hasDied)
                    {
                        SetBehaviourTo(false);
                        hasDied = true;
                    }
                }
                break;
            case State.InActive:
                break;
        }

    }

    public void SetBehaviourTo(bool stateStatus)
    {
        if (stateStatus)
        {
            state = State.Active;
        }
        else
        {
            state = State.InActive;
            getHitFeedbacks.PlayFeedbacks();
        }
    }

    public void ReturnToPool()
    {
        PlayerObjectPool.Instance.ReturnToPool(gameObject);
    }

    public void MoveToDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}