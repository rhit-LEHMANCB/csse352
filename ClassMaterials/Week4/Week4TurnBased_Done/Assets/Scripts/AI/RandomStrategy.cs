using UnityEngine;

//Singleton ensures that all AIs have access to an instance of each strategy
public class RandomStrategy : Singleton<RandomStrategy>, IStrategy
{
    public Move Execute(Monster user, Monster enemy)
    {
        //Debug.LogFormat("(RandomStrategy) Chosing From {0}", MoveManager.MoveArrayToString(user.moves));
        //just pick a totally random one
        return user.moves[Random.Range(0, user.moves.Length - 1)];
    }
}
