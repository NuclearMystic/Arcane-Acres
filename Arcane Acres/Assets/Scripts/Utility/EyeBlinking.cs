using System.Collections;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    public SkinnedMeshRenderer headMesh; // Reference to the SkinnedMeshRenderer
    public float minBlinkTime = 2.0f;    // Minimum time between blinks
    public float maxBlinkTime = 5.0f;    // Maximum time between blinks
    public float blinkDuration = 0.1f;   // Duration of the blink

    private Material[] originalMaterials;  // Store the original materials

    void Start()
    {
        if (headMesh == null)
        {
            Debug.LogError("Please assign a SkinnedMeshRenderer in the Inspector.");
            return;
        }

        // Store the original materials
        originalMaterials = headMesh.materials;

        // Start the blinking coroutine
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // Wait for a random amount of time before blinking
            float waitTime = Random.Range(minBlinkTime, maxBlinkTime);
            yield return new WaitForSeconds(waitTime);

            // Blink (disable the second material)
            EnableBlink(true);
            yield return new WaitForSeconds(blinkDuration);

            // End blink (re-enable the second material)
            EnableBlink(false);
        }
    }

    void EnableBlink(bool isBlinking)
    {
        // Create a new array for the modified materials
        Material[] materials = headMesh.materials;

        if (isBlinking)
        {
            // Disable the second material during the blink (set to null)
            materials[1] = null;
        }
        else
        {
            // Restore the original second material after the blink
            materials[1] = originalMaterials[1];
        }

        // Assign the updated materials back to the mesh renderer
        headMesh.materials = materials;
    }
}
