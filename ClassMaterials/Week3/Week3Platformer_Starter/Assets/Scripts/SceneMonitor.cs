using UnityEngine;

public class SceneMonitor : MonoBehaviour
{
    void Start()
    {
        //tell anyone listening we have loaded up a new scene
        //doing this in Start() so all Awake()'s can be done
        //I could add other checks and do this in Update() or
        //wait for other events to publish before doing this
        EventBus.Publish(EventBus.EventType.SceneLoaded);
    }
}
