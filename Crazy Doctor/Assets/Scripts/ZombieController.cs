using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private Transform bodyVisual;
    [SerializeField] private float viewDistance = 35f;
    [SerializeField] private LayerMask targetLayerMask;

    public enum State
    {
        Following,
        Attacking
    }

    private State state;
    private NavMeshAgent navMeshAgent;
    private Vector3 targetPosition;
    private float initialStoppingDistance;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnSpaceHit += InputManager_OnSpaceHit;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        initialStoppingDistance = navMeshAgent.stoppingDistance;
    }

    private void InputManager_OnSpaceHit(bool status)
    {
        if (status)
        {
            state = State.Attacking;
        }
        else
        {
            state = State.Following;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        switch (state)
        {
            case State.Following:
                FollowPlayer();
                break;
            case State.Attacking:
                AttackVillager();
                break;
        }
    }

    private void FollowPlayer()
    {
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (!player) return;
        targetPosition = player.transform.position;
        navMeshAgent.stoppingDistance = initialStoppingDistance;
        navMeshAgent.SetDestination(targetPosition);
    }

    private void AttackVillager()
    {
        Collider2D[] overlapZombies = Physics2D.OverlapCircleAll(transform.position, viewDistance, targetLayerMask);

        if (overlapZombies.Length == 0) return;
        Debug.Log(overlapZombies.Length);

        foreach (Collider2D obj in overlapZombies)
        {
            if (obj.TryGetComponent<VillagerCollisionManager>(out VillagerCollisionManager villager))
            {
                targetPosition = villager.transform.position;
                navMeshAgent.stoppingDistance = 1;
                navMeshAgent.SetDestination(targetPosition);
                return;
            }
        }
        return;
    }

    public void Flip()
    {
        if (targetPosition.x < transform.position.x)
        {
            bodyVisual.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            bodyVisual.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
