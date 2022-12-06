using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class EntityMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;

    new Rigidbody2D rigidbody;

    public Vector2 MoveDirection { get; set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 target = Vector2.ClampMagnitude(MoveDirection, 1.0f) * moveSpeed;

        Vector2 force = (target - rigidbody.velocity) * acceleration;
        rigidbody.velocity += force * Time.deltaTime;
    }
}
