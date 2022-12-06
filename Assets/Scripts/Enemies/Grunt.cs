using System.Collections;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
public sealed class Grunt : EnemyBase
{
    [SerializeField] float moveDistance;
    [SerializeField] float holdTime;
    [SerializeField] float shootTime;

    private void Start()
    {
        StartCoroutine(Behave());
    }

    private IEnumerator Behave()
    {
        while (true)
        {
            yield return StartCoroutine(Move());
            yield return new WaitForSeconds(holdTime);
            yield return StartCoroutine(Shoot());
        }
    }

    private IEnumerator Move()
    {
        if (!Target) yield break;

        float angle = Random.value * Mathf.PI * 2.0f;
        Vector2 point = (Vector2)Target.transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * moveDistance;

        while (((Vector2)transform.position - point).magnitude > 1.0f)
        {
            MoveDirection = (point - (Vector2)transform.position).normalized;
            FaceDirection = ((Vector2)(Target.transform.position - transform.position)).normalized;

            yield return null;
        }

        MoveDirection = Vector2.zero;
        FaceDirection = ((Vector2)(Target.transform.position - transform.position)).normalized;
    }

    private IEnumerator Shoot()
    {
        if (!Target) yield break;

        FaceDirection = ((Vector2)(Target.transform.position - transform.position)).normalized;

        SetAbilityState(0, true);
        yield return new WaitForSeconds(shootTime);
        SetAbilityState(0, false);
    }
}
