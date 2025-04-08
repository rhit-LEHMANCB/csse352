using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MonsterManager))]
public class GameManager : Singleton<GameManager>
{
    //info about the player
    public Player player;

    //info about the enemy
    public Monster enemy;

    //info about all possible Monsters
    Monster[] possibleMonsters;

    //animation stuff
    [SerializeField] Animator friendAnimator;
    [SerializeField] Animator enemyAnimator;

    private void Start()
    {

        //old version
        //possibleMonsters = MakePossibleMonsters();
        //new version using our MonsterManager
        possibleMonsters = MonsterManager.Instance.GetAllMonsters();
        //generate a random enemy
        enemy = PickRandomMonster();
        enemy.name = "ANGRY " + enemy.name;
        //hook up the callbacks
        SetUpMonsterAnimatorCallbacks(enemy, enemyAnimator);
        //set up the neemy animations
        SetAnimations(enemy, enemyAnimator);

    }

    private Monster[] MakePossibleMonsters()
    {
        //be careful about using these, changes to these, and not copies will influence all future copies
        List<Monster> monsterList = new List<Monster>();
        /*
        monsterList.Add(Monster.MakeNewMonster("Squirrel",  10, MoveManager.GetRandomMoves(), 05, 05));
        monsterList.Add(Monster.MakeNewMonster("Alpaca",    25, MoveManager.GetRandomMoves(), 10, 07));
        monsterList.Add(Monster.MakeNewMonster("Pet Rock",  50, MoveManager.GetRandomMoves(), 01, 20));
        monsterList.Add(Monster.MakeNewMonster("Goblin",    15, MoveManager.GetRandomMoves(), 15, 01));
        */
        return monsterList.ToArray();
    }

    public Monster PickRandomMonster()
    {
        Monster chosenOne = possibleMonsters[Random.Range(0, possibleMonsters.Length)];
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

    void SetUpMonsterAnimatorCallbacks(Monster mon, Animator anim)
    {
        mon.SetListeners(() => { anim.SetTrigger("hurt"); },
            () => { anim.SetTrigger("attack"); },
            () => { anim.SetBool("isAlive", false); });
    }

    //this overload is so the player can call without knowing the animator
    public void SetAnimations(Monster monster)
    {
        SetUpMonsterAnimatorCallbacks(monster, friendAnimator);
        SetAnimations(monster, friendAnimator);
    }

    void SetAnimations(Monster monster, Animator animator)
    {
        Debug.Log("Setting animations for " + monster.monsterData.name);
        MonsterData data = monster.monsterData;
        animator.runtimeAnimatorController = data.animator;
    }

}
