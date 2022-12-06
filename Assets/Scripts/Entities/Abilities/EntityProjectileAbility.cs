using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
[DisallowMultipleComponent]
public sealed class EntityProjectileAbility : AbilityBase
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform muzzle;

    [Space]
    [SerializeField] UnityEvent fireEvent;

    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    protected override void Use()
    {
        Instantiate(projectile, muzzle.position, muzzle.rotation);

        fireEvent?.Invoke();

        base.Use();
    }
}
