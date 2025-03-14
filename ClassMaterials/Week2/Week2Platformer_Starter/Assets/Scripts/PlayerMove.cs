using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //used for turning off debug messages later
    public bool verboseMessages = true;
    //how fast the player moves left and right
    public float speed = 4.5f;

    private Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DoDirectionalMovement();
        DoJumps();
    }


    void DoDirectionalMovement()
    {
        //todo
    }

    void DoJumps()
    {
        //todo
    }

    bool IsGrounded()
    {
        //todo
        return false;
    }
}
