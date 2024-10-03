using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropData cropData;  // Data specific to this crop type
    private int growthStage = 0;  // Tracks the current growth stage
    private int daysInCurrentStage = 0;  // Days spent in the current stage
    private int waterReceived = 0;  // Water received for the current stage

    // State management
    private ICropState currentState;  // Reference to the current state of the crop

    void Start()
    {
        // Set the initial state to SeedState when the crop is first planted
        SetState(new SeedState());
        CropManager.Instance.RegisterCrop(this);  // Register the crop in the CropManager
    }

    public void DestroyCrop()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        CropManager.Instance.UnregisterCrop(this);  // Unregister the crop if it's destroyed
    }

    // Call this method to set a new state for the crop
    public void SetState(ICropState newState)
    {
        currentState = newState;
        currentState.EnterState(this);  // Call the EnterState method for the new state
    }

    // This method is called by the CropManager in the morning
    public void UpdateGrowth()
    {
        daysInCurrentStage++;  // Increment the days in the current stage
        currentState.UpdateGrowth(this);  // Delegate growth logic to the current state
        UpdateVisuals();
    }

    // Method to add water to the crop
    public void WaterCrop(int amount)
    {
        waterReceived += amount;
        Debug.Log("Crop received water. Current water: " + waterReceived);
    }

    // Method to check if the crop has met the growth conditions
    public bool CanGrowToNextStage()
    {
        int requiredWater = cropData.waterNeededPerStage[growthStage];
        int requiredDays = cropData.daysToGrowStages[growthStage];

        // Check if the crop has received enough water and spent enough days in the current stage
        return waterReceived >= requiredWater && daysInCurrentStage >= requiredDays;
    }

    public void ResetGrowthStage()
    {
        daysInCurrentStage = 0;  // Reset the day counter when transitioning to the next stage
        waterReceived = 0;  // Reset the water counter for the new stage
    }

    public void UpdateVisuals()
    {
        // Logic to update the crop's appearance based on growth stage
        Debug.Log("Updating visuals for growth stage: " + currentState.GetType().Name);
    }

    public bool IsHarvestable()
    {
        // Check if the current state is HarvestableState
        return currentState is HarvestableState;
    }

    // Method to forcefully advance the crop to the next stage (for testing purposes)
    public void ForceAdvanceStage()
    {
        if (currentState is SeedState)
        {
            SetState(new SproutState());
        }
        else if (currentState is SproutState)
        {
            SetState(new MatureState());
        }
        else if (currentState is MatureState)
        {
            SetState(new HarvestableState());  // This is your final harvestable stage
        }
        Debug.Log("Crop forcefully advanced to: " + currentState.GetType().Name);
    }

    public bool CanRegrow()
    {
        return cropData.canRegrow;
    }

    // Reset growth stage and water for regrowth
    public void ResetForRegrowth()
    {
        daysInCurrentStage = 0;
        waterReceived = 0;
        SetState(new MatureState());
        Debug.Log("Crop reverted back to MatureState for regrowth.");
    }
}
