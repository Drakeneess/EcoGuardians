using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.TryGetComponent(out Player player)){
            player.PoisonWithTime(3);
        }
    }
}
