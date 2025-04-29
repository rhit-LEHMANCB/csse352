using UnityEngine;
using System.Collections.Generic;
using System.IO;//ADD THIS
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    ItemGenerator itemGenerator;
    string filename;
    string filenameJSON;
    string ITEM_DIR = "items";

    private void Start()
    {
        itemGenerator = GameObject.FindAnyObjectByType<ItemGenerator>();

        //Note that Path.DirectorySeparatorChar is what you should use if you
        //make subdirectories, rather than / or \ etc
        filename = Path.Combine(Application.persistentDataPath, "item_game.dat");
        filenameJSON = Path.Combine(Application.persistentDataPath, "item_game.json");
        Debug.Log(filename);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(300, 0, 200, 20), "Save Current Item"))
            SaveItem(itemGenerator.CurrentItem);
        if (GUI.Button(new Rect(300, 20, 200, 20), "Load Saved Item"))
            LoadItem();
        if (GUI.Button(new Rect(500, 0, 200, 20), "Save Current Item JSON"))
            SaveItemJSON(itemGenerator.CurrentItem);
        if (GUI.Button(new Rect(500, 20, 200, 20), "Load Saved Item JSON"))
            LoadItemJSON();
        if (GUI.Button(new Rect(500, 40, 200, 20), "Read All Items"))
            ReadAllItems();
    }

    private void SaveItem(Item item)
    {
        //basic lazy save the whole game object
        //initialliy just use the filename hardcoded here as "item_game.dat" 
        //it will show up in the project folder
        using (FileStream stream = File.Create(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, item.CreateSaveData());
        }
    }

    private void LoadItem()
    {

        using (FileStream stream = File.Open(filename, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Dictionary<string, object> itemData = formatter.Deserialize(stream) as Dictionary<string, object>;
            Debug.Log("loaded: " + itemData["itemName"]);
            itemGenerator.GenerateItemFromSave(itemData);
        }

    }

    private void SaveItemJSON(Item item)
    {
        string jsonString = item.SaveToJsonString();
        Debug.Log(jsonString);
        System.IO.File.WriteAllText(filenameJSON, jsonString);
    }

    private void LoadItemJSON()
    {
        if (File.Exists(filenameJSON))
        {
            string jsonString = File.ReadAllText(filenameJSON);
            itemGenerator.GenerateItemFromSave(jsonString);
        }


    }

    private void ReadAllItems()
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, ITEM_DIR);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        FileInfo[] info = dir.GetFiles("*.json");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Name);
            itemGenerator.AddItemToPool(File.ReadAllText(f.FullName));
        }
    }

}
