using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    [SerializeField] private Sprite zombieSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private enum State
    {
        Villager,
        Zombie
    }

    private NavMeshAgent navMeshAgent;
    private State state;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = State.Villager;
    }

    private void Update()
    {
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        switch (state)
        {
            case State.Villager:
                break;
            case State.Zombie:
                navMeshAgent.SetDestination(player.transform.position);
                break;
        }
    }

    public void SetStateToZombie()
    {
        spriteRenderer.sprite = zombieSprite;
        state = State.Zombie;
    }
}
