using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(EntityActions))]
public sealed class PlayerAvatar : MonoBehaviour
{
    [SerializeField] InputActionAsset inputAccess;

    public InputActionAsset InputAsset => inputAccess;

    public EntityActions Actions { get; private set; }
    public PlayerState State { get; private set; }

    bool useMouse;
    Vector2 lastStick;
    Vector2 lastMouse;

    private void Awake()
    {
        Actions = GetComponent<EntityActions>();
    }

    private void OnEnable()
    {
        inputAccess.Enable();
    }

    private void Start()
    {
        ChangeState(PlayerState.Normal, true);
    }

    private void OnDisable()
    {
        inputAccess.Disable();
    }

    private void Update()
    {
        switch (State)
        {
            case PlayerState.Normal:
                Move();
                break;
        }
    }

    private void Move()
    {
        Actions.MoveDirection = InputAsset.FindActionMap("Player").FindAction("Move").ReadValue<Vector2>();
        Actions.FaceDirection = LookDirection();
        Actions.SetAbilityState(0, InputAsset.FindActionMap("Player").FindAction("Shoot").ReadValue<float>() > 0.5f);
    }

    public Vector2 LookDirection()
    {
        var stick = InputAsset.FindActionMap("Player").FindAction("Look").ReadValue<Vector2>();
        var mouse = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (stick != lastStick)
        {
            useMouse = false;
        }
        else if (mouse != lastMouse)
        {
            useMouse = true;
        }

        lastStick = stick;
        lastMouse = mouse;

        if (useMouse) return (mouse - (Vector2)transform.position).normalized;
        else return stick.normalized;
    }

    public void ChangeState(PlayerState state, bool force = false)
    {
        if (State == state && !force) return;

        State = state;
    }

    public enum PlayerState
    {
        Normal
    }
}
