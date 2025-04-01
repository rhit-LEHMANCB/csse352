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
    {    }

    private Monster[] MakePossibleMonsters()
    {
        return null;
    }

    public Monster PickRandomMonster()
    {
        return null;
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
