using UnityEngine;

public class OptionsSettings : ScriptableObject
{
    // sound options
    public float musicVolume = 80f;
    public float sfxVolume = 100f;
    public float ambienceVolume = 100f;

    // preferences
    public float mouseSensitivity = 50f;
    public float fieldOfView;

    // graphics
    public Vector2 resolution;
    public GraphicsLevels graphicsLevel;
}

public enum GraphicsLevels
{
    VeryLow,
    Low,
    Default,
    High,
    VeryHigh
}
