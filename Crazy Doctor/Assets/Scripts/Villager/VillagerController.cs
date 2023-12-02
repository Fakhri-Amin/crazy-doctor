using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : MonoBehaviour
{
    [SerializeField] private Sprite zombieSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private enum State
    {
        Villager,
        Zombie
    }

    private State state;

    private void Start()
    {
        state = State.Villager;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Villager:
                break;
            case State.Zombie:
                break;
        }
    }

    public void SetStateToZombie()
    {
        spriteRenderer.sprite = zombieSprite;
        state = State.Zombie;
    }
}
