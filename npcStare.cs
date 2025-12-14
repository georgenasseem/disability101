using UnityEngine;

public class npcStare : MonoBehaviour
{
// The Transform component of the player. 
    // This must be set in the Unity Inspector by dragging the Player GameObject onto this slot.
    public Transform playerTarget;

    // Optional: Controls how quickly the NPC turns to face the player. 
    // A higher value means a faster turn. Use a low value (e.g., 5f) for smoother rotation.
    [Tooltip("The speed at which the NPC rotates to face the player.")]
    public float rotationSpeed = 5f;

    void Update()
    {
        // Check if the player target has been assigned.
        if (playerTarget == null)
        {
            Debug.LogWarning("Player Target is not assigned on " + gameObject.name + ". Please assign it in the Inspector.");
            return;
        }

        // 1. Calculate the direction vector from the NPC to the player.
        // We only care about the horizontal direction (X and Z axes) for standing NPCs.
        Vector3 lookDirection = playerTarget.position - transform.position;
        lookDirection.y = 0; // Prevent the NPC from tilting up or down.

        // 2. Create the desired rotation (Quaternion).
        // Quaternion.LookRotation converts a direction vector into a rotation.
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        // 3. Smoothly rotate the NPC towards the target rotation.
        // Quaternion.Slerp (Spherical Interpolation) is used for smooth rotation over time.
        // We multiply 'rotationSpeed' by 'Time.deltaTime' to make the rotation frame-rate independent.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
