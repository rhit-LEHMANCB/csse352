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
        //GameObject gameObject = new GameObject();
        //Item shirt = gameObject.AddComponent<Item>();
        //shirt.itemName = "Shirt";
        //shirt.strength = 1;
        //shirt.intelligence = 1;
        //shirt.minDamage = 0;
        //shirt.maxDamage = 2;
        //shirt.itemSlot = ScriptableItem.Slot.BODY;
        //shirt.itemType = "Clothing";
        //shirt.itemDescription = "It is just a shirt. I guesss I could slap someone with this.";
        //CurrentItem = shirt; 

        baseItems = new List<ScriptableItem>();
        foreach (ScriptableItem item in startingItemsArray)
        {
            baseItems.Add(item);
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Make New Item"))
        {
            GenerateItem();
        }

        if (CurrentItem != null)
        {
            DisplayItem(CurrentItem);
        }
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
        ScriptableItem scriptableItem = baseItems[Random.Range(0, baseItems.Count)];
        Item item = new GameObject().AddComponent<Item>();
        item.SetUp(scriptableItem);
        CurrentItem = item;
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
