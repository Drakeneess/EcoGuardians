using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Seed : Item
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnGrab(SelectEnterEventArgs args)
    {
        base.OnGrab(args);
    }
    protected override void OnRelease(SelectExitEventArgs args)
    {
        base.OnRelease(args);
        SetRigidBodyKinematic(false);
    }
}
