using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Player : Character
{
    public float speed = 2.5f;
    public DynamicMoveProvider moveProvider;
    public GameObject locomotion;
    private bool isPoisoned = false; // Indica si el jugador está envenenado
    private Coroutine poisonCoroutine; // Para almacenar la corrutina del veneno
    private float poisonDuration = 10f; // Duración para el veneno con tiempo definido
    private float poisonInterval = 1f; // Intervalo de reducción de salud
    private bool inPoisonArea = false; // Indica si el jugador está dentro de una zona de veneno
    public GameObject[] weapons;
    private bool isDead=false;
    private DamageController damageController;

    public bool IsDead { get { return isDead; } }

    protected override void Awake()
    {
        base.Awake();
        damageController=GetComponent<DamageController>();
    }

    protected override void Update()
    {
        base.Update();
        Run();
        if(InputController.ButtonX()>=0.5){
            TakeDamage(currentHealth);
        }
    }

    // Método para activar el veneno por un tiempo definido (por un ataque externo)
    public void PoisonWithTime(float duration)
    {
        if (!isPoisoned)
        {
            isPoisoned = true;
            poisonDuration = duration;
            poisonCoroutine = StartCoroutine(HandlePoison()); // Iniciar corrutina para veneno
        }
    }

    private void Run(){
        if(InputController.ButtonA()>=0.5){
            moveProvider.moveSpeed=speed*2;
        }
        else
        {
            moveProvider.moveSpeed=speed;
        }
    }

    // Método para activar el veneno por tiempo indefinido (por permanecer en un área)
    public void PoisonIndefinitely()
    {
        if (!isPoisoned)
        {
            isPoisoned = true;
            inPoisonArea = true; // El jugador está dentro de un área venenosa
            poisonCoroutine = StartCoroutine(HandlePoisonIndefinitely()); // Iniciar corrutina para veneno indefinido
        }
    }

    // Método para desactivar el veneno (se puede llamar desde otros lugares)
    public void CurePoison()
    {
        if (isPoisoned)
        {
            isPoisoned = false;
            if (poisonCoroutine != null)
            {
                StopCoroutine(poisonCoroutine); // Detener la corrutina de veneno
            }
        }
    }

    // Corrutina que maneja la reducción de salud por veneno (tiempo definido)
    private IEnumerator HandlePoison()
    {
        float elapsedTime = 0f;
        while (elapsedTime < poisonDuration)
        {
            TakeDamage(1f); // Reducir 1 de salud cada segundo
            elapsedTime += poisonInterval;
            yield return new WaitForSeconds(poisonInterval);
        }

        // Al terminar el veneno, se detiene el efecto
        CurePoison();
    }

    // Corrutina que maneja la reducción de salud por veneno (tiempo indefinido)
    private IEnumerator HandlePoisonIndefinitely()
    {
        while (inPoisonArea)
        {
            TakeDamage(1f); // Reducir 1 de salud cada segundo mientras está envenenado
            yield return new WaitForSeconds(poisonInterval);
        }

        // Al salir del área venenosa, se detiene el veneno
        CurePoison();
    }

    // Método para que el jugador salga de la zona venenosa
    public void ExitPoisonArea()
    {
        inPoisonArea = false;
        CurePoison();
    }
    public bool GetPoisonedStatus(){
        return isPoisoned;
    }
    public override void Die()
    {
        base.Die();
        // Al morir, se detiene el efecto de veneno
        CurePoison();
        DeadMenu.ShowMenu();
        locomotion.SetActive(false);
        enabled=false;
        SetWeaponState(false);
        isDead=true;
        EnemyLifebarUI.ClearEnemy();
    }
    public override void Respawn()
    {
        base.Respawn();
        enabled=true;
        InitializeHealth();
        locomotion.SetActive(true);
        SetWeaponState(true);
        RespawnController.RespawnPlayerInNearestPoint();
        EnemyGameController.Refresh();
        isDead=false;
    }
    private void SetWeaponState(bool state){
        foreach(GameObject weapon in weapons){
            weapon.SetActive(state);
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        damageController.TakeDamage();
    }
}
