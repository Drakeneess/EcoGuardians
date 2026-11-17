using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy Info")]
    public string enemyName="";
    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;

    protected float lastAttackTime;
    protected Player player;
    [SerializeField]
    protected Animator animator;
    protected bool stunned = false;
    protected bool hasBeenStunned = false; // Nueva variable para controlar si ya ha sido aturdido
    protected bool isDead = false;
    protected SeedPoint seedPoint;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if(animator==null){
            animator = GetComponent<Animator>();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        isDead = true;

        seedPoint=SeedPointController.SpawnSeedPoint(transform.position);
        
        gameObject.SetActive(false);
        EnemyLifebarUI.ClearEnemy();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        EnemyLifebarUI.SetEnemy(this);
    }

    protected virtual void Attack()
    {   
    }
    public Player GetPlayer(){
        return player;
    }
    public override void Respawn()
    {
        if(isDead && seedPoint!=null){
            if(!seedPoint.GetSeeded()){
                base.Respawn();
                gameObject.SetActive(true);
                Destroy(seedPoint.gameObject);
                InitializeHealth();
            }
        }
    }
}
