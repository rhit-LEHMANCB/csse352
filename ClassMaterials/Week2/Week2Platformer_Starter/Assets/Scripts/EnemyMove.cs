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
        

        return MoveType.Left;
    }

    MoveType WanderRight()
    {
        

        return MoveType.Right;
    }

    bool IsEdgeTowardsAngle(float direction)
    {

       return false; 

    }

    bool _finishedWander = true;
    void WanderRandom()
    {
        
    }

    IEnumerator WanderCoroutine(MoveType direction)
    {
        
        yield return null;
        
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
