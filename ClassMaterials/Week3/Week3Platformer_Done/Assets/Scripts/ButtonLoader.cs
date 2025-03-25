using UnityEngine;

public class ButtonLoader : MonoBehaviour
{
    public void OnClickLoad()
    {
        GameManagerSingleton.Instance.LoadNextLevel();
    }

    public void OnClickLoadWithEvent()
    {
        //tell everyone that the load button was pressed
        EventBus.Publish(EventBus.EventType.LoadLevelButton);
    }
}
