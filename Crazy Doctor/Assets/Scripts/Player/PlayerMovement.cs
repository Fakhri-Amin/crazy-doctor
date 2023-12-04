using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 movementVector;
    private HealthSystemComponent healthSystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystemComponent>();
    }

    private void Start()
    {
        healthSystem.GetHealthSystem().OnDamaged += HealthSystem_OnDamaged;
        // healthSystem.GetHealthSystem().OnDamaged += HealthSystem_OnDamaged;
        healthSystem.GetHealthSystem().OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {

    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        // Instantiate(deadVisual, transform.position, Quaternion.identity);
        GameManager.Instance.Lose();
        Destroy(gameObject);
    }

    private void Update()
    {
        movementVector = InputManager.Instance.GetMovementVectorNormalized();
        // ArenaBoundary.Instance.RestrictObjectPosition(transform);
    }

    private void FixedUpdate()
    {
        rb.velocity = movementVector * movementSpeed;
    }
}