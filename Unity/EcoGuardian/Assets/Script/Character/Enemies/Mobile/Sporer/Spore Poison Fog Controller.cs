using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporePoisonFogController : SporeFogController
{
    private PoisonFog poisonFog;
    protected override void Start()
    {
        base.Start();
        poisonFog=volume.GetComponent<PoisonFog>();
        poisonFog.Initialize(player.GetComponent<Player>());
    }
    protected override void Update()
    {
        base.Update();
    }

    
}
