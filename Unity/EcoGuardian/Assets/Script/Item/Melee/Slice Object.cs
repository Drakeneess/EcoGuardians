using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public Material crossSectionMaterial;
    public float cutForce=2000;
    public float objectLifeTime=3000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit=Physics.Linecast(startSlicePoint.position,endSlicePoint.position,out RaycastHit hit,sliceableLayer);
        if(hasHit && !hit.transform.CompareTag("Player")){
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }
    public void Slice(GameObject target){
        Vector3 velocity=velocityEstimator.GetVelocityEstimate();
        print(velocity);
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position-startSlicePoint.position,velocity);
        planeNormal.Normalize();

        SlicedHull slicedHull = target.Slice(endSlicePoint.position, planeNormal);
        if(slicedHull!=null){
            GameObject upperHull=slicedHull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull=slicedHull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }
    private void SetupSlicedComponent(GameObject slicedObject)
    {
        // Add a MeshCollider and Rigidbody to the sliced object
        MeshCollider meshCollider = slicedObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);

        // Ensure that the sliced object is assigned to the correct layer
        slicedObject.layer = LayerMask.NameToLayer("Character"); // Replace with your layer name

        Destroy(slicedObject,objectLifeTime);
    }
}
