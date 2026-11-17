using System.Collections;
using UnityEngine;

public class BossPoint : Enemy
{
    public enum Type
    {
        inArea,
        outArea
    }

    public Type pointType;
    private Boss boss;

    public Type GetPointType()
    {
        return pointType;
    }

    protected override void Awake()
    {
        base.Awake();
        if (pointType == Type.inArea)
        {
            transform.localScale = Vector3.zero; // Aseguramos que comience en 0
        }
    }

    public override void Die()
    {
        if(pointType==Type.outArea){
            Destroy(gameObject);
        }
        else if(pointType==Type.inArea){
            gameObject.SetActive(false);
            if (boss != null && boss.IsBattleActive)
            {
                boss.TakeDamage(maxHealt); // Inflige daño al jefe al destruir este punto
            }
        }
        EnemyLifebarUI.ClearEnemy();
    }
    public override void Respawn()
    {
        base.Respawn();
        if(boss != null && pointType==Type.inArea){
            gameObject.SetActive(true);
            InitializeHealth();
        }
    }

    public void SetBoss(Boss boss)
    {
        this.boss = boss;
    }

    public void ActivatePoint()
    {
        if (pointType == Type.inArea)
        {
            StartCoroutine(ScaleUp(Vector3.one));
        }
    }
    public void DesactivatePoint()
    {
        if (pointType == Type.inArea)
        {
            StartCoroutine(ScaleUp(Vector3.zero));
        }
    }

    private IEnumerator ScaleUp(Vector3 finalSize)
    {
        float duration = 1f; // Duración de la animación
        Vector3 targetScale = finalSize; // Escala final
        Vector3 startScale = transform.localScale; // Escala inicial
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Garantizamos que llegue exactamente al tamaño final
    }
}
