using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public int maxHitpoints;
    public int hitpoints;
    public Move[] moves;
    public int attack;
    public int defense;

    public MonsterData monsterData;

    //this is just for our use in the editor
    [SerializeField] string movesString;

    public static Monster MakeNewMonster(string name, int startingHP, Move[] moves, int attack, int defense, MonsterData monsterData)
    {
        //should probably set this as a parent somewhere
        GameObject newMonsterGO = new GameObject();
        newMonsterGO.name = name;

        Monster newMonster = newMonsterGO.AddComponent<Monster>();
        newMonster.monsterName = name;
        newMonster.maxHitpoints = startingHP;
        newMonster.hitpoints = startingHP;
        newMonster.moves = moves;
        newMonster.attack = attack;
        newMonster.defense = defense;
        newMonster.monsterData = monsterData;

        //sets up the movesString for the inspector
        newMonster.movesString = "";
        foreach (Move m in newMonster.moves)
        {
            newMonster.movesString += " " + m.ToString();
        }

        return newMonster;
    }

    public static Monster MakewNewMonster(GameObject monsterPrefab)
    {
        Monster otherMonster = monsterPrefab.GetComponent<Monster>();

        //should probably set this as a parent somewhere
        GameObject newMonsterGO = new GameObject();
        newMonsterGO.name = PickRandomName();

        Monster newMonster = newMonsterGO.AddComponent<Monster>();
        newMonster.monsterName = otherMonster.name;
        newMonster.maxHitpoints = otherMonster.hitpoints;
        newMonster.hitpoints = otherMonster.hitpoints;
        newMonster.moves = otherMonster.moves;
        newMonster.attack = otherMonster.attack;
        newMonster.defense = otherMonster.defense;
        newMonster.monsterData = otherMonster.monsterData;

        //sets up the movesString for the inspector
        newMonster.movesString = "";
        foreach (Move m in newMonster.moves)
        {
            newMonster.movesString += " " + m.ToString();
        }

        return newMonster;
    }


    static string PickRandomName()
    {
        string[] possibleDescriptors = new string[] { "Funny", "Furry", "Ferocious", "Fat", "Skinny", "Affectionate", "Terrible"};
        string[] possibleNames = new string[] { "Luna", "Bella", "Charlie", "Lucy", "Daisy", "Cooper", "Bailey", "Max" };
        string descriptor = possibleDescriptors[Random.Range(0, possibleDescriptors.Length - 1)];
        string name =  possibleNames[Random.Range(0, possibleNames.Length - 1)];
        return descriptor + " " + name;
    }

    //returns true if the Monster is still alive, false otherwise
    public bool DealDamage(int damage)
    {
        if(damage > 0)
        {
            //we are being hurt
            //call all our listeners
            if(OnHurt != null)
                OnHurt.Invoke();
        }

        hitpoints -= damage;

        if(hitpoints <= 0)
        {
            //we died
            //call our listeners
            if (OnDie != null)
                OnDie.Invoke();
        }

        return hitpoints > 0;
    }

    //returns true if the attack was changed, false otherwise
    //attack canot be lowered below 1, or raised above 20
    public bool ModifyAttack(int val)
    {
        int newAttack = Mathf.Clamp(attack + val, 1, 20);
        if (newAttack == attack)
            return false;

        attack = newAttack;
        return true;
    }

    //returns true if the defense was changed, false otherwise
    //attack canot be lowered below 1, or raised above 20
    public bool ModifyDefense(int val)
    {
        int newDefense = Mathf.Clamp(defense + val, 1, 20);
        if (newDefense == defense)
            return false;

        defense = newDefense;
        return true;
    }

    //AI choosing Move based on a strategy
    //serialized so I can set it in the Editor
    [SerializeField] IStrategy myStrategy;

    //used to allow strategy changes or not
    //if set to false will use whatever strategy was last assigned
    [SerializeField] bool canChangeStrat = true;
    
    public void AIChooseMove()
    {
        //first pick my strategy
        //pick a random one if I don't have one
        if (myStrategy == null)
            myStrategy = RandomStrategy.Instance;

        if (canChangeStrat)
        {
            float hpFraction = ((float) hitpoints) / maxHitpoints;
            //if high health use the Effect strategy
            if (hpFraction > 0.8f)
                myStrategy = EffectStrategy.Instance;
            //if medium do random stuff
            else if (hpFraction > 0.3)
                myStrategy = RandomStrategy.Instance;
            //if low health use Damage to finish them off
            else
                myStrategy = DamageStrategy.Instance;
        }

        //then use it to pick a move
        Move chosenMove = myStrategy.Execute(this, GameManager.Instance.player.friend);
        //apply the move Effects
        chosenMove.Effect(this, GameManager.Instance.player.friend);
    }

    //listeners etc for animations

    //For use with animations etc
    //we'll set up some delgates to use when we need them
    UnityEvent OnHurt;
    UnityEvent OnAttack;
    UnityEvent OnDie;

    //set up our listeners 
    public void SetListeners(UnityAction onHurtListener, UnityAction onAttackListener, UnityAction onDieListener)
    {
        if (OnHurt == null)
            OnHurt = new UnityEvent();
        OnHurt.AddListener(onHurtListener);

        if (OnAttack == null)
            OnAttack = new UnityEvent();
        OnAttack.AddListener(onAttackListener);

        if (OnDie == null)
            OnDie = new UnityEvent();
        OnDie.AddListener(onDieListener);
    }

    //we'll use this to deal with attacks
    //since we don't always do this for every move
    public void InvokeAttackListeners()
    {
        if (OnAttack != null)
            OnAttack.Invoke();
    }

}
