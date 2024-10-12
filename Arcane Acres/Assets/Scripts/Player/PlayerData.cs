using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    // Player information
    public string playerName;

    // Player's looks
    public GameObject genderPrefab;
    public Material eyeColor;
    public Material skinColor;
    public Material hairColor;
    public Item currentHairStyle;
  
    public Item[] currentEquipment;
}


