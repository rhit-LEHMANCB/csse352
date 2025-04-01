using UnityEngine;
using System;
using System.Collections.Generic;

//Steps to set up a new Move
//  1. Add a new MoveID to the Move.MoveID enum
//  2. Define a new method in MoveEffects that describes the Move's behavior
//  3. Add an Entry in MoveManager.Moves that creates a new Move object with the Move info

//holds all the information about a move
//the Enums here will be used elsewhere for finding and comparing Moves
public class Move
{
    public enum MoveID { tackle, scratch, shield, sleep, poison };
    public enum MoveType { damage, effect };

    public string name;
    public MoveID moveID;
    public MoveType moveType;
    Action<Monster, Monster> effectDelegate;

    public Move(string name, MoveID moveID, MoveType moveType, Action<Monster, Monster> effect)
    {
        this.name = name;
        this.moveID = moveID;
        this.moveType = moveType;
        effectDelegate = effect;
    }

    public void Effect(Monster user, Monster other)
    {
    }

    public override string ToString()
    {
        return name;
    }
     
}

public static class MoveManager
{
    //build our array of Moves
    public static Move[] Moves = new Move[] {
    };

    //doing this a lot is going to be slow, so we should avoid calling this
    public static Move GetMoveByID(Move.MoveID moveID)
    {
        foreach(Move move in Moves)
        {
            if (move.moveID == moveID)
                return move;
        }

        return null;
    }

    //pick random 4 moves for making new monsters
    //sometimes there will be duplicate moves
    public static Move[] GetRandomMoves()
    {
        List<Move> chosenMoves = new List<Move>();
        for(int i = 0; i < 4; i++)
        {
            chosenMoves.Add(Moves[UnityEngine.Random.Range(0, Moves.Length - 1)]);
        }

        return chosenMoves.ToArray();
    }

}

//class defining the behavior of each move
static class MoveEffects
{
    //does a flat amount of damage
    public static void Tackle(Monster user, Monster other)
    {
        Debug.Log("Used tackle");
    }

    //does an amount of damage equal to the user's attack
    //reduced by defense, each point of defense lowers the
    //damage by 1 point, to a miniumum of 1
    public static void Scratch(Monster user, Monster other)
    {
        Debug.Log("Used scratch");

    }

    //adds to the user's defense +2
    public static void Shield(Monster user, Monster other)
    {
        Debug.Log("Used shield");
    }

    //reduces the other's defense -2
    public static void Sleep(Monster user, Monster other)
    {
        Debug.Log("Used sleep");

    }

    //reduces the other's attack -2
    public static void Poison(Monster user, Monster other)
    {
        Debug.Log("Used poison");

    }
}
