using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Battery : Item
{
    public float energyCapacity = 100f;        // Capacidad máxima de la batería
    public float energyDrainRate = 3f;        // Tasa de consumo de energía
    public GameObject indicator;             // Indicador visual de energía

    private float actualEnergy;
    private Pistol pistol;

    protected override void Start()
    {
        base.Start();
        actualEnergy = energyCapacity;        // Energía al máximo
        UpdateIndicator();
    }

    protected override void OnGrab(SelectEnterEventArgs args)
    {
        base.OnGrab(args);
        if (pistol != null) // Liberar batería de la pistola
        {
            pistol.ReleaseBattery();
            pistol = null; // Desvincular
        }
    }

    protected override void OnRelease(SelectExitEventArgs args)
    {
        if (pistol == null) // Si no está anclada, comportarse como un objeto suelto
        {
            base.OnRelease(args);
            SetRigidBodyKinematic(false);
        }
    }

    private void Update()
    {
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        if (indicator != null)
        {
            float energyPercentage = actualEnergy / energyCapacity;
            indicator.transform.localScale = new Vector3(1, 1, energyPercentage);
        }
    }

    public void DrainEnergy()
    {
        if (actualEnergy > 0)
        {
            actualEnergy -= energyDrainRate;
            actualEnergy = Mathf.Max(actualEnergy, 0);
        }
    }

    public void Recharge(float amount)
    {
        actualEnergy = Mathf.Clamp(actualEnergy + amount, 0, energyCapacity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Battery Holder"))
        {
            Pistol potentialPistol = other.gameObject.GetComponentInParent<Pistol>();
            if (potentialPistol != null && !potentialPistol.HasBattery())
            {
                AttachToPistol(potentialPistol, other.transform);
            }
        }
    }

    private void AttachToPistol(Pistol targetPistol, Transform holderTransform)
    {
        pistol = targetPistol;
        pistol.SetBattery(this);

        transform.SetParent(holderTransform, true);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(-90, 0, 0);
        transform.localScale = Vector3.one;

        SetRigidBodyKinematic(true); // Desactivar físicas al acoplarse
    }


    public float GetCurrentEnergy() => actualEnergy;
}
