using UnityEngine;

public abstract class TimedEvent : MonoBehaviour
{
    public string triggerTime; // Time at which the event should trigger, in "HH:MM AM/PM" or "HH:MM" format
    private TimeManager timeManager;
    private float triggerNormalizedTime;  // Normalized value of triggerTime

    protected virtual void Start()
    {
        timeManager = TimeManager.Instance;

        if (timeManager == null)
        {
            Debug.LogError("TimeManager not found in the scene.");
        }
        else
        {
            // Convert trigger time from "HH:MM AM/PM" or "HH:MM" format to normalized time (0-1)
            triggerNormalizedTime = ConvertTimeToNormalized(triggerTime);
        }
    }

    protected virtual void Update()
    {
        if (timeManager != null)
        {
            CheckTime();
        }
    }

    private void CheckTime()
    {
        // Get the current normalized time from TimeManager
        float currentNormalizedTime = timeManager.GetCurrentTimeOfDay();

        // If the current time matches the trigger time, execute the event
        if (Mathf.Abs(currentNormalizedTime - triggerNormalizedTime) < 0.01f) // Allow for small floating-point precision error
        {
            OnTimeTriggered();
        }
    }

    // Method to convert "HH:MM AM/PM" or "HH:MM" format to normalized time (0-1)
    private float ConvertTimeToNormalized(string timeString)
    {
        string[] timeParts = timeString.Split(' ');
        string[] hhmm = timeParts[0].Split(':');
        int hours = int.Parse(hhmm[0]);
        int minutes = int.Parse(hhmm[1]);

        if (timeParts.Length > 1) // AM/PM format
        {
            string period = timeParts[1].ToUpper();
            if (period == "PM" && hours != 12) // Convert PM times
            {
                hours += 12;
            }
            else if (period == "AM" && hours == 12) // Convert 12 AM to 0 hours
            {
                hours = 0;
            }
        }

        // Normalize time: hours + (minutes / 60) divided by 24 hours in the day
        return (hours + (minutes / 60f)) / 24f;
    }

    // Method to be overridden by derived classes
    protected abstract void OnTimeTriggered();
}
