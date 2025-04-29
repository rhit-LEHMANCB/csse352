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
        //make one basic Item to start.
        /*
        GameObject GO = new GameObject();
        Item shirt = GO.AddComponent<Item>();
        shirt.itemName = "Shirt";
        shirt.strength = 1;
        shirt.intelligence = 1;
        shirt.minDamage = 0;
        shirt.maxDamage = 1;
        shirt.itemSlot = ScriptableItem.Slot.BODY;//this is just an Enum defined elsewhere for us
        shirt.itemType = "Clothing";
        shirt.itemDescription = "It is just a shirt. I guess I could slap someone with it.";
        CurrentItem = shirt;
        */

        //read in all our starting items
        baseItems  = new List<ScriptableItem>();
        foreach(ScriptableItem item in startingItemsArray)
        {
            baseItems.Add(item);
        }

        //read in our modifiers
        modifiers = new List<ScriptableModifier>();
        foreach(ScriptableModifier mod in modifiersArray)
        {
            modifiers.Add(mod);
        }        
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Make New Item"))
            GenerateItem();
        if (GUILayout.Button("Add Modifier"))
            AddModifier();
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
        int r = Random.Range(0, baseItems.Count);
        if (destroyOld && CurrentItem != null)
            Destroy(CurrentItem.gameObject);
        GameObject newItem = new GameObject();
        CurrentItem = newItem.AddComponent<Item>();
        CurrentItem.SetUp(baseItems[r]);
    }

    private void AddModifier()
    {
        int r = Random.Range(0, modifiers.Count);
        if (CurrentItem != null)
            CurrentItem.Accept(modifiers[r]);
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
