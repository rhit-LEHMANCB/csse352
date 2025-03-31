using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISetter : MonoBehaviour
{

    [SerializeField] Button _button1;
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _textPanel;

    //the buttons live under a game object containting:
    //Grid Layout Group -
    //              set ChildAlignment to LowerCenter
    //              Cell Size = 200, 45
    //              Spacing = 25, 75
    //              Fixed row count = 2

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    public void AddButton(string text, EventBus.EventType eventType) {
        
    }

    public void AddPanel()
    {
        //turn on the panel
        //this reference is nasty
        _textPanel.transform.parent.gameObject.SetActive(true);
    }

    public void SetPanelText(string s)
    {
        _textPanel.text = s;
    }

}
