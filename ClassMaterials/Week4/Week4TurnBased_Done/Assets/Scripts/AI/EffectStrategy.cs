using System.Collections.Generic;
using UnityEngine;

public class EffectStrategy : Singleton<DamageStrategy>, IStrategy
{
    public Move Execute(Monster user, Monster enemy)
    {
        List<Move> goodMoves = new List<Move>();
        foreach (Move move in user.moves)
        {
            if (move.moveType == Move.MoveType.effect)
            {
                goodMoves.Add(move);
            }
        }

        Move[] toPick = goodMoves.ToArray();
        if (goodMoves.Count == 0)
        {
            //if we don't have any damage moves, just pick a random one
            toPick = user.moves;
        }

        return toPick[Random.Range(0, toPick.Length)];
    }
}
