using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHopper : MonoBehaviour
{
    // Public variables to control hop and animation sync
    public float hopHeight = 2f;           // Maximum height of the hop
    public float animationDuration = 1f;   // Total time for one hop (up and down), matching the 4-frame animation cycle

    // Store the original Y position
    private float originalY;

    // Track the time within one hop cycle
    private float hopTime = 0f;

    // Use this for initialization
    void Start()
    {
        // Record the sprite's initial Y position
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the time within the hop cycle
        hopTime += Time.deltaTime;

        // If hopTime exceeds the total animation duration, reset it
        if (hopTime > animationDuration)
        {
            hopTime -= animationDuration;
        }

        // Determine the percentage of time within the animation cycle
        float normalizedTime = hopTime / animationDuration;

        // Calculate the Y position based on the normalized time
        // Ascend for the first 25% (1 frame), and descend for the remaining 75% (3 frames)
        float newY;
        if (normalizedTime < 0.25f) // First 25%: Ascending
        {
            float ascendProgress = normalizedTime / 0.25f; // Maps time to 0-1 for ascending
            newY = Mathf.Lerp(originalY, originalY + hopHeight, ascendProgress);
        }
        else // Remaining 75%: Descending
        {
            float descendProgress = (normalizedTime - 0.25f) / 0.75f; // Maps time to 0-1 for descending
            newY = Mathf.Lerp(originalY + hopHeight, originalY, descendProgress);
        }

        // Apply the new Y position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
