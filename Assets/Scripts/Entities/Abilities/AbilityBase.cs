using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] float cooldown;

    public bool UseState { get; set; }
    public float LastUseTime { get; private set; }

    protected virtual void Use()
    {
        LastUseTime = Time.time;
    }

    private void Update()
    {
        if (UseState && Time.time > LastUseTime + cooldown)
        {
            Use();
        }
    }
}