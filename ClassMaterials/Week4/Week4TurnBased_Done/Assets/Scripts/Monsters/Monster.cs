using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public int hitpoints;
    public Move[] moves;
    public int attack;
    public int defense;

    //this is just for our use in the editor
    [SerializeField] string movesString;

    public static Monster MakeNewMonster(string name, int startingHP, Move[] moves, int attack, int defense)
    {
        //should probably set this as a parent somewhere
        GameObject newMonsterGO = new GameObject();
        newMonsterGO.name = name;

        Monster newMonster = newMonsterGO.AddComponent<Monster>();
        newMonster.monsterName = name;
        newMonster.hitpoints = startingHP;
        newMonster.moves = moves;
        newMonster.attack = attack;
        newMonster.defense = defense;

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
        newMonster.hitpoints = otherMonster.hitpoints;
        newMonster.moves = otherMonster.moves;
        newMonster.attack = otherMonster.attack;
        newMonster.defense = otherMonster.defense;

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
        hitpoints -= damage;
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

}
