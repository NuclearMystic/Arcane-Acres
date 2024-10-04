using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationUI : MonoBehaviour
{
    public TMP_InputField nameInputField; // Reference to the Input Field UI element
    public string playerName; // Variable to store the player's name

    void Start()
    {
        // Add a listener to call the method when the input field is edited
        nameInputField.onEndEdit.AddListener(SetPlayerName);
    }

    // This method is called when the player finishes typing their name
    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log("Player Name set to: " + playerName);
        // You can now save the player name to your game state or use it in character creation
    }
}
