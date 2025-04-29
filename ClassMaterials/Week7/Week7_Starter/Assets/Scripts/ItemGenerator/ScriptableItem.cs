using System;
using UnityEngine;


[CreateAssetMenu(fileName = "RobItem", menuName = "CSSE352Objects/RobItem")]
public class ScriptableItem : ScriptableObject
{

    public enum Slot {HELM, BODY, HAND, FEET};

    public string itemName;
    public GameObject itemPrefab;
    public string itemDescription;

    public int strength;
    public int intelligence;

    public int minDamage;
    public int maxDamage;

    [Tooltip("The equipment slot this item occupies.")]
    public Slot itemSlot;

    //this should probably be an enum as well....
    [Tooltip("The sub-type of item this is (e.g. sword, axe).")]
    public string itemType;


}
