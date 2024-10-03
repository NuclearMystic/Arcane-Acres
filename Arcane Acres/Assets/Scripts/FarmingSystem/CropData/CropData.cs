using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Crops/New Crop")]
public class CropData : ScriptableObject
{
    public string cropName;
    public Item item;
    public GameObject[] growthStageObjects;  // GameObjects for each growth stage
    public int daysToMature;  // Days required to fully grow
    public int[] daysToGrowStages;  // Days required to move from each stage (e.g., seed to sprout)
    public int[] waterNeededPerStage;  // Water required for each stage (e.g., 5 units for seed to sprout)

    public bool canRegrow;
}