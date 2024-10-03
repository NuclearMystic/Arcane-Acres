using UnityEngine;

public interface ICropState
{
    void EnterState(Crop crop);  // Called when entering this state
    void UpdateGrowth(Crop crop);  // Called to update the crop's growth
}

public class SeedState : ICropState
{
    public void EnterState(Crop crop)
    {
        Debug.Log("Entering Seed State");
        crop.UpdateVisuals();
    }

    public void UpdateGrowth(Crop crop)
    {
        // Check if the crop can grow to the next stage
        if (crop.CanGrowToNextStage())
        {
            crop.SetState(new SproutState());
            crop.ResetGrowthStage();
        }
        else
        {
            Debug.Log("Not enough water or days passed for Seed to grow.");
        }
    }
}

public class SproutState : ICropState
{
    public void EnterState(Crop crop)
    {
        Debug.Log("Entering Sprout State");
        crop.UpdateVisuals();
    }

    public void UpdateGrowth(Crop crop)
    {
        // Check if the crop can grow to the next stage
        if (crop.CanGrowToNextStage())
        {
            crop.SetState(new MatureState());
            crop.ResetGrowthStage();
        }
        else
        {
            Debug.Log("Not enough water or days passed for Sprout to grow.");
        }
    }
}

public class MatureState : ICropState
{
    public void EnterState(Crop crop)
    {
        Debug.Log("Entering Mature State");
        crop.UpdateVisuals();
    }

    public void UpdateGrowth(Crop crop)
    {
        // Check if the crop can grow to the next stage (harvest)
        if (crop.CanGrowToNextStage())
        {
            crop.SetState(new HarvestableState());
            crop.ResetGrowthStage();
        }
        else
        {
            Debug.Log("Not enough water or days passed for Mature to grow.");
        }
    }
}

public class HarvestableState : ICropState
{
    public void EnterState(Crop crop)
    {
        Debug.Log("Entering Harvested State");
        crop.UpdateVisuals();
    }

    public void UpdateGrowth(Crop crop)
    {
        // The crop is harvested and doesn't grow further
        // After harvesting, check if the crop can regrow
        if (crop.CanRegrow())
        {
            // Regrow logic: revert to mature stage
            Debug.Log("Harvested crop will regrow.");
            crop.ResetForRegrowth();
        }
        else
        {
            // No regrowth, final stage
            Debug.Log("Harvested crop will not regrow.");
            crop.DestroyCrop();
            //crop.SetState(new FinalHarvestedState());  // Final harvested state, no further growth
        }
    }
}

