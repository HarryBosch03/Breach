using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
public abstract class EnemyBase : EntityActions
{
    GameObject _target;
    public GameObject Target
    {
        get
        {
            if (!_target)
            {
                var players = FindObjectsOfType<PlayerAvatar>();
                _target = players[Random.Range(0, players.Length)].gameObject;
            }
            return _target;
        }
    }
}
