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

    void Awake()
    {
        // Ensure that there is only one instance of TimeManager
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
        currentTimeOfDay = initialTimeOfDay % 1; // Ensure it wraps around correctly
    }

    void Update()
    {
        currentTimeOfDay += (Time.deltaTime / dayLengthInSeconds) * timeScale;
        currentTimeOfDay %= 1; // Loop back to 0 after 1

        // Update the UI text element
        if (clockText != null)
        {
            clockText.text = GetFormattedTimeOfDay();
        }

        // Broadcast the time change to other scripts
        Shader.SetGlobalFloat("_TimeOfDay", currentTimeOfDay);
    }

    public float GetCurrentTimeOfDay()
    {
        return currentTimeOfDay;
    }

    public string GetFormattedTimeOfDay()
    {
        // Convert time of day (0 to 1) to hours and minutes
        int totalMinutes = Mathf.FloorToInt(currentTimeOfDay * 24 * 60);
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        string timeString;

        if (use24HourFormat)
        {
            // Format time as HH:MM in 24-hour format
            timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        }
        else
        {
            // Format time as HH:MM AM/PM in 12-hour format
            string period = hours >= 12 ? "PM" : "AM";
            hours = hours % 12;
            if (hours == 0) hours = 12; // Convert 0 hours to 12 for 12-hour format
            timeString = string.Format("{0:00}:{1:00} {2}", hours, minutes, period);
        }

        return timeString;
    }

    public int GetTotalMinutesOfDay(string formattedTime)
    {
        string[] parts = formattedTime.Split(' ');
        string[] timeParts = parts[0].Split(':');
        int hours = int.Parse(timeParts[0]);
        int minutes = int.Parse(timeParts[1]);

        if (parts.Length > 1) // 12-hour format with AM/PM
        {
            if (parts[1] == "PM" && hours != 12)
            {
                hours += 12;
            }
            else if (parts[1] == "AM" && hours == 12)
            {
                hours = 0;
            }
        }

        return hours * 60 + minutes;
    }
}
