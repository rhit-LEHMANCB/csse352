using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject fireBallPrefab;
    public float spawnOffset = 1.0f;
    public float rotationOffset = 0f;

    // Update is called once per frame
    void Update()
    {
        //right click fires
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(fireBallPrefab, transform.position + Vector3.forward*spawnOffset,
                transform.rotation * Quaternion.Euler(0, rotationOffset, 0));//this offset is just to play with angles
        }
    }
}
