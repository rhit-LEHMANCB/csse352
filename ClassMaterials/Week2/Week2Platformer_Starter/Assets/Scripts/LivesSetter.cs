using UnityEngine;
using TMPro;
using System;

public class LivesSetter : MonoBehaviour
{
    //we're just using this bool
    //to test different UI options
    [SerializeField] bool useText = true;
    TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
        UpdateLives(GameManager.Instance.Lives);
        EventBus.Subscribe(EventBus.EventType.PlayerDie, () => UpdateLives(GameManager.Instance.Lives));
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
        text.text = "Lives: " + lives.ToString();
    }

    //so we can hook this up in the Editor
    [SerializeField] GameObject[] lifeObjects;

    void setSpriteLives(int lives)
    {
        int i = 0;
        foreach (GameObject go in lifeObjects)
        {
            if (i < lives)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
            i++;
        }
    }
}
