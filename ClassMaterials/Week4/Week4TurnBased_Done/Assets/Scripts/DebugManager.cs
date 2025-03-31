using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugManager : MonoBehaviour
{

    [SerializeField] UISetter setter;
    // Start is called before the first frame update
    void Start()
    {
        setter.SetTitle("FIRST MENU");
        setter.AddButton("BUTTON1", EventBus.EventType.Button1);
        setter.AddButton("BUTTON2", EventBus.EventType.Button2);
        setter.AddButton("BUTTON3", EventBus.EventType.Button3);
        setter.AddPanel();
        setter.SetPanelText("Hello there!");
    }


    // Update is called once per frame
    void Update()
    {
    }
}
