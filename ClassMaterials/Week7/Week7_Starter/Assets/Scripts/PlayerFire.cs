using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float rotationOffset = 0f;
    public float spawnOffset = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Check if the left mouse button is pressed
        {
            Instantiate(fireballPrefab, transform.position * spawnOffset, transform.rotation * Quaternion.Euler(0, rotationOffset, 0)); // Create a fireball at the player's position and rotation
        }
    }
}
