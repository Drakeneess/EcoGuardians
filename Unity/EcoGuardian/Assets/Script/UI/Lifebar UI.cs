using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebarUI : MonoBehaviour
{
    public Character character;
    public Transform healthBarTransform; // Reference to the Transform of the health bar
    public DecrementDirection decrementDirection;
    public enum DecrementDirection{
        right,
        up,
        forward
    }

    void Start()
    {
        if (character == null)
        {
            character = GetComponentInParent<Character>();
        }

        if (healthBarTransform == null)
        {
            healthBarTransform = transform; // Assume the health bar is on the same object if not set
        }
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (character == null || healthBarTransform == null) return;

        // Calculate the health percentage
        float healthPercentage = character.GetCurrentHealtPercentage();

        // Update the local scale of the health bar
        Vector3 newScale = healthBarTransform.localScale;
        switch(decrementDirection){
            case DecrementDirection.right:{
                newScale.x = healthPercentage; // Assuming the health bar scales along the x-axis
                break;
            }
            case DecrementDirection.up:{
                newScale.y = healthPercentage; // Assuming the health bar scales along the x-axis
                break;
            }
            case DecrementDirection.forward:{
                newScale.z = healthPercentage; // Assuming the health bar scales along the x-axis
                break;
            }
        }
        healthBarTransform.localScale = newScale;
    }
}
