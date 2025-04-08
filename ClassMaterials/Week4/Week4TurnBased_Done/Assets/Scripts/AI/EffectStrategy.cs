using UnityEngine;
using System.Collections.Generic;

public class EffectStrategy : Singleton<EffectStrategy>, IStrategy
{
    public Move Execute(Monster user, Monster enemy)
    {
        List<Move> validMoves = new List<Move>();
        foreach(Move m in user.moves)
        {
            if (m.moveType == Move.MoveType.effect)
                validMoves.Add(m);
        }

        Move[] validMovesArray = validMoves.ToArray();

        if (validMovesArray.Length == 0)
        {
            //there were no Effect moves, just pick randomly then
            validMovesArray = user.moves;
        }

        //Debug.LogFormat("(EffectStrategy) Chosing From {0}", MoveManager.MoveArrayToString(validMovesArray));

        return validMovesArray[Random.Range(0, validMovesArray.Length - 1)];
    }
}
