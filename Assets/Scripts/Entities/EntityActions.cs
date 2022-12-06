using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
public class EntityActions : MonoBehaviour
{
    EntityMovement movement;
    AbilityBase[] abilities;

    public Vector2 MoveDirection { get => movement.MoveDirection; set => movement.MoveDirection = value; }
    public Vector2 FaceDirection { get => transform.up; set => transform.up = value; }

    protected virtual void Awake()
    {
        movement = GetComponent<EntityMovement>();
        abilities = GetComponentsInChildren<AbilityBase>(true);
    }

    public virtual void SetAbilityState (int i, bool s)
    {
        if (i < 0 || i >= abilities.Length) return;

        abilities[i].UseState = s;
    }
}
