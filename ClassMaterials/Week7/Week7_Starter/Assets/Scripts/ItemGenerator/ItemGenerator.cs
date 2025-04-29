using UnityEngine;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour
{
    Item currentItem = null;

    [Header("Starting Items")]
    [SerializeField]
    ScriptableItem[] startingItemsArray;

    [Header("Modifiers")]
    [SerializeField]
    ScriptableModifier[] modifiersArray;


    public bool destroyOld = false;

    List<ScriptableItem> baseItems;
    List<ScriptableModifier> modifiers;

    public Item CurrentItem { get => currentItem; set => currentItem = value; }

    // Use this for initialization
    void Start()
    {
              
    }

    private void OnGUI()
    {
      

    }

    private void DisplayItem(Item item)
    {
        GUI.color = Color.green;
        GUI.Label(new Rect(125, 0, 800, 20), "Item Name: " + item.itemName);
        GUI.Label(new Rect(125, 20, 200, 20), "Strength: " + item.strength);
        GUI.Label(new Rect(125, 40, 200, 20), "Intelligence: " + item.intelligence);
        GUI.Label(new Rect(125, 60, 200, 20), "Damage: " + item.minDamage + "-" + item.maxDamage);
        GUI.Label(new Rect(125, 80, 200, 20), "Slot: " + item.itemSlot.ToString() + "(" + item.itemType + ")");
        GUI.Label(new Rect(125, 100, 800, 20), "Description: " + item.itemDescription);

    }

    private void GenerateItem()
    {
        
    }

    private void AddModifier()
    {
        
    }

    public void GenerateItemFromSave(Dictionary<string, object> saveInfo)
    {
        if (destroyOld && CurrentItem != null)
            Destroy(CurrentItem.gameObject);
        GameObject newItem = new GameObject();
        CurrentItem = newItem.AddComponent<Item>();
        CurrentItem.LoadSaveData(saveInfo);

    }

    public void GenerateItemFromSave(string saveInfo)
    {
        if (destroyOld && CurrentItem != null)
            Destroy(CurrentItem.gameObject);
        GameObject newItem = new GameObject();
        CurrentItem = newItem.AddComponent<Item>();
        CurrentItem.LoadSaveData(saveInfo);

    }

    public void AddItemToPool(string itemInfo)
    {
        ScriptableItem newItem = ScriptableObject.CreateInstance<ScriptableItem>();
        JsonUtility.FromJsonOverwrite(itemInfo, newItem);
        baseItems.Add(newItem);
        Debug.Log("Created and loaded " + newItem.itemName + " into the pool");

    }
}
