using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 movementVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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