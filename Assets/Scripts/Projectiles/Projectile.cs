using System;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float size;
    [SerializeField] float lifetime;
    [SerializeField] LayerMask mask;

    [Space]
    [SerializeField] GameObject destroyPrefab;

    float age;

    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = transform.up * speed;
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.CircleCast(rigidbody.position, size, rigidbody.velocity, rigidbody.velocity.magnitude * Time.deltaTime + 0.1f, mask);
        if (hit)
        {
            if (hit.transform.TryGetComponent(out Health health))
            {
                health.Damage(new Health.DamageArgs(gameObject, damage));
            }

            DestroyWithStyle(hit.point);
        }
    }

    private void DestroyWithStyle() => DestroyWithStyle(rigidbody.position);
    private void DestroyWithStyle(Vector2 point)
    {
        if (destroyPrefab)  Instantiate(destroyPrefab, point, Quaternion.Euler(0.0f, 0.0f, rigidbody.rotation));
        Destroy(gameObject);
    }

    private void Update()
    {
        if (age > lifetime)
        {
            DestroyWithStyle();
        }

        age += Time.deltaTime;
    }
}
