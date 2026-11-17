using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase base para enemigos patrulladores
public class PatrollerEnemy : Enemy
{
    [SerializeField]
    protected PatrolBehavior patrolBehavior;
    [SerializeField]
    protected GameObject enemyContainer;

    protected override void Awake()
    {
        base.Awake();
        if(patrolBehavior==null){
            patrolBehavior = GetComponent<PatrolBehavior>();
        }
        if(enemyContainer==null){
            enemyContainer=gameObject;
        }
    }

    protected override void Update()
    {
        base.Update();
        patrolBehavior.Patrol();
    }
    public override void Die()
    {
        base.Die();
        enemyContainer.SetActive(false);
    }
    public override void Respawn()
    {
        base.Respawn();
        enemyContainer.SetActive(true);
    }
}
