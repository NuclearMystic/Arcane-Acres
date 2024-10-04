using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public PlayerData playerData;
    public OptionsSettings optionsSettings;

    void Awake()
    {
        // Ensure there is only one instance of GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        if(playerData == null)
        {
            playerData = new PlayerData();
        }
        if(optionsSettings == null)
        {
            optionsSettings = new OptionsSettings();
        }
        ToggleCursorLock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCursorLock()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
