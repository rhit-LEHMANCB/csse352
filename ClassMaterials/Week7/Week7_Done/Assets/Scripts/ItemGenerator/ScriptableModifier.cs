using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RobModifier", menuName = "CSSE352Objects/RobItemModifier")]
public class ScriptableModifier : ScriptableObject, IItemVisitor
{
    public int strengthMod;
    public int intelligenceMod;
    public int minDamageMod;
    public int maxDamageMod;

    public string namePrefix;

    public void Visit(Item item)
    {
        item.strength += strengthMod;
        item.intelligence += intelligenceMod;
        item.minDamage += minDamageMod;
        item.maxDamage += maxDamageMod;
        item.itemName = namePrefix + " " + item.itemName;
    }
}
