using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    //info about the player
    public Player player;

    //info about the enemy
    public Monster enemy;

    //info about all possible Monsters
    Monster[] possibleMonsters;

    private void Start()
    {
        possibleMonsters = MakePossibleMonsters();
        //generate a random enemy
        enemy = PickRandomMonster();
        enemy.name = "ANGRY " + enemy.name;

    }

    private Monster[] MakePossibleMonsters()
    {
        //be careful about using these, changes to these, and not copies will influence all future copies
        List<Monster> monsterList = new List<Monster>();

        monsterList.Add(Monster.MakeNewMonster("Squirrel",  10, MoveManager.GetRandomMoves(), 05, 05));
        monsterList.Add(Monster.MakeNewMonster("Alpaca",    25, MoveManager.GetRandomMoves(), 10, 07));
        monsterList.Add(Monster.MakeNewMonster("Pet Rock",  50, MoveManager.GetRandomMoves(), 01, 20));
        monsterList.Add(Monster.MakeNewMonster("Goblin",    15, MoveManager.GetRandomMoves(), 15, 01));

        return monsterList.ToArray();
    }

    public Monster PickRandomMonster()
    {
        Monster chosenOne = possibleMonsters[Random.Range(0, possibleMonsters.Length - 1)];
        //make a copy so I dont mess up my "reference" objects
        Monster chosenMonster = Monster.MakewNewMonster(chosenOne.gameObject);
        //return the Monster component
        return chosenMonster.GetComponent<Monster>();
    }

    BattleFSM battleFSM;
    public BattleFSM GetFSM()
    {
        if (battleFSM == null)
        {
            battleFSM = GetComponent<BattleFSM>();
        }
        return battleFSM;
    }

}
