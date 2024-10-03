using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public GameObject genderPrefab;
    public Material eyeColor;
    public Material skinColor;
}


