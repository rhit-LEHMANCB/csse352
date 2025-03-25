using UnityEngine;
using TMPro;

public class LivesSetter : MonoBehaviour
{
    //we're just using this bool
    //to test different UI options
    [SerializeField] bool useText = true;

    void Start()
    {
        //from before the EventBus
        //UpdateLives(GameManagerSingleton.Instance.Lives);
        UpdateLives(GameManagerWithEvents.Instance.Lives);
        //set up the listener for lives updates
        //we're making an anonymous function here
        //basically a lambda 
        EventBus.Subscribe(EventBus.EventType.PlayerDie,
            () => UpdateLives(GameManagerWithEvents.Instance.Lives));
    }

    //this is just for testing, a real game would just
    //pick one of the two implementations below
    public void UpdateLives(int lives)
    {
        if (useText)
            setTextLives(lives);
        else
            setSpriteLives(lives);
    }

    void setTextLives(int lives)
    {
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = "Lives: " + lives.ToString();
    }

    //so we can hook this up in the Editor
    [SerializeField] GameObject[] lifeObjects;

    void setSpriteLives(int lives)
    {
        //update the active status of each of the objects
        int i = 0;
        foreach(GameObject go in lifeObjects)
        {
            if (i < lives)
                go.SetActive(true);
            else
                go.SetActive(false);
            i++;
        }
    }
}
