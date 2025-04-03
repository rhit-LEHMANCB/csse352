using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool verboseMessages = true;

    enum MoveType { Left, Right, Random, Seek};
    [SerializeField] MoveType _currentMoveType;

    public float speed = 2f;

    CircleCollider2D _collider;
    Rigidbody2D enemyBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        enemyBody = GetComponent<Rigidbody2D>();
    }

    public float rayRotation = 30;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, rayRotation) * Vector3.down, Color.white);
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
        enemyBody.linearVelocityX = -speed;
        if (IsEdgeTowardsAngle(-45))
        {
            if (verboseMessages) Debug.Log("Edge detected to the left, changing direction to right");
            return MoveType.Right;
        }

        return MoveType.Left;
    }

    MoveType WanderRight()
    {
        enemyBody.linearVelocityX = speed;
        if (IsEdgeTowardsAngle(45))
        {
            if (verboseMessages) Debug.Log("Edge detected to the right, changing direction to left");
            return MoveType.Left;
        }

        return MoveType.Right;
    }

    // returns true if there is no ground in the direction of the angle
    bool IsEdgeTowardsAngle(float direction)
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int rayHits = _collider.Raycast(Quaternion.Euler(0, 0, direction) * Vector3.down, hits, 1f);

        return rayHits == 0; 

    }

    bool _finishedWander = true;
    void WanderRandom()
    {
        if (_finishedWander)
        {
            if (Random.Range(0f, 1f) < 0.5)
            {
                StartCoroutine(WanderCoroutine(MoveType.Left));
            }
            else
            {
                StartCoroutine(WanderCoroutine(MoveType.Right));
            }
        }
    }

    IEnumerator WanderCoroutine(MoveType direction)
    {
        _finishedWander = false;
        float wanderTimer = 2f;

        if (verboseMessages) Debug.Log("Starting to wander towards " + direction.ToString());

        while (wanderTimer > 0)
        {
            if (direction == MoveType.Left)
            {
                direction = WanderLeft();
            }
            else
            {
                direction = WanderRight();
            }
            wanderTimer -= Time.deltaTime;
            yield return null;
        }

        _finishedWander = true;
    }

    public float visionDistance = 2.5f;
    void Seek()
    {

        PlayerInfo player = GameManager.Instance.GetPlayer();
        if (player == null)
        {
            if (verboseMessages) Debug.Log("No player found");
            return;
        }
        float playerDistance = Vector2.Distance(player.transform.position, transform.position);

        if (playerDistance < visionDistance)
        {
            // move towards player
            Vector3 directionToPlayer = player.transform.position - transform.position;

            Vector3 movement = directionToPlayer.normalized;

            if (!LineOfSight(directionToPlayer))
            {
                return;
            }

            enemyBody.linearVelocityX = movement.x * speed;
        }
    }

    bool LineOfSight(Vector3 direction)
    {
        Debug.DrawRay(transform.position, direction, Color.red);

        RaycastHit2D hits = Physics2D.Raycast(transform.position, direction, visionDistance, ~LayerMask.GetMask("Enemy"));

        if (!hits) return false;

        if (verboseMessages) Debug.LogFormat("I see {0}", hits.transform.name);

        return hits.transform.GetComponent<PlayerInfo>() != null;
    }

    void InstantJump()
    {

    }


}
