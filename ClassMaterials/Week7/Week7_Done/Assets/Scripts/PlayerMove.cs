using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public float speed = 10f;
    public float turnSpeed = 90f;

    // Update is called once per frame
    void Update()
    {
        //move forward
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        //turn
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);

    }
}
