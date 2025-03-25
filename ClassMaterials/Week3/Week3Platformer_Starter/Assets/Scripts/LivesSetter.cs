using UnityEngine;
using TMPro;

public class LivesSetter : MonoBehaviour
{
    //we're just using this bool
    //to test different UI options
    [SerializeField] bool useText = true;

    void Start()
    {
        
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
       
    }

    //so we can hook this up in the Editor
    [SerializeField] GameObject[] lifeObjects;

    void setSpriteLives(int lives)
    {
        
    }
}
