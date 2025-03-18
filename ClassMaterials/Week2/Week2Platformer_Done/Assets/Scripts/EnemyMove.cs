using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool verboseMessages = true;

    enum MoveType { Left, Right, Random, Seek};
    [SerializeField] MoveType _currentMoveType;

    public float speed = 2f;

    CircleCollider2D _collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public float rayRotation = 30;
    // Update is called once per frame
    void Update()
    {
        //draw some debug rays
        //this one is for showing what the 3rd parameter to Quaternion.Euler does
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, rayRotation) * Vector3.down, Color.white);

        //these draw the left and right rays we'll use for WanderLeft() and WanderRight()
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -45) * Vector3.down, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, 45) * Vector3.down, Color.green);



        switch (_currentMoveType)
        {
            case MoveType.Left:
                _currentMoveType = WanderLeft();
                break;
            case MoveType.Right:
                _currentMoveType = WanderRight();
                break;
            case MoveType.Random:
                WanderRandom();
                break;
            case MoveType.Seek:
                Seek();
                break;
        }
    }

    MoveType WanderLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (IsEdgeTowardsAngle(-45))
        {
            //we hit nothing, so we should turn around
            if (verboseMessages) Debug.Log("Found a cliff! Turning around.");
            return MoveType.Right;
        }

        return MoveType.Left;
    }

    MoveType WanderRight()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (IsEdgeTowardsAngle(45))
        {
            //we hit nothing, so we should turn around
            if (verboseMessages) Debug.Log("Found a cliff! Turning around.");
            return MoveType.Left;
        }

        return MoveType.Right;
    }

    bool IsEdgeTowardsAngle(float direction)
    {
        //raycast towards "direction" to see if we are near an edge
        //if we return no hits that means there is empty space over there
        //last parameter is how far to cast, using 1 here, since that is about the diameter of our enemy
        //the hits argument is the array that lists the things we hit, we don't use this here, but its needed
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int rayHits = _collider.Raycast(Quaternion.Euler(0, 0, direction) * Vector3.down, hits, 1.0f);
        return rayHits == 0;

    }

    bool _finishedWander = true;
    void WanderRandom()
    {
        if(_finishedWander)
            if (Random.Range(0f, 1f) < 0.5)
                StartCoroutine(WanderCoroutine(MoveType.Left));
            else
                StartCoroutine(WanderCoroutine(MoveType.Right));
    }

    IEnumerator WanderCoroutine(MoveType direction)
    {
        float wanderTime = 2f;
        _finishedWander = false;
        if (verboseMessages) Debug.Log("Starting Wander towards " + direction.ToString());

        while (wanderTime > 0)
        {
            if (direction == MoveType.Left)
                direction = WanderLeft();
            else
                direction = WanderRight();
            wanderTime -= Time.deltaTime;
            yield return null;
        }
        _finishedWander = true;
    }

    void Seek()
    {

    }

    bool LineOfSight()
    {
        return false;
    }

    void InstantJump()
    {

    }


}
