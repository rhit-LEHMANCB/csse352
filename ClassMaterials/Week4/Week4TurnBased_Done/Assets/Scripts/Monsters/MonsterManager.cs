using UnityEngine;
using System.Collections.Generic;

public class MonsterManager : Singleton<MonsterManager>
{
    [SerializeField] MonsterData[] monsterDatabase;
    Dictionary<string, Monster> nameToMonsterDictionary;

    //we're using this getter to force initialization before we use it the first time
    //if we only have this in Start() sometimes the GameManager
    //may try to use the Dictionary before our Start() has happened
    public Dictionary<string, Monster> NameToMonsterDictionary {
        get {
            if (nameToMonsterDictionary == null)
            {
                InitializeMonsters();
            }
            return nameToMonsterDictionary;
        }

        set => nameToMonsterDictionary = value; }

    public void Start()
    {
        if (NameToMonsterDictionary == null)
            InitializeMonsters();
    }

    void InitializeMonsters()
    {
        NameToMonsterDictionary = new Dictionary<string, Monster>();
        foreach (MonsterData md in monsterDatabase)
        {
            NameToMonsterDictionary.Add(md.name,
                Monster.MakeNewMonster(md.name, md.maxHP,
                                    MoveManager.GetRandomMoves(),
                                    md.attack, md.defense, md));
        }

    }

    public Monster GetMonsterbyName(string name)
    {
        if (!NameToMonsterDictionary.ContainsKey(name))
            return null;

        return NameToMonsterDictionary[name];
    }

    public Monster[] GetAllMonsters()
    {
        Monster[] monsters = new Monster[NameToMonsterDictionary.Count];
        //CopyTo will fill out an array
        //starting from the index (in the Values collection) in the second argument
        //until the array is full
        //so this copies the whole thing
        NameToMonsterDictionary.Values.CopyTo(monsters, 0);

        return monsters;
    }
}

//serializeable flag is needed to actually show the struct in the editor
[System.Serializable]
public struct MonsterData
{
    public int maxHP;
    public string name;
    public int attack;
    public int defense;
    public RuntimeAnimatorController animator;
}
