using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationUI : MonoBehaviour
{
    public TMP_InputField nameInputField; // Reference to the Input Field UI element
    public string playerName; // Variable to store the player's name

    // hair choices
    public Item[] hairStyles;
    public Slider hairSlider;

    // hair color
    public Material[] hairColors;
    public Slider hairColorSlider;

    // eye choices
    public Material[] eyeColors;
    public Slider eyeSlider;

    // skin color
    public Material[] skinColors;
    public Slider skinSlider;

    void InitializeSliders()
    {
        hairSlider.maxValue = hairStyles.Length;
        eyeSlider.maxValue = eyeColors.Length;
        skinSlider.maxValue = skinColors.Length;
        hairColorSlider.maxValue = hairColors.Length;
    }

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
        GameManager.instance.playerData.name = playerName;
    }

    public void SetPlayerHair()
    {
        GameManager.instance.playerData.currentHairStyle = hairStyles[(int)hairSlider.value];
        Debug.Log("Hairstyle " + hairSlider.value + " set as player hair style.");
    }

    public void SetPlayerEyeColor()
    {
        GameManager.instance.playerData.eyeColor = eyeColors[(int)eyeSlider.value];
        Debug.Log("EyeColor " + eyeSlider.value + " set as player eye color.");
    }

    public void SetPlayerSkinColor()
    {
        GameManager.instance.playerData.skinColor = skinColors[(int)skinSlider.value];
        Debug.Log("SkinColor " + skinSlider.value + " set as player skin color.");
    }

    public void SetPlayerHairColor()
    {
        GameManager.instance.playerData.hairColor = hairColors[(int)hairColorSlider.value];
        Debug.Log("HairColor " + hairSlider.value + " set as player hari color.");
    }
}
