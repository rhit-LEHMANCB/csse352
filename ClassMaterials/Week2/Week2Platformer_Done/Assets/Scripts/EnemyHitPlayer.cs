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
        //check if the thing we hit was a player
        PlayerInfo playerInfo = collision.gameObject.GetComponent<PlayerInfo>();
        if (playerInfo != null)
            playerInfo.Die();

    }

    //version that uses Tags instead
    //dont forget to add the Player tag to the player GameObject
    private void TagCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInfo>().Die();
        }
    }

    //version that checks for the direction we hit the player from
    //and kills the enemy if the player hits it from above
    private void ComplexCollision(Collision2D collision)
    {
        //check if the thing we hit was a player
        PlayerInfo playerInfo = collision.gameObject.GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            //get a list of points where the colliders touch
            //the results go into an array we provide
            //the size of this array is arbitrary, we only need one point
            ContactPoint2D contactPoint = collision.GetContact(0);
            if(verboseMessages) Debug.LogFormat("{0}", contactPoint.normal);
            //the normal will be:
            //(-1, 0) - player was on the right
            //(1, 0) - player was on the left
            //(0,-1) - player was above
            //(0, 1) - player was below
            //these numbers seem backwards because its the normal of the `other' objects contact point
            if(contactPoint.normal.y < -0.5f)
            {
                //the player was approximately above us
                Debug.Log("Ouch!");
                Destroy(gameObject);
                //tell the game manager that the player got a kill
                GameManagerSingleton.Instance.IncrementKillCount();
            }
            else
            {
                //we got him!
                //inform the game manager
                GameManagerSingleton.Instance.PlayerDeath();
                //playerInfo.Die();//From before we used singleton
            }

        }
    }
}
