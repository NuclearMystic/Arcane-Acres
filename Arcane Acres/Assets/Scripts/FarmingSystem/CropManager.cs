using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public static CropManager Instance { get; private set; }

    private List<Crop> activeCrops = new List<Crop>();  // List of all planted crops

    void Awake()
    {
        // Ensure there is only one instance of CropManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        TimeManager.OnMorningUpdate += UpdateAllCrops;  // Subscribe to the morning event
    }

    void OnDisable()
    {
        TimeManager.OnMorningUpdate -= UpdateAllCrops;  // Unsubscribe when disabled
    }

    void UpdateAllCrops()
    {
        foreach (Crop crop in activeCrops)
        {
            crop.UpdateGrowth();  // Tell each crop to update its growth
        }
    }

    // Register a crop when it is planted
    public void RegisterCrop(Crop crop)
    {
        if (!activeCrops.Contains(crop))
        {
            activeCrops.Add(crop);
            Debug.Log(crop + " has been registered in the crop manager.");
        }
    }

    // Unregister a crop if it's harvested or removed
    public void UnregisterCrop(Crop crop)
    {
        if (activeCrops.Contains(crop))
        {
            activeCrops.Remove(crop);
            Debug.Log(crop + " has been removed from the crop manager.");
        }
    }
}
