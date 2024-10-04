using System;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float dayLengthInMinutes = 10f; // Length of a full day in real-time minutes
    public float timeScale = 1f; // Speed of time passing
    public float initialTimeOfDay = 0.5f; // Initial time of day (0 to 1)

    public TMP_Text clockText;
    public bool use24HourFormat = true; // Option to switch between 12-hour and 24-hour formats

    private float dayLengthInSeconds;
    private float currentTimeOfDay; // Current time of day as a fraction (0 to 1)
    private int totalDaysPassed = 0; // Total days passed in the game world

    private float previousMorning = 5f / 24f; // 5 AM normalized in the 0-1 scale
    private bool morningTriggered = false; // To ensure morning logic happens only once per day

    public static event Action OnMorningUpdate; // Event triggered at 5:00 AM

    void Awake()
    {
        // Ensure there is only one instance of TimeManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dayLengthInSeconds = dayLengthInMinutes * 60f;
        currentTimeOfDay = initialTimeOfDay % 1; // Wrap around correctly if time exceeds 1
    }

    void Update()
    {
        // Advance the time based on the current time scale and day length
        currentTimeOfDay += (Time.deltaTime / dayLengthInSeconds) * timeScale;

        // Loop the time back to 0 after 1 full day
        if (currentTimeOfDay >= 1f)
        {
            currentTimeOfDay %= 1f;
            totalDaysPassed++;
            morningTriggered = false; // Reset the morning trigger for the new day
        }

        // Update the UI clock (optional)
        if (clockText != null)
        {
            clockText.text = GetFormattedTimeOfDay();
        }

        // Trigger the morning update if it's 5:00 AM
        if (IsMorningTime() && !morningTriggered)
        {
            TriggerMorningUpdate();
        }

        // Broadcast the time change to other systems (optional shader or lighting effects)
        Shader.SetGlobalFloat("_TimeOfDay", currentTimeOfDay);
    }

    // Check if it's the morning time (5:00 AM)
    private bool IsMorningTime()
    {
        return currentTimeOfDay >= previousMorning && currentTimeOfDay < previousMorning + 0.01f;
    }

    // Trigger the morning update event for crops
    private void TriggerMorningUpdate()
    {
        if (OnMorningUpdate != null)
        {
            OnMorningUpdate();  // Notify any subscribers (e.g., CropManager)
        }
        morningTriggered = true;  // Ensure this only happens once per day
    }

    // Get the formatted time of day (for display purposes)
    public string GetFormattedTimeOfDay()
    {
        int totalMinutes = Mathf.FloorToInt(currentTimeOfDay * 24 * 60);
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        string timeString;

        if (use24HourFormat)
        {
            timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        }
        else
        {
            string period = hours >= 12 ? "PM" : "AM";
            hours = hours % 12;
            if (hours == 0) hours = 12; // Handle midnight as 12 AM
            timeString = string.Format("{0:00}:{1:00} {2}", hours, minutes, period);
        }

        return timeString;
    }

    // Get the total days passed in the game world
    public int GetTotalDaysPassed()
    {
        return totalDaysPassed;
    }

    // Get the current time of day in the 0-1 range
    public float GetCurrentTimeOfDay()
    {
        return currentTimeOfDay;
    }

    // Get the normalized value for 5:00 AM in the 0-1 range
    public float GetPreviousMorning()
    {
        return previousMorning;
    }
}
