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

    //note that this force depends on the mass of the RigidBody2D
    public float jumpForce = 5f;
    //making this accessible so we can compare the different input modes
    public ForceMode2D jumpType = ForceMode2D.Impulse;
    //used for double jupming
    //serialized just so we can see it in the editor as we debug
    [SerializeField] private int _jumpCount = 0;

    void DoJumps()
    {
        bool canJump = false;
        //checking groudned fixes "kirby mode"
        //adding in the Y velocity check fixes 2 bugs
        // 1 - Hitting the side of a platform wont reset jumps
        // 2 - "jump count" is reset a few frames after jumping as
        //      we look for the platform a few units *below* us
        //      that line is still inside the platform until we move up a bit
        if (IsGrounded() && body.linearVelocityY < 0.01f)
            _jumpCount = 0;

        //makes it so we can only jump twice before hitting ground again
        if (_jumpCount < 2)
            canJump = true;

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, jumpType);
            _jumpCount += 1;
        }
    }

    bool IsGrounded()
    {
        //find the coordinates of the area under the player
        Vector3 max = capsule.bounds.max;
        Vector3 min = capsule.bounds.min;

        //a small offset to look just "below" our player
        float offset = 0.1f;
        Vector2 bottomRight = new Vector2(max.x, min.y - offset);
        Vector2 bottomLeft  = new Vector2(min.x, min.y - offset);

        //draw a debug line under the player in the editor
        Debug.DrawLine(bottomLeft, bottomRight);

        //Check if any physics objects are in the area of the line
        Collider2D hit = Physics2D.OverlapArea(bottomLeft, bottomRight);

        if(verboseMessages && hit != null)
            Debug.Log("I hit " + hit.gameObject.name);

        //return true if there is anything under us
        return hit != null;
    }
}
