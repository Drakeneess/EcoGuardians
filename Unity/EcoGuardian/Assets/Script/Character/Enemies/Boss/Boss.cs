using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Settings")]
    public GameObject canvas;
    public GameObject missionComplete;
    // Define diferentes cooldowns y rangos de ataques
    public float minCooldown = 1.5f;
    public float maxCooldown = 4f;

    private BossPoint[] bossPoints; // Todos los puntos del jefe
    private List<BossPoint> inAreaPoints = new List<BossPoint>(); // Puntos dentro del área
    private List<BossPoint> outAreaPoints = new List<BossPoint>(); // Puntos fuera del área
    private List<Enemy> otherEnemies = new List<Enemy>(); // Lista de otros enemigos (excluyendo al jefe y los puntos)

    private bool isBossActive = false; // Controla si el jefe está activo
    private bool isBattleActive = false;

    private RootController rootController;
    private SpineController spineController;
    protected override void Awake()
    {
        // Encontrar y organizar los puntos del jefe
        bossPoints = FindObjectsOfType<BossPoint>();
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        // Clasificar los puntos y enemigos
        foreach (BossPoint point in bossPoints)
        {
            if (point.GetPointType() == BossPoint.Type.inArea)
            {
                inAreaPoints.Add(point);
            }
            else if (point.GetPointType() == BossPoint.Type.outArea)
            {
                outAreaPoints.Add(point);
            }
        }
        foreach(BossPoint bossPoint in inAreaPoints){
            bossPoint.SetBoss(this);
        }

        foreach (Enemy enemy in allEnemies)
        {
            // Excluir al jefe (this) y los puntos del jefe
            if (enemy != this && !(enemy is BossPoint))
            {
                otherEnemies.Add(enemy);
            }
        }
        canvas.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        rootController=GetComponent<RootController>();
        spineController = GetComponent<SpineController>();
        missionComplete.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        // Comprobar si se cumplen ambas condiciones para activar al jefe
        if (!isBossActive && AreAllOutAreaPointsDestroyed() && EnemyGameController.EnemiesDefeated()>=EnemyGameController.normalEnemies.Count/4)
        {
            ActivateBoss();
        }
        if(player.IsDead && IsBattleActive){
            DesactivateBattleMode();
        }
        // Atacar si está en modo de batalla
        if (IsBattleActive)
        {
            HandleAttack();
        }
    }

    private void HandleAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            Attack();
            lastAttackTime = Time.time;
            AdjustCooldown(); // Cambiar el cooldown dinámicamente
        }
    }
    protected override void Attack()
    {
        // Elegir un ataque aleatorio
        int attackType = Random.Range(0, 3); // Cambia el número según los ataques que tengas

        switch (attackType)
        {
            case 0:
                AttackRoots();
                break;
            case 1:
                AttackSpines();
                break;
            case 2:
                AttackBoth();
                break;
        }
    }
    private void AttackRoots()
    {
        Debug.Log("Ataque de raíces activado!");
        rootController.SpawnRandomRoots();
        // Opcional: agrega animación o efectos visuales aquí
    }

    private void AttackSpines()
    {
        Debug.Log("Ataque de espinas activado!");
        spineController.SpawnRandomSpines();
        // Opcional: agrega animación o efectos visuales aquí
    }

    private void AttackBoth()
    {
        Debug.Log("Ataque combinado de raíces y espinas activado!");
        rootController.SpawnRandomRoots();
        spineController.SpawnRandomSpines();
        // Opcional: agrega animación o efectos visuales aquí
    }

    private void AdjustCooldown()
    {
        // Ajustar el cooldown dinámicamente entre un rango
        attackCooldown = Random.Range(minCooldown, maxCooldown);
    }
    public override void Die()
    {
        isBattleActive=false;
        isBossActive=false;

        canvas.SetActive(false);
        EnemyLifebarUI.ClearEnemy();
        missionComplete.SetActive(true);
    }

    private bool AreAllOutAreaPointsDestroyed()
    {
        // Verificar si todos los puntos fuera del área han sido destruidos
        foreach (BossPoint point in outAreaPoints)
        {
            if (point != null) // Si hay algún punto que aún no ha sido destruido
            {
                return false;
            }
        }
        return true;
    }

    private void ActivateBoss()
    {
        isBossActive = true;

        Debug.Log("El jefe ha sido activado!");
    }
    public override void TakeDamage(float damage)
    {
        if (IsBattleActive)
        {
            base.TakeDamage(damage);
        }
    }
    public void ActivateBattleMode(){
        IsBattleActive = true;
        // Activar animación de puntos del área
        foreach (BossPoint point in inAreaPoints)
        {
            if (point != null)
            {
                point.ActivatePoint();
            }
        }
        InitializeHealth();
        canvas.SetActive(true);
        SoundController.PlayBackgroundMusic(2);
    }
    public void DesactivateBattleMode(){
        IsBattleActive = false;
        // Activar animación de puntos del área
        foreach (BossPoint point in inAreaPoints)
        {
            if (point != null)
            {
                point.Respawn();
                point.DesactivatePoint();
            }
        }
        canvas.SetActive(false);
        SoundController.PlayBackgroundMusic(1);
    }
    protected override void InitializeHealth()
    {
        maxHealt=0;
        foreach(BossPoint point in inAreaPoints){
            maxHealt+=point.maxHealt;
        }
        currentHealth=maxHealt;
    }
    public bool IsBossActive { get { return isBossActive; } }
    public bool IsBattleActive { get { return isBattleActive; } set { isBattleActive = value; } }
    public override void Respawn()
    {
    }
}