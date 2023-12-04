using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action<bool> OnSpaceHit;

    public static InputManager Instance;
    private PlayerInputActions playerInputActions;
    private bool isSpaceHit = false;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    private void Start()
    {
        OnSpaceHit?.Invoke(isSpaceHit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpaceHit = !isSpaceHit;
            OnSpaceHit?.Invoke(isSpaceHit);
        }
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }

    public bool IsMouseLeftPressedDown()
    {
        return Mouse.current.leftButton.wasPressedThisFrame;
    }

    public bool IsMouseLeftPressedUp()
    {
        return Mouse.current.leftButton.wasReleasedThisFrame;
    }
}