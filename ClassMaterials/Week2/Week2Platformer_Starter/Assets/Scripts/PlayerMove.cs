using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //used for turning off debug messages later
    public bool verboseMessages = true;
    //how fast the player moves left and right
    public float speed = 4.5f;

    private Rigidbody2D body;
    private CapsuleCollider2D capsule;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DoDirectionalMovement();
        DoJumps();
    }


    void DoDirectionalMovement()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        body.linearVelocityX = deltaX;
    }

    public ForceMode2D jumpType;
    public float jumpForce = 5f;
    private bool canDoubleJump = true; 
    void DoJumps()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                canDoubleJump = true;
                body.AddForce(Vector2.up * jumpForce, jumpType);
            }
            else if (canDoubleJump)
            {
                body.linearVelocityY = 0;
                canDoubleJump = false;
                body.AddForce(Vector2.up * jumpForce, jumpType);
            }
        }
    }

    bool IsGrounded()
    {
        Vector3 max = capsule.bounds.max;
        Vector3 min = capsule.bounds.min;

        float offset = 0.1f;
        Vector2 bottomRight = new Vector2(max.x, min.y - offset);
        Vector2 bottomLeft = new Vector2(min.x, min.y - offset);

        //draw line
        Debug.DrawLine(bottomLeft, bottomRight);

        Collider2D hit = Physics2D.OverlapArea(bottomLeft, bottomRight);
        if (verboseMessages && hit != null)
        {
            Debug.Log("I hit " + hit.gameObject.name);
        }

        return hit != null;
    }
}
