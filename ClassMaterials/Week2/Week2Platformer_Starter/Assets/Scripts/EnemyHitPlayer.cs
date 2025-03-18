using UnityEngine;

public class EnemyHitPlayer : MonoBehaviour
{
    public bool verboseMessages = true;
    public enum collisionType { Basic, Tag, Complex };
    public collisionType myCollisionType = collisionType.Basic;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this switch is just to demonstrate the different
        //possible implementations
        //the body of any of the methods below could just
        //be here instead
        switch (myCollisionType)
        {
            case collisionType.Basic:
                BasicCollision(collision);
                break;
            case collisionType.Tag:
                TagCollision(collision);
                break;
            case collisionType.Complex:
                ComplexCollision(collision);
                break;
        }
    }

    //basic version that kills player on any touch
    void BasicCollision(Collision2D collision)
    {
       

    }

    //version that uses Tags instead
    //dont forget to add the Player tag to the player GameObject
    private void TagCollision(Collision2D collision)
    {
        
    }

    //version that checks for the direction we hit the player from
    //and kills the enemy if the player hits it from above
    private void ComplexCollision(Collision2D collision)
    {
       
    }
}
