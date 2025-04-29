using UnityEngine;
using System.Collections.Generic;

public class Item : MonoBehaviour, IItem
{
    ScriptableItem baseItem;

    public string itemName;
    public GameObject itemPrefab;
    public string itemDescription;

    public int strength;
    public int intelligence;

    public int minDamage;
    public int maxDamage;

    [Tooltip("The equipment slot this item occupies.")]
    public ScriptableItem.Slot itemSlot;

    //this should probably be an enum as well....
    [Tooltip("The sub-type of item this is (e.g. sword, axe).")]
    public string itemType;

    public void SetUp(ScriptableItem itemData)
    {
        baseItem = itemData;

        itemName = itemData.itemName;
        itemPrefab = itemData.itemPrefab;
        itemDescription = itemData.itemDescription;
        strength = itemData.strength;
        intelligence = itemData.intelligence;
        minDamage = itemData.minDamage;
        maxDamage = itemData.maxDamage;
        itemSlot = itemData.itemSlot;
        itemType = itemData.itemType;

    }

    public void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }

    public Dictionary<string, object> CreateSaveData()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        saveData.Add("itemName", itemName);
        saveData.Add("itemDescription", itemDescription);
        saveData.Add("strength", strength);
        saveData.Add("intelligence", intelligence);
        saveData.Add("minDamage", minDamage);
        saveData.Add("maxDamage", maxDamage);
        saveData.Add("itemSlot", itemSlot);
        saveData.Add("itemType", itemType);

        return saveData;
    }

    public void LoadSaveData(Dictionary<string, object> saveData)
    {
        itemName = (string)saveData["itemName"];
        itemDescription = (string)saveData["itemDescription"];
        strength = (int)saveData["strength"];
        intelligence = (int)saveData["intelligence"];
        minDamage = (int)saveData["minDamage"];
        maxDamage = (int)saveData["maxDamage"];
        itemSlot = (ScriptableItem.Slot)saveData["itemSlot"];
        itemType = (string)saveData["itemType"];

    }

    public void LoadSaveData(string saveData)
    {
        JsonUtility.FromJsonOverwrite(saveData, this);

    }

    public string SaveToJsonString()
    {
        return JsonUtility.ToJson(this, true);
    }
}
