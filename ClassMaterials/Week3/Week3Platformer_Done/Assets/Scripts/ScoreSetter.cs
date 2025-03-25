using UnityEngine;
using TMPro;

public class ScoreSetter : MonoBehaviour
{

    TMP_Text text;

    void Start()
    {
        //from before the EventBus
        //UpdateScore(GameManagerSingleton.Instance.KillCount);
        UpdateScore(GameManagerWithEvents.Instance.KillCount);
        //set up the listener for lives updates
        //we're making an anonymous function here
        //basically a lambda 
        EventBus.Subscribe(EventBus.EventType.PlayerDie,
            () => UpdateScore(GameManagerWithEvents.Instance.KillCount));
    }

    public void UpdateScore(int s)
    {
        if(text == null)
        {
            text = GetComponent<TMP_Text>();
        }
        text.text = "Score: " + s.ToString();
    }
}
