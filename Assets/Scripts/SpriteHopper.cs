using UnityEngine;

public class SpriteHopper : MonoBehaviour
{
    public float hopHeight = 2f;        // Maximum height of the hop
    public float hopSpeed = 1f;         // Horizontal movement speed
    public Animator animator;           // Reference to the Animator component

    private float originalY;            // Store the original Y position

    // Use this for initialization
    void Start()
    {
        // Record the sprite's initial Y position (the ground level)
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal movement input (left/right arrow keys or A/D keys)
        float moveInput = Input.GetAxis("Horizontal");

        // Move the sprite horizontally based on input and hopSpeed
        float newX = transform.position.x + moveInput * hopSpeed * Time.deltaTime;

        // Get the normalized time of the current animation (0 to 1)
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime % 1;  // Ensure it's a value between 0 and 1

        // Calculate the Y position based on the normalized time
        float newY;
        if (normalizedTime < 0.5f) // First 50% of the animation time: Ascend
        {
            float ascendProgress = normalizedTime / 0.5f; // Maps 0-50% to 0-1 for ascending
            newY = Mathf.Lerp(originalY, originalY + hopHeight, ascendProgress);
        }
        else // Last 50% of the animation time: Descend
        {
            float descendProgress = (normalizedTime - 0.5f) / 0.5f; // Maps 50%-100% to 0-1 for descending
            newY = Mathf.Lerp(originalY + hopHeight, originalY, descendProgress);
        }

        // Apply the new X (horizontal) and Y (vertical) position
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}