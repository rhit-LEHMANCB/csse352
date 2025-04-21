using UnityEngine;

public class RandomStrategy : Singleton<RandomStrategy>, IStrategy
{
    public Move Execute(Monster user, Monster enemy)
    {
        return user.moves[Random.Range(0, user.moves.Length)];
    }
}
