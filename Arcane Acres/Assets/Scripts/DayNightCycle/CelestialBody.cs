using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Transform sunTransform;
    public Transform moonTransform;
    public Light sunLight;
    public Light moonLight;

    public Gradient sunColor;
    public Gradient moonColor;

    public AnimationCurve sunIntensity;
    public AnimationCurve moonIntensity;

    private TimeManager timeManager;
    private float blendFactor;

    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        float time = timeManager.GetCurrentTimeOfDay();

        // Adjust time for slower sunset
        float adjustedTime = time;

        if (time > 0.5f && time < 0.75f) // Between noon and 6 PM
        {
            adjustedTime = 0.5f + ((time - 0.5f) * 0.5f); // Slow down the transition between noon and 6 PM
        }
        else if (time >= 0.75f) // After 6 PM
        {
            adjustedTime = 0.625f + ((time - 0.75f) * 1.5f); // Speed up the transition after 7 PM
        }

        // Rotate the sun and moon
        sunTransform.rotation = Quaternion.Euler(new Vector3((adjustedTime * 360f) - 90f, 170f, 0f));
        moonTransform.rotation = Quaternion.Euler(new Vector3((adjustedTime * 360f) - 270f, 170f, 0f));

        // Change sun and moon light color and intensity
        sunLight.color = sunColor.Evaluate(adjustedTime);
        moonLight.color = moonColor.Evaluate(adjustedTime);

        sunLight.intensity = sunIntensity.Evaluate(adjustedTime);
        moonLight.intensity = moonIntensity.Evaluate(adjustedTime);

        // Calculate blend factor for the transition
        if (adjustedTime < 0.25f) // Before sunrise
        {
            blendFactor = Mathf.Clamp01((0.25f - adjustedTime) * 4);
        }
        else if (adjustedTime > 0.75f) // After sunset
        {
            blendFactor = Mathf.Clamp01((adjustedTime - 0.75f) * 4);
        }
        else
        {
            blendFactor = 0;
        }

        // Enable or disable sun and moon lights based on time of day
        if (adjustedTime < 0.25f || adjustedTime > 0.75f) // Before sunrise or after sunset
        {
            sunLight.enabled = false;
            moonLight.enabled = true;
        }
        else
        {
            sunLight.enabled = true;
            moonLight.enabled = false;
        }

        // Adjust ambient light intensity
        RenderSettings.ambientIntensity = Mathf.Lerp(0.5f, 1.0f, 1 - blendFactor);
    }

}
