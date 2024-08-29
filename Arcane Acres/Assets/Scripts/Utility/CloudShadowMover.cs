using UnityEngine;

public class CloudShadowMover : MonoBehaviour
{
    public float speedX = 1f;
    public float speedY = 0.5f;
    public Vector3 startPosition;
    public float loopDistance = 50f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, 0, speedY * Time.deltaTime);

        // Looping the position
        if (Vector3.Distance(startPosition, transform.position) > loopDistance)
        {
            transform.position = startPosition;
        }
    }
}
