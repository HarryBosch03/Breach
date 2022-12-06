using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
public sealed class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField][Range(0.0f, 1.0f)] float health;

    public void Damage (DamageArgs args)
    {
        health -= args.damage / maxHealth;

        if (health <= 0.0f)
        {
            Die(args);
        }
    }

    public void Die (DamageArgs args)
    {
        Destroy(gameObject);
    }

    [System.Serializable]
    public struct DamageArgs
    {
        public GameObject damager;
        public float damage;

        public DamageArgs(GameObject damager, float damage)
        {
            this.damager = damager;
            this.damage = damage;
        }
    }
}
