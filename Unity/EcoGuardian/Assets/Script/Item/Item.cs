using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Item : MonoBehaviour
{
    public float spawnRate = 45f; // Tasa de aparición
    protected Rigidbody rb;
    protected XRGrabInteractable xrGrabInteractable;
    protected bool isGrabbed = false;
    public bool IsGrabbed{get {return isGrabbed;}}

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        xrGrabInteractable = GetComponent<XRGrabInteractable>();

        if (xrGrabInteractable != null)
        {
            xrGrabInteractable.selectEntered.AddListener(OnGrab);
            xrGrabInteractable.selectExited.AddListener(OnRelease);
        }

        SetRigidBodyKinematic(true);
    }

    protected virtual void OnGrab(SelectEnterEventArgs args)
    {
        transform.SetParent(null);
        isGrabbed = true;
    }

    protected virtual void OnRelease(SelectExitEventArgs args)
    {
    }

    public void SetRigidBodyKinematic(bool state)
    {
        if (rb != null)
        {
            rb.isKinematic = state;
        }
    }

    private void OnDestroy()
    {
        if (xrGrabInteractable != null)
        {
            xrGrabInteractable.selectEntered.RemoveListener(OnGrab);
            xrGrabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }
}
